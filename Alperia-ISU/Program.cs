using System;
using System.IO;
using Serilog;

namespace Alperia_ISU
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\log-Z1.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            var fileInput = ofd.FileName;
            Log.Information("File processato: {0}", fileInput);
            using (var reader = new StreamReader(fileInput))
            {

            }

        }
    }
}
