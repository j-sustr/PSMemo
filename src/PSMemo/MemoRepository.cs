using System.Linq;
using static PSMemo.Constants;
using static PSMemo.Utils.KeyUtils;

namespace PSMemo;

public class MemoFileRepository
{
    private readonly string root;

    public MemoFileRepository(string root)
    {
        this.root = root;
    }

    public IEnumerable<string> GetAll(string key)
    {
        string path = ConvertKeyToMemoFilePath(key);

        string[] lines = File.ReadAllLines(path);

        return lines.Select(line => line.Trim()).Where(line => line != String.Empty);
    }

    public void UpdateAll(string key, IEnumerable<string> values)
    {
        string path = ConvertKeyToMemoFilePath(key);

        File.WriteAllLines(path, values);
    }

    public void Add(string key, string value)
    {

    }

    public void Remove(string key, string value)
    {

    }

    public string ConvertKeyToMemoFilePath(string key)
    {
        string childPath = ConvertKeyToPath(key);
        string path = Path.Join(root, childPath);
        return $"{path}.{PSMemoFileExtension}";
    }
}