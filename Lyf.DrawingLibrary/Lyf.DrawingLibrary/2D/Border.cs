using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Lyf.DrawingLibrary.Common;

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

        #region 变量

        /// <summary>
        /// 矩形的放大系数
        /// </summary>
        private float _inflateFactor;

        #endregion

        #region Default Struct

        /// <summary>
        /// 一个定义 <see cref="Fill"/> 属性默认值的简单结构
        /// </summary>
        new public struct Default
        {
            /// <summary>
            /// <see cref="Border.InflateFactor"/> 的默认值，单位：点（1/72英寸）
            /// </summary>
            public static float InflateFactor = 0.0F;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置矩形的放大系数
        /// </summary>
        public float InflateFactor
        {
            get { return _inflateFactor; }
            set { _inflateFactor = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化默认的 Border
        /// </summary>
        public Border() : base()
        {
            _inflateFactor = Default.InflateFactor;
        }

        /// <summary>
        /// 初始化一个具有可见性的指定颜色和宽度的 Border
        /// </summary>
        /// <param name="isVisible"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public Border(bool isVisible, Color color, float width) : base(color)
        {
            base._width = width;
            base._isVisible = isVisible;
        }

        /// <summary>
        /// 根据指定颜色和宽度初始化 Border 实例
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public Border(Color color, float width) : this(!color.IsEmpty, color, width)
        {

        }

        /// <summary>
        /// 实现拷贝的构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public Border(Border lBase) : base(lBase)
        {
            this._inflateFactor = lBase._inflateFactor;
        }

        /// <summary>
        /// 用于反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected Border(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // 'schema' 值是文件版本号参数
            int sch = info.GetInt32(CommonConsts.SCHEMASTRING);

            this._inflateFactor = info.GetSingle("inflateFactor");
        }

        #endregion

        #region 函数

        /// <summary>
        /// 深度拷贝——克隆方法
        /// </summary>
        /// <returns></returns>
        public Border Clone()
        {
            return new Border(this);
        }

        /// <summary>
        /// 用于绘制长方形图形区域的边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pane"></param>
        /// <param name="scaleFactor"></param>
        /// <param name="rect"></param>
        public void Draw(Graphics g, PaneBase pane, float scaleFactor, RectangleF rect)
        {
            if (this._isVisible)
            {
                RectangleF tRect = rect;

                float scledInflate = (float)(this._inflateFactor * scaleFactor);
                tRect.Inflate(scaleFactor, scaleFactor);

                using (Pen pen = GetPen(pane, scledInflate))
                {
                    g.DrawRectangle(pen, tRect.X, tRect.Y, tRect.Width, tRect.Height);
                }

            }
        }

        #endregion

        #region ISerializable 接口实现

        /// <summary>
        /// 实现反序列化
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(CommonConsts.SCHEMASTRING, schema);
            info.AddValue("inflateFactor", _inflateFactor);
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
