using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Ninject;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.Website
{
    public static class DependencyResolverConfig
    {
        // this was helpful: http://www.codeproject.com/Articles/412383/Dependency-Injection-in-asp-net-mvc4-and-webapi-us

        public static IDependencyResolver CreateDependencyResolver()
        {
            var kernel = CreateKernel();
            var resolver = new NinjectDependencyResolver(kernel);

            return resolver;
        }

        private static void BindAllTheThings(IKernel kernel)
        {
            kernel.Bind<IRoomieDatabaseContext>().To<RoomieDatabaseContext>();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            BindAllTheThings(kernel);

            return kernel;
        }

        private class NinjectDependencyResolver : IDependencyResolver
        {
            private readonly IKernel _kernel;

            public NinjectDependencyResolver(IKernel kernel)
            {
                _kernel = kernel;
            }

            object IDependencyResolver.GetService(Type serviceType)
            {
                return _kernel.TryGet(serviceType);
            }

            IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
            {
                try
                {
                    return _kernel.GetAll(serviceType);
                }
                catch
                {
                    return new List<object>();
                }
            }
        }
    }
}