using Lyf.DrawingLibrary.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 定义绘制图形区域内的每条曲线的数据和方法，如颜色、符号、大小和线型等
    /// </summary>
    [Serializable]
    abstract public class CurveItem : ISerializable, ICloneable
    {
        #region 常量

        /// <summary>
        /// 当前的 schema 值，用于定义序列化文件的版本号
        /// </summary>
        public const int SCHEMA = 11;

        #endregion

        #region 变量

        /// <summary>
        /// 用于定义是否在图形上显示 <see cref="CurveItem"/> 对象。通过
        /// 属性 <see cref="IsVisible"/> 访问
        /// </summary>
        protected bool _isVisible;

        #endregion

        #region 属性

        /// <summary>
        /// 用于定义是否在图形上显示 <see cref="CurveItem"/> 对象。
        /// 注意：该值仅用于控制曲线的显示与否，但不能控制图例的显示与否；
        /// 要隐藏图例，则需要设置 <see cref="Label.IsVisible"/> 为 false。
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        /// <summary>
        /// 获取一个标识，用于标识 X 坐标轴是否独立于 <see cref="CurveItem"/> 对象而存在
        /// </summary>
        /// <param name="pane"></param>
        /// <returns></returns>
        abstract internal bool IsXIndependent(GraphPane pane);

        #endregion

        #region 构造函数

        /// <summary>
        /// 克隆一个当前实例的一个副本（深度克隆）
        /// </summary>
        /// <param name="cItm"></param>
        public CurveItem(CurveItem cItm)
        {
            _isVisible = cItm.IsVisible;
        }

        /// <summary>
        /// 实现反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public CurveItem(SerializationInfo info, StreamingContext context)
        {
            int sch = info.GetInt32(CommonConsts.SCHEMASTRING);
            
            _isVisible = info.GetBoolean("isVisible");
        }

        #endregion

        #region ICloneable 接口的实现

        object ICloneable.Clone()
        {
            throw new NotImplementedException("Can't clone an abstract base type -- child types must implement ICloneable");
        }

        #endregion

        #region ISerializable 接口的实现

        /// <summary>
        /// 实现反序列化
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CommonConsts.SCHEMASTRING, SCHEMA);
            info.AddValue("isVisible", _isVisible);
        }

        #endregion

        #region 抽象函数

        /// <summary>
        /// 在指定的 <see cref="Graphics"/> 设备中对显示在上面的 <see cref="CurveItem"/> 对象进行渲染
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pane"></param>
        /// <param name="pos"></param>
        /// <param name="scaleFactor"></param>
        abstract public void Draw(Graphics g, GraphPane pane, int pos, float scaleFactor);

        #endregion


    }
}
