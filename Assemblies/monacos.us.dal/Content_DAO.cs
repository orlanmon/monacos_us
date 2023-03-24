using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monacos.us.model;
using System.Configuration;




namespace monacos.us.dal
{
    public class Content_DAO
    {

        public Int32 Add(model.Content objContent)
        {
            string DatabaseConnectionString = "";

            try
            {


                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                monacos.us.model.Entities objDataContext = new monacos.us.model.Entities(DatabaseConnectionString);

                objDataContext.Contents.Add(objContent);

                objDataContext.SaveChanges();

            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Add" + objException.Message);

            }


            return objContent.Content_ID;


        }


        public void Delete(Int32 Content_ID)
        {

            string DatabaseConnectionString = "";
            monacos.us.model.Content objContent = null;

            try
            {


                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                monacos.us.model.Entities objDataContext = new monacos.us.model.Entities(DatabaseConnectionString);

                objContent = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == Content_ID);

                objDataContext.Contents.Remove(objContent);

                objDataContext.SaveChanges();

            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Delete" + objException.Message);

            }


        }

        public void Update(model.Content objContent)
        {

            string DatabaseConnectionString = "";
            monacos.us.model.Content objContentUpdate = null;

            try
            {

                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                monacos.us.model.Entities objDataContext = new monacos.us.model.Entities(DatabaseConnectionString);

                objContentUpdate = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == objContent.Content_ID);

                objContentUpdate.ContentArea_ID = objContent.ContentArea_ID;
                objContentUpdate.ContentValue = objContent.ContentValue;
                objContentUpdate.Publish_Date = objContent.Publish_Date;
                objContentUpdate.Expiration_Date = objContent.Expiration_Date;
                objContentUpdate.Description = objContent.Description;
                objContentUpdate.Active = objContent.Active;

                objDataContext.SaveChanges();


            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Update" + objException.Message);

            }

        }

        public Content Select(Int32 Content_ID)
        {

            string DatabaseConnectionString = "";
            monacos.us.model.Content objContent = null;

            try
            {


                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                monacos.us.model.Entities objDataContext = new monacos.us.model.Entities(DatabaseConnectionString);

                objContent = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == Content_ID);
            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Select" + objException.Message);

            }

            return objContent;

        }


        public List<model.Content> Get(Int32 ContentArea_ID, bool IncludeOnlyCurrentlyPublished)
        {

            string DatabaseConnectionString = "";
            List<monacos.us.model.Content> objContents = null;

            try
            {


                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                monacos.us.model.Entities objDataContext = new monacos.us.model.Entities(DatabaseConnectionString);

                //contentitem.Expiration_Date >= System.DateTime.Now

                if (!IncludeOnlyCurrentlyPublished)
                {
                    // This Includes Everything Except InActive 

                    objContents = objDataContext.Contents.Where(contentitem => contentitem.Active == true && contentitem.ContentArea_ID == ContentArea_ID).OrderByDescending(contentitem => contentitem.Publish_Date).ToList();

                }
                else
                {

                    // Only Active Content That is Currently Published
                    // Two Scenarios:
                    // #1 Between and Including Publish Date and Expiration Date 
                    // #2 After and Including Publish Date but No Expiration Date so never expires.

                    /*
                    objContents = objDataContext.Contents.Where(contentitem => contentitem.Active == true 
                     && contentitem.ContentArea_ID == ContentArea_ID 
                     &&
                     (
                           (  
                            ( (((System.DateTime)contentitem.Expiration_Date).Subtract(System.DateTime.Now).Days >= 0))
                            && (System.DateTime.Now.Subtract((DateTime)contentitem.Publish_Date).Days >= 0) 
                           )
                           ||
                           (
                            ( contentitem.Expiration_Date == null )
                            && (System.DateTime.Now.Subtract((DateTime)contentitem.Publish_Date).Days >= 0) 
                           )
                    )

                     ).OrderByDescending(contentitem => contentitem.Publish_Date).ToList();
                    */

                    objContents = objDataContext.Contents.Where(contentitem => contentitem.Active == true
                     && contentitem.ContentArea_ID == ContentArea_ID
                     &&
                     (
                           (
                            (contentitem.Expiration_Date != null)
                            &&
                            (System.DateTime.Now <= contentitem.Expiration_Date)
                            &&
                            (System.DateTime.Now >= contentitem.Publish_Date)
                           )
                           ||
                           (
                            (contentitem.Expiration_Date == null)
                            && (System.DateTime.Now >= contentitem.Publish_Date)
                           )
                    )

                     ).OrderByDescending(contentitem => contentitem.Publish_Date).ToList();



                }

                // contentitem.Expiration_Date == null ||
            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Get" + objException.Message);

            }

            return objContents;

        }

    }
}
