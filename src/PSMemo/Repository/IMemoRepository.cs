

namespace PSMemo.Repository;

public interface IMemoRepository
{
    public IEnumerable<string> ListCollectionKeys();

    public IEnumerable<string> GetCollection(string key);

    public void RemoveCollection(string key);

    public bool TryAdd(string key, string value);

    public bool TryRemove(string key, string value);
}