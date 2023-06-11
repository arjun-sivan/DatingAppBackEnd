using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITockenService
    {
        private readonly SymmetricSecurityKey _key;
        // private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config)
        {
            // _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string Createtoken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        //public string CreateToken(AppUser user)
        //{
        // new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),

        //   // var roles = await _userManager.GetRolesAsync(user);

        //    //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //    



        //    
        //   

        //   
        //}
    }
}