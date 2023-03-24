using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.SessionState;
using monacos.us_mvc.BusinessObjects;


/// <summary>
/// Summary description for NavigationSystemService
/// </summary>
/// 

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class NavigationSystemService : INavigationSystemService
{

    

    [WebGet(UriTemplate = "GetHeaderMenu?chka={CheckAuthentication}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public string GetHeaderMenu(bool CheckAuthentication)
    {

        NavigationSystem_BO objNavigationSystem_BO = null; 
        String HeaderMenuJSON = null;
        String DatabaseConnectionString = null;
        Authentication objAuthentication = null;
       

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();




            
            if (CheckAuthentication == false)
            {
                // Get the Currently Generated Menu with the current Authentication

                if (HttpContext.Current.Session["Session_HeaderMenuJSON"] == null)
                {

                    objNavigationSystem_BO = new NavigationSystem_BO();

                    DatabaseConnectionString = DatabaseConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"];

                    objNavigationSystem_BO.DatabaseConnection = DatabaseConnectionString;


                    if (HttpContext.Current.Session["Session_Authentication"] == null)
                    {
                        // No Authentication Performed So Default to General Access

                        HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, 6);

                        
                        HttpContext.Current.Session["Session_HeaderMenuJSON"] = HeaderMenuJSON;


                    }
                    else
                    {
                        // Uses Existing Authentication and Role Associated with Logged In User

                        objAuthentication = (Authentication) HttpContext.Current.Session["Session_Authentication"];

                        if ( objAuthentication.UserRoles.Count() > 0 ) 
                        {

                            // Access Dependent on First Role Nothing Fancy Here
                            HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, objAuthentication.UserRoles[0].Resource_Role_ID);
                            
                        }
                        else
                        {
                            // No Roles Then Just General Access
                            HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, 6);

                            
                        }

                        HttpContext.Current.Session["Session_HeaderMenuJSON"] = HeaderMenuJSON;
 

                    }

                    
                    

                }
                else
                {
                    HeaderMenuJSON = HttpContext.Current.Session["Session_HeaderMenuJSON"].ToString();
                }

            
            }
            else
            {

                // Re Generate the Menu with the current Authentication

                DatabaseConnectionString = DatabaseConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"];

                objNavigationSystem_BO = new NavigationSystem_BO();

                objNavigationSystem_BO.DatabaseConnection = DatabaseConnectionString;
                
              
                if ((HttpContext.Current.Session["Session_Authentication"] != null)  )
                {

                    objAuthentication = (Authentication)HttpContext.Current.Session["Session_Authentication"];


                    if (objAuthentication.UserRoles.Count() > 0)
                    {
                        // Access Dependent on First Role Nothing Fancy Here
                        HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, objAuthentication.UserRoles[0].Resource_Role_ID);
                        
                    }
                    else
                    {
                        // No Roles Then Just General Access
                        HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, 6);
                        
                    }

                }
                else
                {
                    // No Authentication Then Just General Access
                    HeaderMenuJSON = objNavigationSystem_BO.BuildNavigationMenu(1, 6);
                    

                }

                HttpContext.Current.Session["Session_HeaderMenuJSON"] = HeaderMenuJSON;

            }

        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("GetHeaderMenu Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {
            objNavigationSystem_BO = null;

        }

        return HeaderMenuJSON;

       
    }

   

}