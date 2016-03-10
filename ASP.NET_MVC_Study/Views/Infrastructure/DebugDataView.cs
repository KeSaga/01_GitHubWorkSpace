using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Views.Infrastructure
{
    public class DebugDataView : IView
    {

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Write(writer, "---Routing Data（路由数据）---");
            foreach (string key in viewContext.RouteData.Values.Keys)
            {
                Write(writer, "key: {0},value: {1}", key, viewContext.RouteData.Values[key]);
            }

            Write(writer, "---View Data（视图数据）---");
            foreach (string key in viewContext.ViewData.Keys)
            {
                Write(writer, "key: {0},value: {1}", key, viewContext.ViewData[key]);
            }

        }

        private void Write(TextWriter writer, string template, params object[] values)
        {
            writer.Write(string.Format(template, values) + "<p/>");
        }

    }
}