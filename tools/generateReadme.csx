#pragma warning disable RCS1018, RCS1110

#load "shared.csx"
#r "nuget: DotMarkdown, 0.1.0"
#r "nuget: Newtonsoft.Json, 12.0.3"

using DotMarkdown;
using Newtonsoft.Json;

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
    { "html", "HTML" },
    { "csproj", ".csproj (XML, MSBuild)" }
};

var extension = JsonConvert.DeserializeObject<Extension>(
    File.ReadAllText(Path.Join(GetProjectRoot(), "package.json")));

var snippetFiles = new[] { "csharp.json", "razor.json", "html.json", "csproj.json" }
    .Select(f => new
    {
        FileNameBase = Path.GetFileNameWithoutExtension(f),
        Snippets = JsonConvert.DeserializeObject<Dictionary<string, Snippet>>(
                File.ReadAllText(Path.Join(GetSnippetsFolder(), f)))
            .OrderBy(s => s.Value.Prefix)
    });

var stringBuilder = new StringBuilder();

using (var markdownWriter = MarkdownWriter.Create(stringBuilder))
{
    markdownWriter.WriteHeading1(extension.DisplayName);

    markdownWriter.WriteStartItalic();

    markdownWriter.WriteLinkOrText("Created with ");
    markdownWriter.WriteLinkOrText("https://vscodesnippetgenerator.azurewebsites.net/", "https://vscodesnippetgenerator.azurewebsites.net/");

    markdownWriter.WriteLinkOrText(" (");
    markdownWriter.WriteLinkOrText("GitHub", "https://github.com/AdrianWilczynski/VSCodeSnippetGenerator/");
    markdownWriter.WriteLinkOrText(")");

    markdownWriter.WriteEndItalic();

    markdownWriter.WriteLine();
    markdownWriter.WriteLine();

    markdownWriter.WriteLinkOrText(extension.Description);

    markdownWriter.WriteLine();
    markdownWriter.WriteLine();

    markdownWriter.WriteImage("In Action", "img/InAction.gif");

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
    Path.Join(GetProjectRoot(), "README.md"),
    stringBuilder.ToString().TrimEnd());