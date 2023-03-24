using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monacos.us_mvc.HomeAutomation;
using System.Configuration;


namespace monacos.us_mvc.Controllers.Components
{
    public class Home_Automation_ComponentController : Controller
    {
        // GET: Home_Automation_Component
        public ActionResult Index()
        {

            HomeAutomation.HomeAutomationServiceClient objHomeAutomationServiceClient = null;
            HomeAutomation.ResultInformation objRI = new HomeAutomation.ResultInformation();
            HomeAutomation.DeviceInformation[] objDeviceInformationArray = null;

            try
            {

              
                objHomeAutomationServiceClient = new HomeAutomation.HomeAutomationServiceClient();

                string HomeAutomationEndPoint = ConfigurationManager.AppSettings.Get("HomeAutomationServiceEndPoint");


                objHomeAutomationServiceClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(HomeAutomationEndPoint);


                objHomeAutomationServiceClient.InitializeService(ref objDeviceInformationArray, ref objRI);
            }


            catch (System.Exception e)
            {

                throw new Exception("Home_Automation_ComponentController::Index Error: " + e.Message);

            }


            return PartialView(objDeviceInformationArray);

        }

       
        [HttpPost]
        public ActionResult Index(int ControlDeviceID, string ControlDeviceCommand, int ControlDeviceValue )
        {

            HomeAutomation.HomeAutomationServiceClient objHomeAutomationServiceClient = null;
            HomeAutomation.ResultInformation objRI = new HomeAutomation.ResultInformation();
            HomeAutomation.DeviceInformation[] objDeviceInformationArray = null;


            try
            {

                objHomeAutomationServiceClient = new HomeAutomation.HomeAutomationServiceClient();

                string HomeAutomationEndPoint = ConfigurationManager.AppSettings.Get("HomeAutomationServiceEndPoint");

                objHomeAutomationServiceClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(HomeAutomationEndPoint);

                objHomeAutomationServiceClient.UpdateControlDeviceState(ControlDeviceID, ControlDeviceCommand, Convert.ToDouble(ControlDeviceValue), ref objDeviceInformationArray, ref objRI);
            }


            catch (System.Exception e)
            {

                throw new Exception("Home_Automation_ComponentController::Index Error: " + e.Message);

            }

            return PartialView(objDeviceInformationArray);

        }



    }
}