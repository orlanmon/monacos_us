using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace SiteDAO
{

    /// <summary>
    /// Summary description for GPT_Tracking_BO
    /// </summary>
    public class GPS_Tracking_DAO
    {
        public GPS_Tracking_DAO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Int32 Add(string Latitude, string Longitude, string DeviceID)
        {
            string DatabaseConnectionString = "";
            GPS_Tracking objGPS_Tracking = null;


            DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);


            objGPS_Tracking = new GPS_Tracking();

            objGPS_Tracking.Latitude = Latitude;
            objGPS_Tracking.Longitude = Longitude;
            objGPS_Tracking.Device_ID = DeviceID;
            objGPS_Tracking.Entry_Date = System.DateTime.Now;






            objDataContext.GPS_Trackings.InsertOnSubmit(objGPS_Tracking);

            objDataContext.SubmitChanges();

            return objGPS_Tracking.GPS_TrackingID;


        }

        public void Delete(Int32 GPS_TrackingID)
        {
            string DatabaseConnectionString = "";

            DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

            GPS_Tracking objGTP_Tracking = objDataContext.GPS_Trackings.Single(gpstitem => gpstitem.GPS_TrackingID == GPS_TrackingID);

            objDataContext.GPS_Trackings.DeleteOnSubmit(objGTP_Tracking);

            objDataContext.SubmitChanges();


        }

        public void Update(Int32 GPS_TrackingID, string Longitude, string Latitude, string DeviceID)
        {
            string DatabaseConnectionString = "";

            DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);


            GPS_Tracking objGTP_Tracking = objDataContext.GPS_Trackings.Single(gpstitem => gpstitem.GPS_TrackingID == GPS_TrackingID);

            objGTP_Tracking.Latitude = Latitude;
            objGTP_Tracking.Longitude = Longitude;
            objGTP_Tracking.Device_ID = DeviceID;


            objDataContext.SubmitChanges();


        }

        public GPS_Tracking Select(Int32 GPS_TrackingID)
        {
            string DatabaseConnectionString = "";

            DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);


            GPS_Tracking objGTP_Tracking = objDataContext.GPS_Trackings.Single(gpstitem => gpstitem.GPS_TrackingID == GPS_TrackingID);


            return objGTP_Tracking;

        }


        public GPS_Tracking GetLatestEntrySelect()
        {
            string DatabaseConnectionString = "";

            DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);



            GPS_Tracking objGTP_Tracking = (from gpst in objDataContext.GPS_Trackings
                                           orderby gpst.GPS_TrackingID descending
                                           select gpst).FirstOrDefault();

                                           


            return objGTP_Tracking;

        }

    }
}
