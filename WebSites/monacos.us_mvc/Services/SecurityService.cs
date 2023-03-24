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
/// Summary description for SecurityService
/// </summary>

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class SecurityService : ISecurityService
{

    

    [WebGet(UriTemplate = "Logout", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare) ]
    public bool Logout()
    {

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();

            HttpContext.Current.Session["Session_Authentication"] = null;

 
        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("Logout Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {

        }

        return true;


    }



    [WebGet(UriTemplate = "Login?un={strUserName}&pw={strPassword}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare )]
    public bool Login(String strUserName, String strPassword )
    {

        SiteSecurity_BO objSiteSecurity_BO = new SiteSecurity_BO();
        String DatabaseConnectionString = null;
        bool Authenticated = false;
        Authentication objAuthentication = null;

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();


            if (System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"] != null)
            {
                DatabaseConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"];
            }
            else
            {
                throw new Exception("Database Connection Configuration Doesn't Exist");
            }

            objSiteSecurity_BO.DatabaseConnection = DatabaseConnectionString;


            objAuthentication = objSiteSecurity_BO.GetAuthentication(strUserName, strPassword, "East Coast Reflector");

            if (objAuthentication != null)
            {
                Authenticated = true;

                HttpContext.Current.Session["Session_Authentication"] = objAuthentication;

            }

         
        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("Login Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {

        }


        return Authenticated;
       

    }

    [WebGet(UriTemplate = "GetSessionID", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare )]
    public string GetSessionID()
    {

        string SessionID = null;

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();



            HttpSessionState objSessionState = HttpContext.Current.Session;

            SessionID = objSessionState.SessionID;

        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("GetSessionID Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {

        }

        return SessionID;

    }

    [WebGet(UriTemplate = "GetLogInStatus", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare )]
    public bool GetLogInStatus()
    {

        bool LogInStatus = false;

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();

            LogInStatus = (HttpContext.Current.Session["Session_Authentication"] != null ? true : false);
            
        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("GetLogInStatus Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {

        }

        return LogInStatus;

    }


    [WebGet(UriTemplate = "GetLogInStatusRole?rn={RoleName}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare )]
    public bool GetLogInStatusRole(string RoleName)
    {

        bool LogInStatus = false;
        Authentication objAuthentication = null;

        try
        {

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.SetNoStore();

            if ( HttpContext.Current.Session["Session_Authentication"] != null )
            {
                objAuthentication = (Authentication)HttpContext.Current.Session["Session_Authentication"];

                for( int index = 0; index < objAuthentication.UserRoles.Count();  index++ )
                {
                    Role RoleItem = objAuthentication.UserRoles[index];

                    if ( RoleItem.Role_Name == RoleName )
                    {
                        LogInStatus = true;

                        break;
                       

                    }

                }
                    

                   

            }
            else
            {
                LogInStatus = false;

            }
            
        }
        catch (Exception objException)
        {

            RestException objRestException = new RestException("GetLogInStatus Exception", objException.Message);

            throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

        }

        finally
        {

        }

        return LogInStatus;

    }



}

    

