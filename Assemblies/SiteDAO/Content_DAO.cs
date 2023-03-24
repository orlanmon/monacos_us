using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SiteDAO
{
    public class Content_DAO
    {


        public Int32 Add( Content objContent )
        {
            string DatabaseConnectionString = "";
           
            try
            {


                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

                objDataContext.Contents.InsertOnSubmit(objContent);

                objDataContext.SubmitChanges();
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
            Content objContent = null;

            try
            {
               

                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

                objContent = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == Content_ID);

                objDataContext.Contents.DeleteOnSubmit(objContent);

                objDataContext.SubmitChanges();
            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Delete" + objException.Message);

            }


        }

        public void Update(Content objContent)
        {

            string DatabaseConnectionString = "";
            Content objContentUpdate = null;

          
            try
            {
               

                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

                objContentUpdate = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == objContent.Content_ID);

                objContentUpdate.ContentArea_ID = objContent.ContentArea_ID;
                objContentUpdate.ContentValue = objContent.ContentValue;
                objContentUpdate.Publish_Date = objContent.Publish_Date;
                objContentUpdate.Expiration_Date = objContent.Expiration_Date;
                objContentUpdate.Description = objContent.Description;
                objContentUpdate.Active = objContent.Active;


                objDataContext.SubmitChanges();
            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Update" + objException.Message);

            }

        }

        public Content Select(Int32 Content_ID )
        {

            string DatabaseConnectionString = "";
            Content objContent = null;

            try
            {
             

                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

                objContent = objDataContext.Contents.Single(contentitem => contentitem.Content_ID == Content_ID);
            }
            catch (Exception objException)
            {
                throw new Exception("Content_DAO::Select" + objException.Message);

            }

            return objContent;
 
        }


        public List<Content> Get(Int32 ContentArea_ID, bool IncludeOnlyCurrentlyPublished )
        {

            string DatabaseConnectionString = "";
            List<Content> objContents = null;

            try
            {

              
                DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

                Monacos_usDataContext objDataContext = new Monacos_usDataContext(DatabaseConnectionString);

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
                            (System.DateTime.Now <= contentitem.Expiration_Date )
                            &&
                            (System.DateTime.Now >= contentitem.Publish_Date )
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
