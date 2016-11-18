using Lyf.DrawingLibrary.Common;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 抽象基类，定义处理图形窗体的基本功能
    /// </summary>
    abstract public class PaneBase : ICloneable
    {
        #region 常量

        /// <summary>
        /// 当前的 schema 值，用于定义序列化文件的版本号
        /// </summary>
        public const int SCHEMA = 10;

        #endregion

        #region 变量

        /// <summary>
        /// 定义被渲染图板区域内的区域面积，单位是像素
        /// </summary>
        protected RectangleF _rect;
        /// <summary>Private field that holds the main title of the pane.  Use the
        /// public property <see cref="Title"/> to access this value.
        /// </summary>
        protected GapLabel _title;
        /// <summary>
        /// Private field that stores the user-defined tag for this <see cref="PaneBase"/>.  This tag
        /// can be any user-defined value.  If it is a <see cref="String"/> type, it can be used as
        /// a parameter to the <see cref="PaneList.IndexOfTag"/> method.  Use the public property
        /// <see cref="Tag"/> to access this value.
        /// </summary>
        protected object _tag;
        /// <summary>
        /// 用于存储 <see cref="PaneBase"/> 的边界值
        /// </summary>
        internal Margin _margin;
        /// <summary>
        /// 用于控制笔宽是否根据图形区域的尺寸进行合适的缩放。
        /// 如果 <see cref="IsFontsScaled"/> 为 false 则将忽
        /// 略 <see cref="IsPenWidthScaled"/> 的值，否则，将
        /// 允许缩放。
        /// </summary>
        protected bool _isPenWidthScaled;
        /// <summary>
        /// 用于为 <see cref="Rect"/> 的边界存储 <see cref="Border"/> 数据
        /// </summary>
        protected Border _border;
        /// <summary>
        /// 用于定义图板区域的基础尺寸。字体、刻度、间隙等都将按此进行缩放
        /// </summary>
        protected float _baseDimension;

        #endregion

        #region 属性

        /// <summary>
        /// 定义被渲染图板区域内的区域面积，单位是像素
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        /// <summary>
        /// Gets the <see cref="Label" /> instance that contains the text and attributes of the title.
        /// This text can be multiple lines separated by newline characters ('\n').
        /// </summary>
        /// <seealso cref="FontSpec"/>
        /// <seealso cref="Default.FontColor"/>
        /// <seealso cref="Default.FontBold"/>
        /// <seealso cref="Default.FontItalic"/>
        /// <seealso cref="Default.FontUnderline"/>
        /// <seealso cref="Default.FontFamily"/>
        /// <seealso cref="Default.FontSize"/>
        public Label Title
        {
            get { return _title; }
        }

        /// <summary>
        /// Gets or sets the user-defined tag for this <see cref="PaneBase"/>.  This tag
        /// can be any user-defined value.  If it is a <see cref="String"/> type, it can be used as
        /// a parameter to the <see cref="PaneList.IndexOfTag"/> method.
        /// </summary>
        /// <remarks>
        /// Note that, if you are going to Serialize Example data, then any type
        /// that you store in <see cref="Tag"/> must be a serializable type (or
        /// it will cause an exception).
        /// </remarks>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// 获取或设置 <see cref="Border"/> 类的实例，以便围绕 <see cref="Rect"/> 绘制边框
        /// </summary>
        public Border Border
        {
            get { return _border; }
            set { _border = value; }
        }
        /// <summary>
        /// 获取或设置 <see cref="Margin"/> 实例，以便控制 <see cref="PaneBase.Rect"/> 和图表上渲染内容间的空余空间
        /// </summary>
        public Margin Margin
        {
            get { return _margin; }
            set { _margin = value; }
        }
        /// <summary>
        /// 用于定义图板区域的基础尺寸。字体、刻度、间隙等都将按此进行缩放
        /// </summary>
        public float BaseDimension
        {
            get { return _baseDimension; }
            set { _baseDimension = value; }
        }
        /// <summary>
        /// 获取或设置一个布尔值，用于控制笔宽是否根据图形区域的尺寸进行缩放
        /// </summary>
        public bool IsPenWidthScaled
        {
            get { return _isPenWidthScaled; }
            set { _isPenWidthScaled = value; }
        }

        #endregion

        #region 构造函数

        public PaneBase(RectangleF paneRect)
        {
            _rect = paneRect;

            _baseDimension = Default.BaseDimension;
            _margin = new Margin();

            _isPenWidthScaled = Default.IsPenWidthScaled;
            _border = new Border(Default.IsBorderVisible, Default.BorderColor, Default.BorderPenWidth);

        }

        public PaneBase(PaneBase pBase)
        {
            _isPenWidthScaled = pBase.IsPenWidthScaled;
            _margin = pBase.Margin;
            _rect = pBase.Rect;
            _border = pBase.Border;

        }

        public PaneBase(SerializationInfo info, StreamingContext context)
        {
            int sch = info.GetInt32(CommonConsts.SCHEMASTRING);

            _rect = (RectangleF)info.GetValue("rect", typeof(RectangleF));
            _isPenWidthScaled = info.GetBoolean("isPenWidthScaled");
            _border = (Border)info.GetValue("border", typeof(Border));
            _baseDimension = info.GetSingle("baseDimension");
            _margin = (Margin)info.GetValue("margin", typeof(Margin));
        }

        #endregion

        #region ICloneable 接口的实现

        object ICloneable.Clone()
        {
            throw new NotImplementedException("Can't clone an abstract base type -- child types must implement ICloneable");
        }

        #endregion

        #region 函数

        /// <summary>
        /// 创建当前实例的一个浅拷贝
        /// </summary>
        /// <returns></returns>
        public PaneBase ShallowClone()
        {
            return this.MemberwiseClone() as PaneBase;
        }

        /// <summary>
        /// 反序列化的函数方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CommonConsts.SCHEMASTRING, SCHEMA);

            info.AddValue("rect", _rect);
            info.AddValue("isPenWidthScaled", _isPenWidthScaled);
            info.AddValue("border", _border);
            info.AddValue("baseDimension", _baseDimension);
            info.AddValue("margin", _margin);
        }

        /// <summary>
        /// 在指定的 <see cref="Graphics"/> 设备中实现与 <see cref="PaneBase"/> 相关联的渲染操作。
        /// 具体逻辑将通过其子类实现
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        {
            if (_rect.Width <= 1 || _rect.Height <= 1)
                return;

            // calculate scaleFactor on "normal" pane size (BaseDimension)
            // 在“普通”模式的图板区域尺寸上计算“scaleFactor”
            float scaleFactor = this.CalcScaleFactor();

            // Fill the pane background and draw a border around it
            // 填充图板背景并绘制环绕它的边框
            DrawPaneFrame(g, scaleFactor);

            // Clip everything to the rect
            // 剪切所有元素到“rect”中
            g.SetClip(_rect);

            // Reset the clipping
            // 重置剪切板
            g.ResetClip();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="scaleFactor"></param>
        /// <returns></returns>
        public RectangleF CalcScaleFactor(Graphics g, float scaleFactor)
        {
            // chart rect starts out at the full pane rect.  It gets reduced to make room for the legend,
            // scales, titles, etc.
            RectangleF innerRect = new RectangleF(
                            _rect.Left + _margin.Left * scaleFactor,
                            _rect.Top + _margin.Top * scaleFactor,
                            _rect.Width - scaleFactor * (_margin.Left + _margin.Right),
                            _rect.Height - scaleFactor * (_margin.Top + _margin.Bottom));

            return innerRect;
        }

        /// <summary>
        /// 绘制环绕 <see cref="Rect"/> 区域的边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="scaleFactor"></param>
        public void DrawPaneFrame(Graphics g, float scaleFactor)
        {

            // Reduce the rect width and height by 1 pixel so that for a rect of
            // new RectangleF( 0, 0, 100, 100 ), which should be 100 pixels wide, we cover
            // from 0 through 99.  The draw routines normally cover from 0 through 100, which is
            // actually 101 pixels wide.
            // 对于 'Rect' 的宽和高，都要减少一个像素，因此对于这样一个长方形
            // 'new RectangleF(0,0,100,100)' 需要具有 100 像素的宽，
            // 我们是从 0 到 99 进行处理的。而对于正常的绘制过程是从 0 到 100 进行处理的，也就是实际宽度为 101 像素
            RectangleF rect = new RectangleF(_rect.X, _rect.Y, _rect.Width - 1, _rect.Height - 1);

            _border.Draw(g, this, scaleFactor, rect);
        }

        /// <summary>
        /// 重置 <see cref="Rect"/> 的大小。可重载该虚函数，以便根据具体情况实现相应的重置算法
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        public virtual void ReSize(Graphics g, RectangleF rect)
        {
            _rect = rect;
        }

        /// <summary>
        /// 基于当前 <see cref="Rect"/> 的大小与 <see cref="Default.BaseDimension"/> 的比例来计算比例因数
        /// </summary>
        /// <returns></returns>
        public float CalcScaleFactor()
        {
            float scaleFactor; //, xInch, yInch;
            const float ASPECTLIMIT = 1.5F;

            // Assume the standard width (BaseDimension) is 8.0 inches
            // Therefore, if the rect is 8.0 inches wide, then the fonts will be scaled at 1.0
            // if the rect is 4.0 inches wide, the fonts will be half-sized.
            // if the rect is 16.0 inches wide, the fonts will be double-sized.
            // 假设标准宽度 'BaseDimension' 是 8.0 英寸
            // 因此，如果 'rect' 是 8.0 英寸，那么字体的缩放因子将是 1.0
            // 如果 'rect' 是 4.0 英寸，那么字体的缩放因子将是 0.5（标准值的一半）
            // 如果 'rect' 是 16.0 英寸，那么字体的缩放因子将是 2.0（标准值的两倍）

            // Scale the size depending on the client area width in linear fashion
            // 比例因子的大小取决于线性样式下客户区域的宽度
            if (_rect.Height <= 0)
                return 1.0F;
            float length = _rect.Width;
            float aspect = _rect.Width / _rect.Height;
            if (aspect > ASPECTLIMIT)
                length = _rect.Height * ASPECTLIMIT;
            if (aspect < 1.0F / ASPECTLIMIT)
                length = _rect.Width * ASPECTLIMIT;

            scaleFactor = length / (_baseDimension * 72F);

            // Don't let the scaleFactor get ridiculous
            // 避免 scaleFactor 超出合理范围
            if (scaleFactor < 0.1F)
                scaleFactor = 0.1F;

            return scaleFactor;
        }

        /// <summary>
        /// 计算笔宽的比例因子，引入 'scaleFactor' 参数，并且设置图板的 <see cref="IsPenWidthScaled"/> 属性
        /// </summary>
        /// <param name="penWidth"></param>
        /// <param name="scaleFactor"></param>
        /// <returns></returns>
        public float ScaledPenWidth(float penWidth, float scaleFactor)
        {
            if (_isPenWidthScaled) return penWidth * scaleFactor;
            else return penWidth;
        }

        /// <summary>
        /// 用适当的 '' 设置构建一个 <see cref="Graphics"/> 实例
        /// </summary>
        /// <param name="g"></param>
        /// <param name="isAntiAlias"></param>
        internal void SetAntiAliasMode(Graphics g, bool isAntiAlias)
        {
            if (isAntiAlias)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
        }

        /// <summary>
        /// 基于 <see cref="PaneBase.Rect"/> 计算客户区域长方形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="scaleFactor"></param>
        /// <returns></returns>
        public RectangleF CalcClientRect(Graphics g, float scaleFactor)
        {
            // chart rect starts out at the full pane rect.  It gets reduced to make room for the legend,
            // scales, titles, etc.
            // 客户区域长方形（即绘图区域）需留出一些空余用于放置图例、比例值、标题等
            RectangleF innerRect = new RectangleF(
                            _rect.Left + _margin.Left * scaleFactor,
                            _rect.Top + _margin.Top * scaleFactor,
                            _rect.Width - scaleFactor * (_margin.Left + _margin.Right),
                            _rect.Height - scaleFactor * (_margin.Top + _margin.Bottom));

            return innerRect;
        }

        #endregion

        #region Defaults Struct

        public struct Default
        {
            /// <summary>
            /// The default border mode for the <see cref="PaneBase"/>.
            /// (<see cref="PaneBase.Border"/> property). true
            /// to draw a border around the <see cref="PaneBase.Rect"/>,
            /// false otherwise.
            /// </summary>
            public static bool IsBorderVisible = true;
            /// <summary>
            /// The default color for the <see cref="PaneBase"/> border.
            /// (<see cref="PaneBase.Border"/> property). 
            /// </summary>
            public static Color BorderColor = Color.Black;

            /// <summary>
            /// The default pen width for the <see cref="PaneBase"/> border.
            /// (<see cref="PaneBase.Border"/> property).  Units are in points (1/72 inch).
            /// </summary>
            public static float BorderPenWidth = 1;

            /// <summary>
            /// The default dimension of the <see cref="PaneBase.Rect"/>, which
            /// defines a normal sized plot.  This dimension is used to scale the
            /// fonts, symbols, etc. according to the actual size of the
            /// <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <seealso cref="PaneBase.CalcScaleFactor"/>
            public static float BaseDimension = 8.0F;

            /// <summary>
            /// The default setting for the <see cref="PaneBase.IsPenWidthScaled"/> option.
            /// true to have all pen widths scaled according to <see cref="PaneBase.BaseDimension"/>,
            /// false otherwise.
            /// </summary>
            /// <seealso cref="PaneBase.CalcScaleFactor"/>
            public static bool IsPenWidthScaled = false;
        }

        #endregion

    }
}
