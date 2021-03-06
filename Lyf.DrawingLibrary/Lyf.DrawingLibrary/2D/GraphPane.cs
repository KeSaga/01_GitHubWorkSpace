﻿using Lyf.DrawingLibrary.Common;
using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 继承自 PaneBase ，用于封装绘图区域上所有的显示元素，提供接口访问图表的属性。
    /// 可以在同一文档的多个图表中实例化多个 <see cref="GraphPane"/> 对象。
    /// </summary>
    [Serializable]
    public class GraphPane : PaneBase, ICloneable, ISerializable
    {
        #region 常量

        public const int SCHEMA_GP = 11;

        #endregion

        #region 变量

        internal Chart _chart;
        /// <summary>
        /// 用于定义当定义 Y 或 Y2 坐标轴比例范围时是否包含或排除了零值
        /// </summary>
        private bool _isIgnoreInitial;
        /// <summary>
        /// 用于定义是否初始化了 <see cref="PointPairBase.Missing"/> 值，该值将影响组成曲线的线段能否连续出现。
        /// 如果被设置为 true ，那么曲线将被绘制为连续的线段，即使存在 Missing 值。
        /// </summary>
        private bool _isIgnoreMissing;
        /// <summary>
        /// 用于定义是否将在坐标轴的自动比例值范围内基于“人工”设置的比例值范围添加新的数值点。
        /// 如：坐标轴的比例值范围时 50 ~ 100，按照自动比例值的方法是以 10 为间隔设置比例点，
        /// 当设置该值为 true 时，将按照你设定的比例点间隔范围添加数据点（如设定的比例间隔为 5）
        /// </summary>
        private bool _isBoundedRanges;

        #endregion

        #region Defaults Struct

        /// <summary>
        /// 用于定义 GraphPane 的默认值的简单结构
        /// </summary>
        public new struct Default
        {
            public static bool IsIgnoreInitial = true;
        }

        #endregion

        #region 属性

        public Chart Chart
        {
            get { return _chart; }
        }
        /// <summary>
        /// 获取或设置一个值，该值用来指示数据范围是否采用自动标尺
        /// </summary>
        public bool IsIgnoreInitial
        {
            get { return _isIgnoreInitial; }
            set { _isIgnoreInitial = value; }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示自动缩放范围是否基于任何手动设定的比例范围
        /// </summary>
        public bool IsBoundedRanges
        {
            get { return _isBoundedRanges; }
            set { _isBoundedRanges = value; }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示是否初始化 <see cref="PointPairBase.Missing"/> 值，以定义曲线是否连续
        /// </summary>
        public bool IsIgnoreMissing
        {
            get { return _isIgnoreMissing; }
            set { _isIgnoreMissing = value; }
        }

        #endregion

        #region 构造函数

        public GraphPane(RectangleF rect) : base(rect)
        {
            _isIgnoreInitial = Default.IsIgnoreInitial;
            _chart = new Chart();
        }

        /// <summary>
        /// 复制一个 GraphPane 对象（实现拷贝）
        /// </summary>
        /// <param name="gPane"></param>
        public GraphPane(GraphPane gPane) : base(gPane)
        {
            _isIgnoreInitial = gPane.IsIgnoreInitial;
            _isBoundedRanges = gPane.IsBoundedRanges;
            _chart = gPane.Chart.Clone();
        }
        /// <summary>
        /// 用于反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected GraphPane(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");
            _chart = (Chart)info.GetValue("chart", typeof(Chart));


            _isIgnoreInitial = info.GetBoolean("isIgnoreInitial");
            _isBoundedRanges = info.GetBoolean("isBoundedRanges");
            _isIgnoreMissing = info.GetBoolean("isIgnoreMissing");

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
        /// 深度拷贝的函数
        /// </summary>
        /// <returns></returns>
        public GraphPane Clone()
        {
            return new GraphPane(this);
        }
        /// <summary>
        /// 实现反序列化的函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(CommonConsts.SCHEMASTRING, SCHEMA_GP);

            info.AddValue("chart", _chart);
            info.AddValue("isIgnoreInitial", _isIgnoreInitial);
            info.AddValue("isBoundedRanges", _isBoundedRanges);
            info.AddValue("isIgnoreMissing", _isIgnoreMissing);
        }
        /// <summary>
        /// 该函数用于当坐标轴的比例范围发生变化时，基于当前的数据范围重新计算坐标轴的比例范围
        /// </summary>
        public void AxisChange()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                AxisChange(g);
        }
        /// <summary>
        /// 该函数用于当坐标轴的比例范围发生变化时，基于当前的数据范围重新计算坐标轴的比例范围
        /// </summary>
        /// <param name="g"></param>
        public void AxisChange(Graphics g)
        {
            float scaleFactor = this.CalcScaleFactor();

            if (_chart._isRectAuto)
            {
                _chart._rect = CalcChartRect(g);
            }
        }

        public override void Draw(Graphics g)
        {

            base.Draw(g);

            if (_rect.Width <= 1 || _rect.Height <= 1) return;

            // 将剪辑区设置为 RectangleF 类型结构指定的矩形
            g.SetClip(_rect);

            // 定义比例因子
            float scaleFactor = this.CalcScaleFactor();


            // if the size of the ChartRect is determined automatically, then do so
            // otherwise, calculate the legendrect, scalefactor, hstack, and legendwidth parameters
            // but leave the ChartRect alone
            // 如果 ChartRect 设置为自动，则根据比例因子重新计算，否则将不修改 Chart Rect 的值
            if (_chart._isRectAuto) _chart._rect = CalcChartRect(g, scaleFactor);
            else CalcChartRect(g, scaleFactor);

            // 检测 Chart Rect 的完整性
            if (_chart._rect.Width < 1 || _chart._rect.Height < 1) return;

            // 设置边框
            _chart.Border.Draw(g, this, scaleFactor, _chart._rect);

            // 重置剪辑区
            g.ResetClip();

        }

        /// <summary>
        /// 基于 <see cref="PaneBase.Rect"/> 计算 <see cref="Chart.Rect"/>
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public RectangleF CalcChartRect(Graphics g)
        {
            return CalcChartRect(g, CalcScaleFactor());
        }
        /// <summary>
        /// 基于 <see cref="PaneBase.Rect"/> 计算 <see cref="Chart.Rect"/>
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public RectangleF CalcChartRect(Graphics g, float scaleFactor)
        {
            // 指定绘图区域的大小为界于图板边界和图板标题之间
            RectangleF clientRect = base.CalcClientRect(g, scaleFactor);
            
            // 绘图区域的左边界的最小坐标轴空间
            float minSpaceL = 0;
            // 绘图区域的右边界的最小坐标轴空间
            float minSpaceR = 0;
            // 绘图区域的底边界的最小坐标轴空间
            float minSpaceB = 0;
            // 绘图区域的上边界的最小坐标轴空间
            float minSpaceT = 0;

            float spaceB = 0, spaceT = 0;

            float totSpaceL = 0;
            float totSpaceR = 0;

            RectangleF tmpRect = clientRect;

            totSpaceL = Math.Max(totSpaceL, minSpaceL);
            totSpaceR = Math.Max(totSpaceR, minSpaceR);
            spaceB = Math.Max(spaceB, minSpaceB);
            spaceT = Math.Max(spaceT, minSpaceT);

            tmpRect.X += totSpaceL;
            tmpRect.Width -= totSpaceL + totSpaceR;
            tmpRect.Height -= spaceT + spaceB;
            tmpRect.Y += spaceT;

            return tmpRect;
        }

        private void SetSpace(float clientSize, ref float spaceNorm, ref float spaceAlt)
        {
            float crossPix = clientSize;
        }

        #endregion

    }
}
