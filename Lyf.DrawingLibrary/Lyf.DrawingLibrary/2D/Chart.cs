using Lyf.DrawingLibrary.Common;
using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Lyf.DrawingLibrary._2D
{
    [Serializable]
    public class Chart : ICloneable, ISerializable
    {
        #region 常量

        /// <summary>
        /// 当前的 schema 值用于定义序列化文件的版本号
        /// </summary>
        public const int SCHEMA = 10;

        #endregion

        #region 变量

        /// <summary>
        /// 通过坐标轴限定图形区域的矩形
        /// </summary>
        internal RectangleF _rect;


        /// <summary>
        /// 用于存储图形区域的 Border 的数值，通过属性 <see cref="Border"/> 访问
        /// </summary>
        internal Border _border;

        /// <summary>
        /// 用于定义 <see cref="Rect"/> 是否自动调整大小，通过属性 <see cref="IsRectAuto"/> 访问
        /// </summary>
        internal bool _isRectAuto;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置由坐标轴（<see cref="XAxis"/>、<see cref="YAxis"/>、<see cref="Y2Axis"/>）限定的包含图
        /// 形区域边界的长方形区域。如果将该值设置为手工方式获取，则 <see cref="IsRectAuto"/> 将被自动设置为 false
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set
            {
                _rect = value;
                _isRectAuto = false;
            }
        }

        /// <summary>
        /// 获取或设置图形区域的边框值
        /// </summary>
        public Border Border
        {
            get { return _border; }
            set { _border = value; }
        }

        /// <summary>
        /// 获取或设置一个布尔值，该值用于定义是否自动计算 Rect （绝大多数情况都是 true）
        /// </summary>
        public bool IsRectAuto
        {
            get { return _isRectAuto; }
            set { _isRectAuto = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Chart()
        {
            this._isRectAuto = true;
            this._border = new Border(Default.IsBorderVisible, Default.BorderColor, Default.BorderPenWidth);
        }

        /// <summary>
        /// 克隆一个当前实例的一个副本（深度克隆）
        /// </summary>
        /// <param name="chart"></param>
        public Chart(Chart chart)
        {
            _border = chart._border.Clone();
            _rect = chart._rect;
            _isRectAuto = chart._isRectAuto;
        }

        /// <summary>
        /// 实现反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Chart(SerializationInfo info, StreamingContext context)
        {
            int sch = info.GetInt32(CommonConsts.SCHEMASTRING);

            _rect = (RectangleF)info.GetValue("rect", typeof(RectangleF));
            _border = (Border)info.GetValue("border", typeof(Border));
            _isRectAuto = info.GetBoolean("isRectAuto");
        }
        
        #endregion

        #region ICloneable 接口实现

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region ISerializable 接口实现

        /// <summary>
        /// 实现反序列化
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CommonConsts.SCHEMASTRING, SCHEMA);
            info.AddValue("rect", _rect);
            info.AddValue("border", _border);
            info.AddValue("isRectAuto", _isRectAuto);
        }

        #endregion

        #region 函数

        /// <summary>
        /// 实现深度克隆
        /// </summary>
        /// <returns></returns>
        public Chart Clone()
        {
            return new Chart(this);
        }

        #endregion

        #region Defaults Struct

        /// <summary>
        /// 用于定义 Chart 的默认值的结构
        /// </summary>
        public struct Default
        {
            /// <summary>
            /// <see cref="Chart"/> 边框的默认颜色
            /// (<see cref="Chart.Border"/> 属性). 
            /// </summary>
            public static Color BorderColor = Color.Black;

            /// <summary>
            /// 绘制 <see cref="GraphPane.Chart"/> 边框的默认笔宽
            /// (<see cref="Chart.Border"/> 属性).
            /// 单位：点 (1/72 英寸).
            /// </summary>
            public static float BorderPenWidth = 1F;

            /// <summary>
            /// 定义默认显示 <see cref="Chart"/> 边框
            /// (<see cref="Chart.Border"/> 属性).
            /// </summary>
            public static bool IsBorderVisible = true;

        }

        #endregion

    }
}
