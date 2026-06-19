using System;
using System.Diagnostics;

var input = "./assets/input.md";
var luaFilter = "./assets/table-filter.lua";
var template = "./assets/template.typ";
var output = "./output/output.pdf";

var arguments =
    $"{input} -o {output} " +
    $"--lua-filter={luaFilter} " +
    $"--pdf-engine=typst " +
    $"--template={template}";

var psi = new ProcessStartInfo
{
    FileName = "pandoc",
    Arguments = arguments,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false,
    CreateNoWindow = true
};

using var process = new Process { StartInfo = psi };
process.Start();

string stderr = process.StandardError.ReadToEnd();

process.WaitForExit();

if(!string.IsNullOrEmpty(stderr))
{
    Console.WriteLine("STDERR:");
    Console.WriteLine(stderr);
} else 
{
    Console.WriteLine($"File created succesfully: '{output}'!");
}

Console.WriteLine($"Exit Code: {process.ExitCode}");
