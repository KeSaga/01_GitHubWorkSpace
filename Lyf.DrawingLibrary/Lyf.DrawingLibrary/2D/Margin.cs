using Lyf.DrawingLibrary.Common;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Lyf.DrawingLibrary._2D
{
    [Serializable]
    public class Margin : ICloneable, ISerializable
    {
        #region 常量

        public const int SCHEMA = 10;

        #endregion

        #region 变量

        protected float _left, _right, _top, _bottom;

        #endregion

        #region 属性

        /// <summary>
        /// Gets or sets a float value that determines the margin area between the left edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Left"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        /// <seealso cref="Bottom"/>
        public float Left
        {
            get { return _left; }
            set { _left = value; }
        }
        /// <summary>
        /// Gets or sets a float value that determines the margin area between the right edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Right"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Top"/>
        /// <seealso cref="Bottom"/>
        public float Right
        {
            get { return _right; }
            set { _right = value; }
        }
        /// <summary>
        /// Gets or sets a float value that determines the margin area between the top edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Top"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Bottom"/>
        public float Top
        {
            get { return _top; }
            set { _top = value; }
        }
        /// <summary>
        /// Gets or sets a float value that determines the margin area between the bottom edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Bottom"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        public float Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        /// <summary>
        /// Concurrently sets all outer margin values to a single value.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Bottom"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        public float All
        {
            set
            {
                _bottom = value;
                _top = value;
                _left = value;
                _right = value;
            }
        }
        
        #endregion

        #region 构造函数

        public Margin()
        {
            _left = Default.Left;
            _right = Default.Right;
            _top = Default.Top;
            _bottom = Default.Bottom;
        }

        public Margin(Margin rhs)
        {
            _left = rhs._left;
            _right = rhs._right;
            _top = rhs._top;
            _bottom = rhs._bottom;
        }

        protected Margin(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32(CommonConsts.SCHEMASTRING);

            _left = info.GetSingle("left");
            _right = info.GetSingle("right");
            _top = info.GetSingle("top");
            _bottom = info.GetSingle("bottom");
        }

        #endregion

        #region 函数

        public Margin Clone()
        {
            return new Margin(this);
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CommonConsts.SCHEMASTRING, SCHEMA);

            info.AddValue("left", _left);
            info.AddValue("right", _right);
            info.AddValue("top", _top);
            info.AddValue("bottom", _bottom);
        }

        #endregion

        #region ICloneable 接口的实现

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Defaults Struct

        /// <summary>
        /// 用于定义 <see cref="Margin"/> 的默认值的结构
        /// </summary>
        public class Default
        {
            /// <summary>
            /// The default value for the <see cref="Margin.Left"/> property, which is
            /// the size of the space on the left side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Left = 10.0F;
            /// <summary>
            /// The default value for the <see cref="Margin.Right"/> property, which is
            /// the size of the space on the right side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Right = 10.0F;
            /// <summary>
            /// The default value for the <see cref="Margin.Top"/> property, which is
            /// the size of the space on the top side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Top = 10.0F;
            /// <summary>
            /// The default value for the <see cref="Margin.Bottom"/> property, which is
            /// the size of the space on the bottom side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Bottom = 10.0F;

        }

        #endregion

    }
}
