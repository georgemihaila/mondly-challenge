using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace asp_core_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(string param)
        {
            switch (param)
            {
                case "abc":
                    return "Ok";
            }
            return "Fail.";
        }
    }
}