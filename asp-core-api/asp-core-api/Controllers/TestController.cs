﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace asp_core_api.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("Ok");
        }
    }
}