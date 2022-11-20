using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IsApi.DTO;
using IsApi.Service.Identity;
using IsApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace IsApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;
        private ITokenService _tokenService;
        public TokenController(ILogger<TokenController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult GetToken([FromBody]TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Ok(allErrors);
            }
            try
            {
                var response = _tokenService.GetToken(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}