using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monacos.us.model;
using monacos.us.dtos;
using AutoMapper.Mappers;
using monacos.us_mvc.BusinessObjects;


namespace monacos.us_mvc.Components
{
    public class Home_Content_ComponentController : Controller
    {
        // GET: Home_Content_Component
        public ActionResult Index()
        {

            List<Content> objContent_List = null;
            ContentSystem_BO objContentSystem_BO = new ContentSystem_BO();

            AutoMapper.IMapper objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

            // Include Only Currently Published Content
            objContent_List = objContentSystem_BO.GetActiveContentItems(1, true);

            var ContentDTOList = (from cli in objContent_List
                                  select (objAutoMapper.Map<Content, ContentDTO>(cli))).ToList<ContentDTO>();

            
            return PartialView(ContentDTOList);

        }
    }
}