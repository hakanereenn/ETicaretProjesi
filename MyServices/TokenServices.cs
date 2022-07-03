using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EticaretProjesi.MyServices
{
    public partial class TokenServices
    {
        public static string GenerateToken(string jwtKey, DateTime expires,IEnumerable<Claim> claims, string issuer="site.com", string audience= "site.com" )
        {
            byte[] key = Encoding.UTF8.GetBytes(jwtKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //List<Claim> claims = new List<Claim>()
            //        {
            //            new Claim("id", account.Id.ToString()),
            //            new Claim("type",((int) account.Type).ToString()),
            //            new Claim(ClaimTypes.Name, account.UserName),
            //            new Claim(ClaimTypes.Role, account.Type.ToString()),
            //        };
            JwtSecurityToken tokenSecurity = new JwtSecurityToken(issuer, audience, claims, expires:expires, signingCredentials: credentials);
            string writeToken = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
            return writeToken;
        }
    }
}
