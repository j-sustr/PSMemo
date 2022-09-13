

namespace PSMemo.Repository;

public interface IMemoRepository
{
    public IEnumerable<string> GetAll(string key);

    public void UpdateAll(string key, IEnumerable<string> values);

    public bool TryAdd(string key, string value);

    public bool TryRemove(string key, string value);

    public void RemoveBranch(string key);
}