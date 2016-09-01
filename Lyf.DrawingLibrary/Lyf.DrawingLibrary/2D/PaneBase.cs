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

        protected RectangleF _rect;

        internal Margin _margin;

        protected bool _isPenWidthScaled;

        protected Border _border;

        protected float _baseDimension;

        #endregion

        #region 属性

        public RectangleF Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public Border Border
        {
            get { return _border; }
            set { _border = value; }
        }

        public Margin Margin
        {
            get { return _margin; }
            set { _margin = value; }
        }

        public float BaseDimension
        {
            get { return _baseDimension; }
            set { _baseDimension = value; }
        }

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

        public virtual void Draw(Graphics g)
        {
            if (_rect.Width <= 1 || _rect.Height <= 1)
                return;

            // calculate scaleFactor on "normal" pane size (BaseDimension)
            float scaleFactor = this.CalcScaleFactor();

            // Fill the pane background and draw a border around it			
            DrawPaneFrame(g, scaleFactor);

            // Clip everything to the rect
            g.SetClip(_rect);
            
            // Reset the clipping
            g.ResetClip();
        }

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

        public void DrawPaneFrame(Graphics g, float scaleFactor)
        {

            // Reduce the rect width and height by 1 pixel so that for a rect of
            // new RectangleF( 0, 0, 100, 100 ), which should be 100 pixels wide, we cover
            // from 0 through 99.  The draw routines normally cover from 0 through 100, which is
            // actually 101 pixels wide.
            RectangleF rect = new RectangleF(_rect.X, _rect.Y, _rect.Width - 1, _rect.Height - 1);

            _border.Draw(g, this, scaleFactor, rect);
        }

        public virtual void ReSize(Graphics g, RectangleF rect)
        {
            _rect = rect;
        }

        public float CalcScaleFactor()
        {
            float scaleFactor; //, xInch, yInch;
            const float ASPECTLIMIT = 1.5F;
            
            // Assume the standard width (BaseDimension) is 8.0 inches
            // Therefore, if the rect is 8.0 inches wide, then the fonts will be scaled at 1.0
            // if the rect is 4.0 inches wide, the fonts will be half-sized.
            // if the rect is 16.0 inches wide, the fonts will be double-sized.

            // Scale the size depending on the client area width in linear fashion
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
            if (scaleFactor < 0.1F)
                scaleFactor = 0.1F;

            return scaleFactor;
        }

        public float ScaledPenWidth(float penWidth, float scaleFactor)
        {
            if (_isPenWidthScaled) return penWidth * scaleFactor;
            else return penWidth;
        }

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
