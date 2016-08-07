using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using ZillowSearch.Models.PropertyDetails;
using ZillowSearch.ZillowPropertySerach;

namespace ZillowSearch
{
    public class UnityConfiguration
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            
            // Register classes
            container.RegisterType<IZillowPropertySearchGateway, ZillowPropertySearchGateway>();
            container.RegisterType<IGenericRestServiceCaller, GenericRestServiceCaller>();
            container.RegisterType<IPropertyDetailFactory, PropertyDetailFactory>();

            return container;
        }
    }
}