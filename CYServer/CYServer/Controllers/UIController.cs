﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYServer.Controllers
{
    public class UIController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
