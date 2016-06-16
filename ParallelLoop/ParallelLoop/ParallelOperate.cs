using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLoop
{
    /// <summary>
    /// 执行并行操作
    /// </summary>
    public class ParallelOperate
    {
        /// <summary>
        /// 使用 Parallel 并行方式打开或关闭集合中的灯
        /// </summary>
        /// <param name="lights"></param>
        public static void UpdateLightsStatusParallel(List<Light> lights)
        {
            Parallel.ForEach(lights, light =>
                {
                    if (light.Status)
                    {
                        Operator.TurnOff(light);
                    }
                    else
                    {
                        Operator.TurnOn(light);
                    }
                });
        }

        /// <summary>
        /// 使用 PLINQ 并行方式打开或关闭集合中的灯
        /// </summary>
        /// <param name="lights"></param>
        public static void UpdateLightsStatusPLinq(List<Light> lights)
        {
            lights.AsParallel().ForAll(light =>
            {
                if (light.Status)
                {
                    Operator.TurnOff(light);
                }
                else
                {
                    Operator.TurnOn(light);
                }
            });
        }

    }
}
