using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Infrastructure
{
    public static class CustomHelpers
    {

        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            TagBuilder tag = new TagBuilder("ul");

            foreach (string str in list)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(str);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        //public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        //{
        //    string result = String.Format("This is the message: <p>{0}</p>", msg);
        //    return new MvcHtmlString(result);
        //}

        /// <summary>
        /// 字符串的返回结果，以通知视图引擎在添加信息之前进行编码
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public static string DisplayMessage(this HtmlHelper html, string msg)
        //{
        //    return String.Format("This is the message: <p>{0}</p>", msg);
        //}

        /// <summary>
        /// 使用 Encode 实例方法解决字符串编码问题
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        {
            string encodeMessage = html.Encode(msg);
            string result = String.Format("This is the message: <p>{0}</p>", encodeMessage);
            return new MvcHtmlString(result);
        }

    }
}