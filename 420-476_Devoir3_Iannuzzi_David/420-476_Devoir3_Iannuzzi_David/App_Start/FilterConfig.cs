﻿using System.Web;
using System.Web.Mvc;

namespace _420_476_Devoir3_Iannuzzi_David
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
