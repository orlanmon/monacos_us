using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.ServiceModel.Activation;
using monacos.us.dtos;





namespace monacos.us_mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {


            AreaRegistration.RegisterAllAreas();


            // Hosted Restful Services Start 
            RouteTable.Routes.Add(new ServiceRoute("NavigationSystemService", new WebServiceHostFactory(), typeof(NavigationSystemService)));

            RouteTable.Routes.Add(new ServiceRoute("SecurityService", new WebServiceHostFactory(), typeof(SecurityService)));

            RouteTable.Routes.Add(new ServiceRoute("ContentService", new WebServiceHostFactory(), typeof(ContentService)));
            


            RouteConfig.RegisterRoutes(RouteTable.Routes);


            /*
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            */


            
           



            // Configure AutoMapper For Model to DTOs
            AutoMapperConfiguration.Configure();

        }


        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            try
            {


                /*
                 
                string DatabaseConnectionString = null;
                string HeaderMenuJSON = null;

                DatabaseConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"];


                // Web Traffic Tracking

                WebTracking_BO objWebTracking_BO = new WebTracking_BO();

                objWebTracking_BO.DatabaseConnection = DatabaseConnectionString;

                objWebTracking_BO.RecordSessionInformation(this.Request);


                // Build Menu System

                NavigationSystem_BO objNavigationSystem_BO = new NavigationSystem_BO();
               
                objNavigationSystem_BO.DatabaseConnection = DatabaseConnectionString;

                // General Role Initially Used
                //HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, 6);

                //this.Session["Session_HeaderMenuJSON"] = HeaderMenuJSON;

                */


            }
            catch (Exception objException)
            {
                // Ignore
            }

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.



        }
    }
}