using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// <see cref="CurveItem"/> 对象的集合类
    /// </summary>
    [Serializable]
    public class CurveList : List<CurveItem>, ICloneable
    {
        #region 构造函数

        /// <summary>
        /// 初始化一个默认的 CurveList 实例
        /// </summary>
        public CurveList()
        {
        }

        /// <summary>
        /// 克隆一个当前实例的一个副本（深度克隆）
        /// </summary>
        /// <param name="cList"></param>
        public CurveList(CurveList cList)
        {
            foreach (CurveItem itm in cList)
            {
                this.Add((CurveItem)((ICloneable)itm).Clone());
            }
        }

        #endregion

        #region ICloneable 接口的实现

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region 函数

        /// <summary>
        /// 实现深度克隆
        /// </summary>
        /// <returns></returns>
        public CurveList Clone()
        {
            return new CurveList(this);
        }

        #endregion

    }
}
