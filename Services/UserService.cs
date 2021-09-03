using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using PgccApi.Models;
using PgccApi.Helpers;
using PgccApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace PgccApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly PgccContext _context;
        private readonly AppSettings _appSettings;

        public UserService(PgccContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var tokenExpiry = DateTime.UtcNow.AddHours(2);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = tokenExpiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.TokenExpiry = tokenExpiry;

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}