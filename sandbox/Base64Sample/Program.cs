using System;
using BenchmarkDotNet.Running;

namespace Base64Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(BenchmarkTarget)
            });

            switcher.Run(args);
        }
    }
}
