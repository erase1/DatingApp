using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] //grants several features, parameter binding is one of them, model validation is another
    [Route("api/[controller]")] //route with placeholder for controller
    public class BaseApiController : ControllerBase
    {
        
    }
}