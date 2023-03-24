using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using monacos.us.model;
using monacos.us.dal;




namespace monacos.us_mvc.BusinessObjects
{


    /// <summary>
    /// Summary description for ContentSystem_BO
    /// </summary>
    public class ContentSystem_BO
    {
        public ContentSystem_BO()
        {
            //
            // TODO: Add constructor logic here
            //






        }


        public List<Content> GetActiveContentItems(Int32 ContentArea_ID, bool IncludeOnlyCurrentlyPublished)
        {

            List<Content> objList_Content = null;
            Content_DAO objContentSystem_BO = null;


            try
            {

                objContentSystem_BO = new Content_DAO();

                objList_Content = objContentSystem_BO.Get(ContentArea_ID, IncludeOnlyCurrentlyPublished);

            }

            catch (System.Exception objException)
            {
                throw new Exception("ContentSystem_BO::GetActiveContentItems Error: " + objException.Message);
            }

            finally
            {

            }


            return objList_Content;

        }


        public Content SelectContentItem(Int32 Content_ID)
        {

            Content objECR_Content = null;
            Content_DAO objContentSystem_BO = null;

            try
            {

                objContentSystem_BO = new Content_DAO();

                objECR_Content = objContentSystem_BO.Select(Content_ID);

            }

            catch (System.Exception objException)
            {
                throw new Exception("ContentSystem_BO::SelectContentItem Error: " + objException.Message);
            }

            finally
            {

            }

            return objECR_Content;

        }



        public void AddContentItem(Content objECR_ContentItem)
        {


            Content_DAO objContentSystem_BO = null;

            try
            {

                objContentSystem_BO = new Content_DAO();

                objContentSystem_BO.Add(objECR_ContentItem);

            }

            catch (System.Exception objException)
            {
                throw new Exception("ContentSystem_BO::AddContentItem Error: " + objException.Message);
            }

            finally
            {

            }

        }

        public void UpdateContentItem(Content objECR_ContentItem)
        {


            Content_DAO objContentSystem_BO = null;

            try
            {

                objContentSystem_BO = new Content_DAO();

                objContentSystem_BO.Update(objECR_ContentItem);

            }

            catch (System.Exception objException)
            {
                throw new Exception("ContentSystem_BO::UpdateContentItem Error: " + objException.Message);
            }

            finally
            {

            }

        }



    }
}