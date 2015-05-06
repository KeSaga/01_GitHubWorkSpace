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
        }

    }
}