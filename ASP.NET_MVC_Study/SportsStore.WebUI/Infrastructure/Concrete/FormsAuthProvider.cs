using SportsStore.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

// 认证提供器
// 使用表单认证特性要求我们调用 System.Web.Security.FormsAuthentication 类的两个静态方法。如下：
// Authenticate 方法验证由用户提供的凭据。
// SetAuthCookie 方法对浏览器的响应添加一个 cookie，这样，便不需要对用户所作出的每一个请求都进行认证。

// 在动作方法中调用镜头方法会带来一个问题，就是它会使控制器的单元测试变得困难。像 Moq 这样的模块框架只能模仿
// 实例成员。之所以会出现这一问题，是应为 FormsAuthentication 类早于 MVC 的友好单元测试的设计。
// 这个问题的最好解决办法是使用一个接口，以便将控制器从带有静态方法的类中解耦出来。

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        /// <summary>
        /// 调用希望放在控制器值为的静态 FormsAuthentication 方法（这里调用了 Authenticate 和 SetAuthCookie 两个静态方法）
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}