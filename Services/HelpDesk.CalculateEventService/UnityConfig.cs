using System;
using Microsoft.Practices.Unity;
using HelpDesk.EventBus;
using System.Web.Configuration;

namespace HelpDesk.CalculateEventService
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
           
            //����������� ����
            EventBusInstaller.Install(container,
                WebConfigurationManager.AppSettings["RabbitMQHost"],
                WebConfigurationManager.AppSettings["ServiceAddress"],
                WebConfigurationManager.AppSettings["RabbitMQUserName"],
                WebConfigurationManager.AppSettings["RabbitMQPassword"]);
           
        }
        
    }    
        
}
