using Microsoft.IdentityModel.Tokens;
using project.Entities.identity;
using project.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project.Implementation
{
    public class TokenInterface : ITokenInterface
    {
        private readonly IConfiguration configuration;

        private readonly SymmetricSecurityKey key;

        public TokenInterface(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
        }

        public string CreateToken(AppUser user)
        {
            var claim = new List<Claim>
           {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.GivenName,user.DisplayName)
           };
            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Subject=new ClaimsIdentity(claim),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials = cred,
                Issuer = configuration["Token:Issuer"]

            };

             var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }


}
