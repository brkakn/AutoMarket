﻿using AutoMapper;
using AutoMarket.Api.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMarket.Api.Infrastructures.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : this("AutoMapperProfileMappings")
        {
        }
        public AutoMapperProfile(string profileName) : base(profileName)
        {
            IEnumerable<Type> types = Assembly.Load(CommonConstants.SERVICE_NAME).GetExportedTypes();

            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IMapping).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                        select (IMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(this);
            }
        }
    }
}
