using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelLoop;
using System.Diagnostics;

namespace ParallelLoopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Light> lights = new List<Light>();
            for (int i = 0; i < 10; i++)
            {
                lights.Add(new Light());
            }

            Stopwatch stpWatch = new Stopwatch();

            stpWatch.Start();
            SequentialOperate.UpdateLightsStatusSequential(lights);
            stpWatch.Stop();
            Console.WriteLine("执行顺序操作耗时：" + stpWatch.ElapsedMilliseconds + " 毫秒");

            stpWatch.Restart();
            ParallelOperate.UpdateLightsStatusParallel(lights);
            stpWatch.Stop();
            Console.WriteLine("执行 Parallel 操作耗时：" + stpWatch.ElapsedMilliseconds + " 毫秒");

            stpWatch.Restart();
            ParallelOperate.UpdateLightsStatusPLinq(lights);
            stpWatch.Stop();
            Console.WriteLine("执行 PLINQ 操作耗时：" + stpWatch.ElapsedMilliseconds + " 毫秒");


            Console.ReadKey();
        }
    }
}
