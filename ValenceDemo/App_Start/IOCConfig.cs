using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using ValenceDemo.Controllers;
using ValenceDemo.DAL;

namespace ValenceDemo.App_Start
{

    /// <summary>
    /// Dependecy injection
    /// </summary>
    public static class IOCConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<Icontact, ContactRepository>();
            DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(container));
        }
    }
}