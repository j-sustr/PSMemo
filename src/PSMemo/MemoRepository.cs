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

    public IEnumerable<string> Get(string key)
    {
        string path = ConvertKeyToPath(key);

        string[] lines = File.ReadAllLines(path);

        return lines.Where(line => line.Trim() != String.Empty);
    }

    public void Add(string key, string value)
    {

    }

    public void Remove(string key, string value)
    {

    }
}