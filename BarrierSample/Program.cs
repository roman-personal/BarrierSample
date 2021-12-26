using System;
using System.Diagnostics;

namespace BarrierSample {
    internal class Program {
        static void Main(string[] args) {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Generating...");
            var data = GenerateData(500000000); // 500M values ~ 2GB
            Console.WriteLine($"Done! Elapsed: {sw.Elapsed}");
            sw.Stop();
            Console.WriteLine("Normalizing...");
            sw.Restart();
            //var norm = new LinearNorm1();
            var norm = new LinearNorm2();
            norm.Execute(data);
            sw.Stop();
            Console.WriteLine($"Done! Elapsed: {sw.Elapsed}");
        }

        static float[] GenerateData(int count) {
            var random = new Random(42); // using same seed to generate same sequences
            var result = new float[count];
            for (int i = 0; i < count; i++)
                result[i] = (float)(random.NextDouble() * 300 + 100);
            return result;
        }
    }
}
