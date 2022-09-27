

using System.Text.RegularExpressions;

namespace PSMemo.Utils;

public static class PathUtils
{
    private static readonly Regex _invalidFileNameCharsPatternRegex;

    static PathUtils()
    {
        string _invalidFileNameCharsPattern = string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars())));
        _invalidFileNameCharsPatternRegex = new Regex(_invalidFileNameCharsPattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant)
    }

    public static bool ContainsInvalidFileNameChars(string fileName)
    {
        return _invalidFileNameCharsPatternRegex.IsMatch(fileName);
    }

}