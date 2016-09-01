using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 定义线段的基本属性
    /// </summary>
    [Serializable]
    abstract public class LineBase : ISerializable, ICloneable
    {
        #region 变量

        /// <summary>
        /// 画笔宽度
        /// 通过属性 <see cref="Width"/> 访问该值
        /// </summary>
        internal float _width;
        /// <summary>
        /// 画笔类型
        /// 通过属性 <see cref="Style"/> 访问该值
        /// </summary>
        internal DashStyle _style;
        /// <summary>
        /// 存储 Dash On 属性的值
        /// 通过属性 <see cref="DashOn"/> 访问该值
        /// </summary>
        internal float _dashOn;
        /// <summary>
        /// 存储 Dash Off 属性的值.
        /// 通过属性 <see cref="DashOff"/> 访问该值
        /// </summary>
        internal float _dashOff;
        /// <summary>
        /// 定义是否显示线段
        /// 通过属性 <see cref="IsVisible"/> 访问该值
        /// </summary>
        internal bool _isVisible;
        /// <summary>
        /// 是否对线段启用反锯齿功能
        /// 通过属性 <see cref="IsAntiAlias"/> 访问该值
        /// </summary>
        internal bool _isAntiAlias;
        /// <summary>
        /// 线段颜色
        /// 通过属性 <see cref="Color"/> 访问该值
        /// </summary>
        internal Color _color;

        #endregion

        #region 常量

        public const int SCHEMA_0 = 12;

        #endregion

        #region Default Struct

        /// <summary>
        /// 用于定义 <see cref="LineBase"/> 类的默认属性值的简单结构
        /// </summary>
        public struct Default
        {
            /// <summary>
            /// 默认显示线段
            /// </summary>
            public static bool IsVisible = true;
            /// <summary>
            /// 默认宽度 1
            /// </summary>
            public static float Width = 1;
            /// <summary>
            /// 抗锯齿设置默认为 false
            /// </summary>
            public static bool IsAntiAlias = false;
            /// <summary>
            /// 画笔类型默认为 Solid
            /// </summary>
            public static DashStyle Style = DashStyle.Solid;
            /// <summary>
            /// 线段的 Dash On 的默认尺寸：1.0F，单位：点（1/72英寸）
            /// </summary>
            public static float DashOn = 1.0F;
            /// <summary>
            /// 线段的 Dash Off 的默认尺寸：1.0F，单位：点（1/72英寸）
            /// </summary>
            public static float DashOff = 1.0F;
            /// <summary>
            /// 线段的默认颜色：黑色
            /// </summary>
            public static Color Color = Color.Black;

        }

        #endregion

        #region 属性

        /// <summary>
        /// 绘制 <see cref="Line"/> 的笔宽，单位：点（1/72 英寸）
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// <see cref="Line"/> 的样式
        /// </summary>
        public DashStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }
        /// <summary>
        /// 绘制线段时的 Dash On 模式
        /// </summary>
        public float DashOn
        {
            get { return _dashOn; }
            set { _dashOn = value; }
        }
        /// <summary>
        /// 绘制线段时的 DashOff 模式
        /// </summary>
        public float DashOff
        {
            get { return _dashOff; }
            set { _dashOff = value; }
        }
        /// <summary>
        /// 显示或隐藏线段
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }
        /// <summary>
        /// 获取或设置一个值，用于定义绘制线段时是否启用反锯齿功能
        /// </summary>
        public bool IsAntiAlias
        {
            get { return _isAntiAlias; }
            set { _isAntiAlias = value; }
        }
        /// <summary>
        /// <see cref="Line"/> 的颜色。
        /// 注意这个颜色将被覆盖，如果
        /// <see cref="Fill.Type">GradientFill.Type</see> 是如下类型之一：
        /// <see cref="FillType.GradientByX"/> ,
        /// <see cref="FillType.GradientByY"/> ,
        /// <see cref="FillType.GradientByZ"/> ,
        /// 和 <see cref="FillType.GradientByColorValue"/> .
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 通过 <see cref="Default"/> 初始化具有默认值的 LineBase 的一个实例
        /// </summary>
        public LineBase() : this(Color.Empty)
        {
        }

        /// <summary>
        /// 通过指定的 color 及 <see cref="Default"/> 类指定的默认值初始化一个 LineBase 实例
        /// </summary>
        /// <param name="color"></param>
        public LineBase(Color color)
        {
            _width = Default.Width;
            _style = Default.Style;
            _dashOn = Default.DashOn;
            _dashOff = Default.DashOff;
            _isVisible = Default.IsVisible;
            _isAntiAlias = Default.IsAntiAlias;
            _color = Default.Color;
        }

        /// <summary>
        /// 复制一个 LineBase 对象（实现拷贝）
        /// </summary>
        /// <param name="lBase"></param>
        public LineBase(LineBase lBase)
        {
            _width = lBase._width;
            _style = lBase._style;
            _dashOn = lBase._dashOn;
            _dashOff = lBase._dashOff;
            _isVisible = lBase._isVisible;
            _isAntiAlias = lBase._isAntiAlias;
            _color = lBase._color;
        }

        /// <summary>
        /// 用于反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LineBase(SerializationInfo info, StreamingContext context)
        {
            int sch = info.GetInt32("SCHEMA_0");

            _width = info.GetSingle("width");
            _style = (DashStyle)info.GetValue("style", typeof(DashStyle));
            _dashOn = info.GetSingle("dashOn");
            _dashOff = info.GetSingle("dashOff");
            _isVisible = info.GetBoolean("isVisible");
            _isAntiAlias = info.GetBoolean("isAntiAlias");
            _color = (Color)info.GetValue("color", typeof(Color));
        }

        #endregion

        #region 实现接口 ISerializable

        /// <summary>
        /// 反序列化的函数方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SCHEMA_0", SCHEMA_0);

            info.AddValue("width", _width);
            info.AddValue("style", _style);
            info.AddValue("dashOn", _dashOn);
            info.AddValue("dashOff", _dashOff);
            info.AddValue("isVisible", _isVisible);
            info.AddValue("isAntiAlias", _isAntiAlias);
            info.AddValue("color", _color);
        }

        #endregion

        #region 实现接口 ICloneable

        object ICloneable.Clone()
        {
            throw new NotImplementedException("Can't clone an abstract base type -- child types must implement ICloneable");
        }

        #endregion

        #region 函数

        /// <summary>
        /// 基于 <see cref="LineBase"/> 的属性创建一个 <see cref="Pen"/> 对象
        /// </summary>
        /// <param name="pane"></param>
        /// <param name="scaleFacor"></param>
        /// <returns></returns>
        public Pen GetPen(PaneBase pane, float scaleFacor)
        {
            Color color = _color;

            Pen pen = new Pen(color, pane.ScaledPenWidth(_width, scaleFacor));

            pen.DashStyle = _style;

            // 1.0 x 10 的 -10 次方 = 0.0000000001
            double d1e_10 = 1e-10;

            if (_style == DashStyle.Custom)
            {
                if (_dashOff > d1e_10 && _dashOn > d1e_10)
                {
                    pen.DashStyle = DashStyle.Custom;
                    float[] pattern = new float[2];
                    pattern[0] = _dashOn;
                    pattern[1] = _dashOff;
                    pen.DashPattern = pattern;
                }
                else
                {
                    pen.DashStyle = DashStyle.Solid;
                }
            }

            return pen;
        }

        #endregion

    }
}
