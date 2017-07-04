using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JourneysApp.WebHookHandlers 
{
    public class GitHubWebHookHandler : WebHookHandler
    {
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            if ("GitHub".Equals(receiver, StringComparison.CurrentCultureIgnoreCase))
            {
                string action = context.Actions.First();
                JObject data = context.GetDataOrDefault<JObject>();
                var dataAsString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            }

            return Task.FromResult(true);
        }

    }
}