using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;

namespace EssentialTools.Infrastructure
{
    /// <summary>
    /// 依赖解析器
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        #region IDependencyResolver 的实现

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        #endregion End IDependencyResolver 的实现

        private void AddBindings()
        {
            _kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //使用 WithPropertyValue 方法设置 DefaultDiscountHelper 类中的 DiscountSize 属性的值
            //通过这种方式可以不修改绑定或通过 Get 方法获取具体实现类的实例的方式
            _kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50M);

            //通过条件绑定，通知 Ninject 何时使用 FlexibleDiscountHelper，何时使用 DefaultDiscountHelper
            _kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }

    }
}