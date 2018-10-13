using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_core_mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static asp_core_mvc.Entities.Globals;

namespace asp_core_mvc.Controllers
{
    public class SessionsController : Controller
    {
        public string GetStatus(string sid, string token)
        {
            SessionStatus status = new SessionStatus();
            if (!Globals.SessionCollection[sid].IsValid(token))
                return Failure;
            int count = Globals.SessionCollection[sid].Players.Count();
            status.Ready = (count == 3);
            status.Message = (status.Ready) ? "Ready" :
                string.Format("Awaiting {0} player{1}...", 3 - count, (3 - count != 1) ? "s" : string.Empty);
            return JsonConvert.SerializeObject(status);
        }
        //[HttpPost]

        class SessionStatus
        {
            public bool Ready { get; set; }

            public string Message { get; set; }
        }
    }
}