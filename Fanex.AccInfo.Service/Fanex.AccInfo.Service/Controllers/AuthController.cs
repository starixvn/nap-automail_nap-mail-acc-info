using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Fanex.AccInfo.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string secretKey = AccInfoConfigService.Config.ApiKey;

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] string apiKey)
        {
            apiKey ??= string.Empty;
            if (apiKey.Equals("%MPAkBS8eZGkNEHD4M7fs6EQNPLrzJg98$"))
            {
                var token = GenerateJwtToken(apiKey);
                return Ok(new TokenModel { Token = token });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("Query")]
        public IActionResult Query(ParamModel param)
        {
            var result = new Dictionary<string, AccInfoModel>();
            param.Keys.ForEach(key =>
            {
                var acc = AccInfoConfigService.Config.AccInfos.FirstOrDefault(s => s.Key == key);
                if (acc.Key != null)
                {
                    result.Add(acc.Key, acc.Value);
                }
            });

            return new JsonResult(result);
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}