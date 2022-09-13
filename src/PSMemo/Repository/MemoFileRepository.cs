using System.Linq;
using PSMemo.Exception;
using static PSMemo.Constants;
using static PSMemo.Utils.KeyUtils;

namespace PSMemo.Repository;

public class MemoFileRepository : IMemoRepository
{
    private readonly string root;

    public MemoFileRepository(string root)
    {
        this.root = root;
    }

    public IEnumerable<string> GetAll(string key)
    {
        return ReadAllValues(key);
    }

    public void UpdateAll(string key, IEnumerable<string> values)
    {
        WriteAllValues(key, values);
    }

    public bool TryAdd(string key, string value)
    {
        var values = ReadAllValues(key).ToList();

        values.Prepend(value);

        bool newValueIsUnique = value.Distinct().Count() == values.Count;
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

    public void RemoveBranch(string key)
    {
        string childPath = ConvertKeyToPath(key);
        string path = Path.Join(root, childPath);

        // TODO: check if file of dir
    }

    private IEnumerable<string> ReadAllValues(string key)
    {
        string path = ConvertKeyToMemoFilePath(key);

        string[] lines;
        try
        {
            lines = File.ReadAllLines(path);
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
            File.WriteAllLines(path, values);
        }
        catch (System.Exception ex)
        {
            // TODO: use correct ex type
            throw ex;
            // throw new InvalidMemoKeyException(key);
        }
    }

    private string ConvertKeyToMemoFilePath(string key)
    {
        string childPath = ConvertKeyToPath(key);
        string path = Path.Join(root, childPath);
        return $"{path}.{PSMemoFileExtension}";
    }
}