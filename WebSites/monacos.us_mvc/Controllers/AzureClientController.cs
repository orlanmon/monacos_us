using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;

namespace monacos.us_mvc.Controllers
{
    public class AzureClientController : Controller
    {
        // GET: AzureClient
        [HttpGet]
        public ActionResult Index()
        {

            string WebAPIResponse = string.Empty;


            return View(WebAPIResponse);


        }

        [HttpPost]
        public async Task<ActionResult> InvokeAzureAPI()
        {

            string WebAPIResponse = string.Empty;


            WebAPIResponse = await GetKeyVaultSettings();

            //return RedirectToRoute("Message", new { controller = "AzureClient", action = "Index" });


            //return View("Index", WebAPIResponse);

            return View("Index", "_Layout", WebAPIResponse);


        }



        public async Task<string> GetKeyVaultSettings()
        {

            string APIResponse = string.Empty;
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            string accessToken = string.Empty;
            HttpRequestMessage request = null;


            //*********************************************************************************************************************

            string tenantID = "02ab6bc4-9edc-4e50-b34f-f804fb5ff6fa";   // Directory ID/Tenant ID
            string authority = "https://login.microsoftonline.com/" + tenantID;
            string resourceID = "https://monacosmonacos.onmicrosoft.com/424f5034-98ca-445e-bca1-1cbbc03b6281";               // Web App ID URI
            //string redirectUri = "www.monacos.us";
            //TokenCache tokenCache = null;
            //string authorizationCode = context.ProtocolMessage.Code;

           
            // www.monacos.us  AAD Application Registration Information
            // Created in Azure Active Directory App Registration
            string clientId = "4c7f365d-85e0-4297-9ec1-975a1bb3e276";                // Application ID / Client ID
            string clientSecret = "BLz7/ANWX1cDvwSQCgcvpZtZ723QcPU+GPiLukg/dz0=";    // Application Secret Key
            AuthenticationContext authContext = null;
            ClientCredential credential = null;
            AuthenticationResult authResult = null;


            //*********************************************************************************************************************
            // Obtain Access Token
            //*********************************************************************************************************************

            credential = new ClientCredential(clientId, clientSecret);

            authContext = new AuthenticationContext(authority);

            if (authContext.TokenCache.ReadItems().Count() > 0)
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);


                try
                {
                    authResult = authContext.AcquireTokenSilentAsync(resourceID, clientId).Result;
                }
                catch (Exception ex)
                {

                    //AuthenticationResult authResult = await authContext.AcquireTokenByAuthorizationCodeAsync( authorizationCode, new Uri(redirectUri), credential, resourceID);

                    authResult = await authContext.AcquireTokenAsync(resourceID, credential);

                }

            }
            else
            {

                authResult = await authContext.AcquireTokenAsync(resourceID, credential);

            }


            //*********************************************************************************************************************
            // Invoke Web API With Access Token in Header
            //*********************************************************************************************************************

            request = new HttpRequestMessage(HttpMethod.Get, "http://micro-service-one-prod-env.azurewebsites.net/api/configuration/GetKeyVaultSetting/UserName");

            accessToken = authResult.AccessToken;

            // Add Access Token To HTTP Header of Request
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpResponseMessage response = await httpClient.SendAsync(request);

            // Following Code Is What Used to be A Callback

            if (response.IsSuccessStatusCode)
            {

                var stream = response.Content.ReadAsStreamAsync().Result;
               
                StreamReader readStream = new StreamReader(stream, Encoding.UTF8);
                APIResponse = readStream.ReadToEnd();

               


            }
            else
            {
                // Error

                
            
                APIResponse = response.ToString();




            }


            return APIResponse;


        }



    }
}