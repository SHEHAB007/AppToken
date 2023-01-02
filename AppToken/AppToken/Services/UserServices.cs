using AppToken.Helpers;
using AppToken.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppToken.Services
{
    public class UserServices : IUserServices
    {
        private readonly AppSettings appSettings;
        public UserServices(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
        }
        public List<User> users = new List<User>()
        {
            new User
            {
                Id = 1,
                UserName = "Shehab",
                Email = "Shehab@Sh.com",
                FirstName = "Shehab",
                LastName = "Ahmed",
                Password = "123456"

            },
            new User
            {
                Id = 2,
                UserName = "Shehab2",
                Email = "Shehab2@Sh.com",
                FirstName = "Shehab2",
                LastName = "Ahmed2",
                Password = "7891011"

            }
        };
        public User Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if(user == null)
            {
                return null;
            }
            var tokenHndler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(20),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , user.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),SecurityAlgorithms.HmacSha256Signature)
            };
           var token = tokenHndler.CreateToken(tokenDescriptor);
            user.Token = tokenHndler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users.Select(u => { u.Password = null;return u; });
        }
    }
}
