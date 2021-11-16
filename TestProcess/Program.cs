using System.Diagnostics;

var process = new Process();
process.StartInfo = new ProcessStartInfo
{
    FileName = "TestProcess32.exe",
    RedirectStandardError = true,
    RedirectStandardOutput = true
};

process.Start();
var stderr = process.StandardError.ReadToEnd();
var stdout = process.StandardOutput.ReadToEnd();

Console.WriteLine($"32bit stdout: {stdout}");
Console.WriteLine($"32bit err: {stderr}");
