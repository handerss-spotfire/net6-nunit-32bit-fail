using System.Diagnostics;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class Test32Bit
    {
        [Test]
        public void TheTest()
        {
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

            Assert.IsTrue(string.IsNullOrEmpty(stderr));
        }
    }
}