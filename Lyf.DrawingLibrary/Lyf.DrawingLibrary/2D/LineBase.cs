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
    public class LineBase : ISerializable, ICloneable
    {
        #region 构造函数

        public LineBase(LineBase lBase)
        {

        }



        #endregion

        #region 变量

        internal float _width;

        internal DashStyle _style;

        internal float _dashOn;

        internal bool _isVisible;

        internal bool _isAntiAlias;

        internal Color _color;

        #endregion

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException("Can't clone an abstract base type -- child types must implement ICloneable");
        }

    }
}
