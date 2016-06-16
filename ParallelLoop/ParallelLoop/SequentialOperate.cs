using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLoop
{
    /// <summary>
    /// 执行顺序操作
    /// </summary>
    public class SequentialOperate
    {
        public static void UpdateLightsStatusSequential(List<Light> lights)
        {
            foreach(Light light in lights)
            {
                if (light.Status)
                {
                    Operator.TurnOff(light);
                }
                else
                {
                    Operator.TurnOn(light);
                }
            }
        }
    }
}
