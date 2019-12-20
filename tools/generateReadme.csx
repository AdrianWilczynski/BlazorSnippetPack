#pragma warning disable RCS1018, RCS1110

#r "nuget: DotMarkdown, 0.1.0"
#r "nuget: Newtonsoft.Json, 12.0.3"

using System.Runtime.CompilerServices;
using DotMarkdown;
using Newtonsoft.Json;

public static string GetScriptFolder([CallerFilePath] string path = null)
    => Path.GetDirectoryName(path);

class Extension
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
}

class Snippet
{
    public string Prefix { get; set; }
}

var fileHeadingMap = new Dictionary<string, string>
{
    { "razor", "Razor" },
    { "csharp", "C#" },
    { "html", "HTML" }
};

var projectRootDirectory = Path.Join(GetScriptFolder(), "..");
var snippetsDirectory = Path.Join(projectRootDirectory, "snippets");

var extension = JsonConvert.DeserializeObject<Extension>(
    File.ReadAllText(Path.Join(projectRootDirectory, "package.json")));

var snippetFiles = new[] { "csharp.json", "razor.json", "html.json" }
    .Select(f => new
    {
        FileNameBase = Path.GetFileNameWithoutExtension(f),
        Snippets = JsonConvert.DeserializeObject<Dictionary<string, Snippet>>(
                File.ReadAllText(Path.Join(snippetsDirectory, f)))
            .OrderBy(s => s.Value.Prefix)
    });

var stringBuilder = new StringBuilder();

using (var markdownWriter = MarkdownWriter.Create(stringBuilder))
{
    markdownWriter.WriteHeading1(extension.DisplayName);
    markdownWriter.WriteLinkOrText(extension.Description);

    foreach (var snippetFile in snippetFiles)
    {
        markdownWriter.WriteHeading2(fileHeadingMap[snippetFile.FileNameBase]);

        markdownWriter.WriteStartTable(2);
        markdownWriter.WriteStartTableRow();
        markdownWriter.WriteTableCell(nameof(Snippet.Prefix));
        markdownWriter.WriteTableCell("Description");
        markdownWriter.WriteEndTableRow();
        markdownWriter.WriteTableHeaderSeparator();

        foreach (var snippet in snippetFile.Snippets)
        {
            markdownWriter.WriteStartTableRow();
            markdownWriter.WriteTableCell(snippet.Value.Prefix);
            markdownWriter.WriteTableCell(snippet.Key);
            markdownWriter.WriteEndTableRow();
        }

        markdownWriter.WriteEndTable();
    }
}

File.WriteAllText(
    Path.Join(projectRootDirectory, "README.md"),
    stringBuilder.ToString().TrimEnd());