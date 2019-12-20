#pragma warning disable RCS1018, RCS1110

#load "shared.csx"
#r "nuget: Newtonsoft.Json, 12.0.3"

using Newtonsoft.Json;
using System.Text.RegularExpressions;

class Snippet
{
    public IEnumerable<string> Body { get; set; }
}

foreach (var file in Directory.GetFiles(GetSnippetsFolder()))
{
    var snippets = JsonConvert.DeserializeObject<Dictionary<string, Snippet>>(
        File.ReadAllText(file));

    foreach (var snippet in snippets)
    {
        foreach (var (line, i) in snippet.Value.Body.Select((line, i) => (line, i)))
        {
            var match = Regex.Match(line, @"(?<!\$){(?! get;).+}");

            if (match.Success)
            {
                WriteLine("Possibly incomplete tab stop:");
                WriteLine($"    File: {Path.GetFileName(file)},");
                WriteLine($"    Snippet: {snippet.Key},");
                WriteLine($"    Line: {i},");
                WriteLine($"    Text: \"{line}\"");
            }
        }
    }
}