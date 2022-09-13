

namespace PSMemo.Exception;

public class InvalidMemoKeyException : System.Exception
{
    public InvalidMemoKeyException(string key) : base($"Memo key '{key}' is invalid")
    {
    }
}