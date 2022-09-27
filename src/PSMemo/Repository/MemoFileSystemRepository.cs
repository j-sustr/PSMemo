using System.Linq;
using System.IO.Abstractions;
using PSMemo.Exception;
using static PSMemo.Constants;
using PSMemo.Utils;

namespace PSMemo.Repository;

public class MemoFileSystemRepository : IMemoRepository
{
    private readonly IFileSystem _fileSystem;
    private readonly string _root;

    public MemoFileSystemRepository(IFileSystem fileSystem, string root)
    {
        this._fileSystem = fileSystem;
        this._root = root;
    }

    public IEnumerable<string> ListCollectionKeys()
    {
        return _fileSystem.Directory.EnumerateFiles(_root)
            .Where(path => IsMemoFile(path))
            .Select(path => ConvertMemoFilePathToKey(path));
    }

    public IEnumerable<string> GetCollection(string key)
    {
        return ReadAllValues(key);
    }

    public void RemoveCollection(string key)
    {
        if (!DoesCollectionExist(key))
        {
            throw new InvalidMemoKeyException(key);
        }

        string path = ConvertKeyToMemoFilePath(key);

        _fileSystem.File.Delete(path);
    }

    public bool TryAdd(string key, string value)
    {
        if (!DoesCollectionExist(key))
        {
            WriteAllValues(key, new string[] { value });
            return true;
        }

        var values = ReadAllValues(key)
            .Prepend(value)
            .ToList();

        bool newValueIsUnique = values.Distinct().Count() == values.Count;
        if (newValueIsUnique)
        {
            WriteAllValues(key, values);
            return true;
        }
        return false;
    }

    public bool TryRemove(string key, string value)
    {
        var values = ReadAllValues(key).ToList();

        var newValues = values.Where(_value => _value != value);

        bool didRemove = newValues.Count() != values.Count;
        if (didRemove)
        {
            WriteAllValues(key, newValues);
            return true;
        }
        return false;
    }

    private IEnumerable<string> ReadAllValues(string key)
    {
        string path = ConvertKeyToMemoFilePath(key);

        string[] lines;
        try
        {
            lines = _fileSystem.File.ReadAllLines(path);
        }
        catch (FileNotFoundException)
        {
            throw new InvalidMemoKeyException(key);
        }

        return lines.Select(line => line.Trim()).Where(line => line != String.Empty);
    }

    private void WriteAllValues(string key, IEnumerable<string> values)
    {
        string path = ConvertKeyToMemoFilePath(key);

        try
        {
            _fileSystem.File.WriteAllLines(path, values);
        }
        catch (System.Exception ex)
        {
            // TODO: use correct ex type
            throw ex;
            // throw new InvalidMemoKeyException(key);
        }
    }

    private bool DoesCollectionExist(string key)
    {
        string path = ConvertKeyToMemoFilePath(key);

        return _fileSystem.File.Exists(path);
    }

    private string ConvertKeyToMemoFilePath(string key)
    {
        if (PathUtils.ContainsInvalidFileNameChars(key))
        {
            throw new InvalidMemoKeyException(key);
        }

        string fileName = $"{key}.{PSMemoFileExtension}";
        string path = Path.Join(_root, fileName);

        return path;
    }

    private string ConvertMemoFilePathToKey(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    private bool IsMemoFile(string path)
    {
        return Path.GetExtension(path) == $".{PSMemoFileExtension}";
    }
}