

namespace PSMemo.Repository;

public interface IMemoRepository
{
    public IEnumerable<string> GetAll(string key);

    public void UpdateAll(string key, IEnumerable<string> values);

    public bool Add(string key, string value);

    public bool Remove(string key, string value);

    public void RemoveBranch(string key);
}