using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Binders
{
    /// <summary>
    /// 通过实现 IModelBinder 接口，可以创建一个自定义的模型绑定器
    /// </summary>
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        // ControllerContext 对控制器类所具有的全部信息提供了访问，这些信息包含了客户端请求的细节。
        // ModelBindingContext 提供了要求建立的模型对象的信息，以及使绑定更易于处理的工具。

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // 通过会话获取 Cart
            Cart cart=(Cart)controllerContext.HttpContext.Session[sessionKey];
            // 若会话中没有 Cart，则创建一个
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            // 返回 Cart
            return cart;
        }
    }
}