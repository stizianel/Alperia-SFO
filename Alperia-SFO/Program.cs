using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alperia_SFO
{
    
 
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            var fileInput = ofd.FileName;
            Console.WriteLine("Reading z1.csv");

            List<Z1> LZ1 = ProcessFileInput(fileInput);

            var ctx = new Z1Context();

            InsMongo(LZ1, ctx).GetAwaiter().GetResult();

            Console.WriteLine("Fine programma");
        }
        private static async Task InsMongo(List<Z1> LZ1, Z1Context ctx)
        {
            await ctx.Z1s.InsertManyAsync(LZ1);
        }
        private static List<Z1> ProcessFileInput(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Z1.ParseFromCsv)
                .ToList();
        }
    }


}
