using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoop
{
    /// <summary>
    /// 电灯操作员
    /// </summary>
    public static class Operator
    {
        /// <summary>
        /// 打开灯
        /// </summary>
        /// <param name="light"></param>
        public static void TurnOn(Light light)
        {
            // 操作需要0.1秒
            Thread.Sleep(100);
            light.Status = true;
        }

        /// <summary>
        /// 关闭灯
        /// </summary>
        /// <param name="light"></param>
        public static void TurnOff(Light light)
        {
            // 操作需要0.1秒
            Thread.Sleep(100);
            light.Status = false;
        }

    }
}
