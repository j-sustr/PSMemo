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
        string path = ConvertKeyToMemoFilePath(key);

        File.WriteAllLines(path, values);
    }

    public bool Add(string key, string value)
    {
        var values = ReadAllValues(key).ToList();

        values.Prepend(value);

        bool newValueIsUnique = value.Distinct().Count() == values.Count;
        if (newValueIsUnique)
        {
            WriteAllValues(key, values);
        }
    }

    public void Remove(string key, string value)
    {
        var values = ReadAllValues(key).ToList();

        var newValues = values.Where(_value => _value != value);

        WriteAllValues(key, newValues);
    }

    public void RemoveBranch(string key)
    {
        string childPath = ConvertKeyToPath(key);
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
            throw new InvalidMemoKeyException();
        }

        return lines.Select(line => line.Trim()).Where(line => line != String.Empty);
    }

    private void WriteAllValues(string key, IEnumerable<string> values)
    {
        string path = ConvertKeyToMemoFilePath(key);

        File.WriteAllLines(path, values);
    }

    private string ConvertKeyToMemoFilePath(string key)
    {
        string childPath = ConvertKeyToPath(key);
        string path = Path.Join(root, childPath);
        return $"{path}.{PSMemoFileExtension}";
    }
}