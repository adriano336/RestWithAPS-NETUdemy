using Microsoft.IdentityModel.Tokens;
using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Repository;
using RestWithAPS_NETUdemy.Security.Config;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace RestWithAPS_NETUdemy.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfiguration _sign;
        private readonly TokenConfiguration _token;

        public LoginBusiness(IUserRepository repository, SigningConfiguration sign, TokenConfiguration token)
        {
            _repository = repository;
            _sign = sign;
            _token = token;
        }

        public object FindByLogin(User user)
        {
            bool credentialValid = false;

            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user);
                credentialValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }

            if (credentialValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                    });

                DateTime createDate = DateTime.Now;
                DateTime expiration = createDate + TimeSpan.FromSeconds(_token.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expiration, handler);

                return SuccessObject(createDate, expiration, token);
            }

            return ExceptionObject();
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expiration, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                SigningCredentials = _sign.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expiration
            });

            return handler.WriteToken(securityToken);
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Autenticação falhou"
            };
        }

        private object SuccessObject(DateTime create, DateTime expiration, string token)
        {
            return new
            {
                autenticated = true,
                created = create.ToString("yyy-MM-dd HH:mm:ss"),
                expiration = expiration.ToString("yyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "Ok"
            };
        }
    }
}
