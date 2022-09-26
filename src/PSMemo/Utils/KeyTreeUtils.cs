using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PSMemo.Utils;


public static class KeyTreeUtils
{
    public const string Separator = ".";

    public static string ConvertKeyToPath(string key)
    {
        var components = key.Split(Separator);
        return Path.Join(components);
    }

    public static string ConvertPathToKey(string path)
    {
        path = path.Trim(Path.DirectorySeparatorChar);
        var components = path.Split(Path.DirectorySeparatorChar);
        return string.Join(Separator, components);
    }
}