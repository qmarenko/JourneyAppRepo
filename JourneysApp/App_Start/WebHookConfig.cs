﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace JourneysApp.App_Start
{
    public class WebHookConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // registration of webhook for github
            config.InitializeReceiveGitHubWebHooks();
        }
    }
}