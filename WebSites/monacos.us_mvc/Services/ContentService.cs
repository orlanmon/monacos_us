using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.SessionState;
using System.Web.Http;
using monacos.us.dtos;
using monacos.us.model;
using AutoMapper;
using monacos.us_mvc.BusinessObjects;

/// <summary>
/// Summary description for ContentService
/// </summary>
/// 
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContentService : IContentService
    {
        public ContentService()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        [WebGet(UriTemplate = "/Content/Select?cid={Content_ID}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public ContentDTO SelectContent(Int32 Content_ID)
        {
            
            Content ContentItem = null;
            ContentDTO ContentItemDTO = null;

            try
            {

                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                HttpContext.Current.Response.Cache.SetNoStore();


                ContentSystem_BO objContentSystem_BO = new ContentSystem_BO();

                ContentItem = objContentSystem_BO.SelectContentItem(Content_ID);

                AutoMapper.IMapper objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

                ContentItemDTO = objAutoMapper.Map<Content, ContentDTO>(ContentItem);

            }
            catch (Exception objException)
            {

                RestException objRestException = new RestException("SelectContent Exception", objException.Message);

                throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

            }

            finally
            {

            }

            return ContentItemDTO;

        }


        [WebInvoke(Method = "GET", UriTemplate = "/Content/GetActive?caid={ContentArea_ID}&icp={IncludeOnlyCurrentlyPublished}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public List<ContentDTO> GetActiveContent(Int32 ContentArea_ID, bool IncludeOnlyCurrentlyPublished)
        {

            List<Content> objListContentItems = new List<Content>();
            ContentDTO objContentDTO = null;

            List<ContentDTO> objListContentDTOItems = null;



            try
            {

                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                HttpContext.Current.Response.Cache.SetNoStore();


                ContentSystem_BO objContentSystem_BO = new ContentSystem_BO();

                objListContentItems = objContentSystem_BO.GetActiveContentItems(ContentArea_ID, IncludeOnlyCurrentlyPublished);

                AutoMapper.IMapper objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

                objListContentDTOItems = new List<ContentDTO>();
              

                foreach (Content ContentItem in objListContentItems)
                {
                    
                    objContentDTO = objAutoMapper.Map<Content, ContentDTO>(ContentItem);

                    objListContentDTOItems.Add(objContentDTO);

                }

            }
            catch (Exception objException)
            {

                RestException objRestException = new RestException("GetActiveContent Exception", objException.Message);

                throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

            }

            finally
            {

            }

            return objListContentDTOItems;

        }


        [WebInvoke(Method = "POST", UriTemplate = "/Content/Add", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public void AddContent(ContentDTO objContentDTO)
        {


            Content objContentItem = null;

            ContentSystem_BO objContentSystem_BO = null;


            try
            {

                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                HttpContext.Current.Response.Cache.SetNoStore();

              
                AutoMapper.IMapper objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

                objContentItem = objAutoMapper.Map< ContentDTO, Content >(objContentDTO);

                objContentSystem_BO = new ContentSystem_BO();

                objContentSystem_BO.AddContentItem(objContentItem);

            }
            catch (Exception objException)
            {

                RestException objRestException = new RestException("AddContent Exception", objException.Message);

                throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

            }

            finally
            {

            }


        }



        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/Content/Update", BodyStyle = WebMessageBodyStyle.Bare)]
        public void UpdateContent(ContentDTO objContentDTO)
        {

            Content objContentItem = null;
            ContentSystem_BO objContentSystem_BO = null;

            try
            {

                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                HttpContext.Current.Response.Cache.SetNoStore();

                AutoMapper.IMapper objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

                objContentItem = objAutoMapper.Map<ContentDTO, Content>(objContentDTO);

                objContentSystem_BO = new ContentSystem_BO();

                objContentSystem_BO.UpdateContentItem(objContentItem);

            }
            catch (Exception objException)
            {

                RestException objRestException = new RestException("UpdateContent Exception", objException.Message);

                throw new WebFaultException<RestException>(objRestException, System.Net.HttpStatusCode.InternalServerError);

            }

            finally
            {

            }

        }



    }

