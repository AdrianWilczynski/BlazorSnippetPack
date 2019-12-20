using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null)
    => Path.GetDirectoryName(path);

public static string GetProjectRoot()
    => Path.Join(GetScriptFolder(), "..");

public static string GetSnippetsFolder()
    => Path.Join(GetProjectRoot(), "snippets");