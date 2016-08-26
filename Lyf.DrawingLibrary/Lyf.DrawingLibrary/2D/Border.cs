using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 图形对象边框类
    /// 功能：实现用于绘制各种图表对象边框的功能
    /// </summary>
    [Serializable]
    public class Border : LineBase, ISerializable, ICloneable
    {
        #region 常量

        public const int schema = 11;


        #endregion

        #region 构造函数

        /// <summary>
        /// 实现拷贝
        /// </summary>
        /// <param name="rhs"></param>
        public Border(Border lBase) : base(lBase)
        {
        }

        #endregion

        #region 函数

        public Border Clone()
        {
            return new Border(this);
        }

        #endregion


        #region ISerializable 接口实现

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("schema", schema);
        }

        #endregion

        #region ICloneable 接口实现

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

    }
}
