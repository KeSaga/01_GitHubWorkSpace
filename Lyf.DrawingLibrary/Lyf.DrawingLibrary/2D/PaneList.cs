using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// 一个存储 <see cref="GraphPane"/> 对象的集合类
    /// </summary>
    public class PaneList : List<GraphPane>, ICloneable
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PaneList()
        {
        }

        /// <summary>
        /// 实现拷贝
        /// </summary>
        /// <param name="rhs">The <see cref="PaneList"/> object from which to copy</param>
        public PaneList(PaneList rhs)
        {
            foreach (GraphPane item in rhs)
            {
                this.Add(item.Clone());
            }
        }

        /// <summary>
        /// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
        /// calling the typed version of <see cref="Clone" />
        /// 调用 <see cref="Clone"/> 方法实现 <see cref="ICloneable"/> 接口
        /// </summary>
        /// <returns>A deep copy of this object</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// 深拷贝克隆方法
        /// </summary>
        /// <returns>返回当前对象的一个新实例</returns>
        public PaneList Clone()
        {
            return new PaneList(this);
        }


        #endregion

        #region Serialization
        /// <summary>
        /// 当前的 'schema' 值用于定义序列化文件的版本号
        /// </summary>
        public const int schema = 10;

        /// <summary>
        /// 用于反序列化的构造函数
        /// </summary>
        /// <param name="info">一个 <see cref="SerializationInfo"/> 的实例，用于定义序列化的数据</param>
        /// <param name="context">一个 <see cref="StreamingContext"/> 类型的包含序列化数据的上下文</param>
        protected PaneList(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");
        }
        /// <summary>
        /// 实现反序列化的构造函数
        /// </summary>
        /// <param name="info">一个 <see cref="SerializationInfo"/> 的实例，用于定义序列化的数据</param>
        /// <param name="context">一个 <see cref="StreamingContext"/> 类型的包含序列化数据的上下文</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("schema", schema);
        }
        #endregion

        #region List Methods

        /// <summary>
        /// Indexer to access the specified <see cref="GraphPane"/> object by
        /// its <see cref="PaneBase.Title"/> string.
        /// </summary>
        /// <param name="title">The string title of the
        /// <see cref="GraphPane"/> object to be accessed.</param>
        /// <value>A <see cref="GraphPane"/> object reference.</value>
        public GraphPane this[string title]
        {
            get
            {
                int index = IndexOf(title);
                if (index >= 0)
                    return ((GraphPane)this[index]);
                else
                    return null;
            }
        }

        /// <summary>
        /// Return the zero-based position index of the
        /// <see cref="GraphPane"/> with the specified <see cref="PaneBase.Title"/>.
        /// </summary>
        /// <remarks>The comparison of titles is not case sensitive, but it must include
        /// all characters including punctuation, spaces, etc.</remarks>
        /// <param name="title">The <see cref="String"/> label that is in the
        /// <see cref="PaneBase.Title"/> attribute of the item to be found.
        /// </param>
        /// <returns>The zero-based index of the specified <see cref="GraphPane"/>,
        /// or -1 if the <see cref="PaneBase.Title"/> was not found in the list</returns>
        /// <seealso cref="IndexOfTag"/>
        public int IndexOf(string title)
        {
            int index = 0;
            foreach (GraphPane pane in this)
            {
                if (String.Compare(pane.Title.Text, title, true) == 0)
                    return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// Return the zero-based position index of the
        /// <see cref="GraphPane"/> with the specified <see cref="PaneBase.Tag"/>.
        /// </summary>
        /// <remarks>In order for this method to work, the <see cref="PaneBase.Tag"/>
        /// property must be of type <see cref="String"/>.</remarks>
        /// <param name="tagStr">The <see cref="String"/> tag that is in the
        /// <see cref="PaneBase.Tag"/> attribute of the item to be found.
        /// </param>
        /// <returns>The zero-based index of the specified <see cref="GraphPane"/>,
        /// or -1 if the <see cref="PaneBase.Tag"/> string is not in the list</returns>
        public int IndexOfTag(string tagStr)
        {
            int index = 0;
            foreach (GraphPane pane in this)
            {
                if (pane.Tag is string &&
                        String.Compare((string)pane.Tag, tagStr, true) == 0)
                    return index;
                index++;
            }

            return -1;
        }

        #endregion
    }
}
