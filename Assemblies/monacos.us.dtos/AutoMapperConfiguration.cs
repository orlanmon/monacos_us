using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using AutoMapper;
using monacos.us.model;



namespace monacos.us.dtos
{
    static public class AutoMapperConfiguration
    {

        public static MapperConfiguration AutoMapperConfig = null;

        public static void Configure()
        {

            AutoMapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<monacos.us.model.Content, monacos.us.dtos.ContentDTO>();
                cfg.CreateMap<monacos.us.dtos.ContentDTO, monacos.us.model.Content >();
            });

        }


    }
}
