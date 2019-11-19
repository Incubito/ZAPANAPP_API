using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using CreatorAPI.Models;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

namespace CreatorAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private string GeneratedPassword;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            byte[] usrdata = System.Convert.FromBase64String(context.UserName);
            string Usrbase64Decoded = System.Text.UTF8Encoding.UTF8.GetString(usrdata);
            byte[] pwddata = System.Convert.FromBase64String(context.Password);
            string Pwdbase64Decoded = System.Text.UTF8Encoding.UTF8.GetString(pwddata);

            try
            {
                IncubitoCryptoGraphy.IncubitoCrypto EncryptedUUID = new IncubitoCryptoGraphy.IncubitoCrypto(Usrbase64Decoded, true);
                string DecryptedUIID = EncryptedUUID.StringToStringDecryption();

                DecryptedUIID = DecryptedUIID.Replace("-", "");
                string UUIDEmail = DecryptedUIID + "@cubitsuite.com";

                IncubitoCryptoGraphy.IncubitoCrypto EncryptedPassword = new IncubitoCryptoGraphy.IncubitoCrypto(Pwdbase64Decoded, true);
                string DecryptedPassword = EncryptedPassword.StringToStringDecryption();

                string Handshake = context.Request.Headers.GetValues("Handshake")[0];
                IncubitoCryptoGraphy.IncubitoCrypto EncryptedHandshake = new IncubitoCryptoGraphy.IncubitoCrypto(Handshake, true);
                string DecryptedHandshake = EncryptedHandshake.StringToStringDecryption();

                ApplicationUser user;
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

                if (DecryptedHandshake == DecryptedUIID)
                {
                    if (DecryptedPassword == "")
                    {
                        byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(UUIDEmail.ToLower());
                        RijndaelManaged Encoder = new RijndaelManaged();
                        Encoder.Mode = CipherMode.CBC;
                        Encoder.Padding = PaddingMode.PKCS7;
                        Encoder.KeySize = 128;
                        Encoder.BlockSize = 128;
                        Encoder.Key = System.Text.Encoding.UTF8.GetBytes("qs9em6$%#MXMswPB");
                        Encoder.IV = System.Text.Encoding.UTF8.GetBytes("a7Tqxpd()+HVmVpE");

                        byte[] Encoded1 = Encoder.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                        GeneratedPassword = Convert.ToBase64String(Encoded1);

                        var UserRegistration = await userManager.FindByEmailAsync(UUIDEmail);

                        if (UserRegistration == null)
                        {
                            var NewUser = new ApplicationUser() { UserName = UUIDEmail, Email = UUIDEmail };
                            IdentityResult result = await userManager.CreateAsync(NewUser, GeneratedPassword);
                        }
                        else
                        {
                            var token = await userManager.GeneratePasswordResetTokenAsync(UserRegistration.Id);
                            var result = await userManager.ResetPasswordAsync(UserRegistration.Id, token, GeneratedPassword);
                        }

                        user = await userManager.FindAsync(UUIDEmail, GeneratedPassword);
                    }
                    else
                    {
                        GeneratedPassword = "Login Sucessfull";
                        user = await userManager.FindAsync(UUIDEmail, DecryptedPassword);
                    }

                    if (user == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }

                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                       OAuthDefaults.AuthenticationType);

                    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = CreateProperties(user.UserName);
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesIdentity);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {            
            IncubitoCryptoGraphy.IncubitoCrypto EncryptedHandshake = new IncubitoCryptoGraphy.IncubitoCrypto(GeneratedPassword, true);

            string EncryptedHandshakeResponse = EncryptedHandshake.StringToStringEncryption();

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(EncryptedHandshakeResponse);
            string Base64Pwd = Convert.ToBase64String(byt);

            context.AdditionalResponseParameters.Add("Handshake Response", Base64Pwd);
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}