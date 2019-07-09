using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vsol.Api.AppSecurity.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.Shared.Domain;
using System.Net.Http;
using Vsol.Api.Shared.Infra.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;

namespace Vsol.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserApplicationService userApp;

        public AccountController(IUserApplicationService userApp)
        {
            this.userApp = userApp;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserAuthenticationCommand authentication)
        {
            NotificationResult result = new NotificationResult();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSettings.Site.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var contentData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", authentication.Username),
                    new KeyValuePair<string, string>("password", authentication.Password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });

                var response = await client.PostAsync("connect/token", contentData);

                var str = await response.Content.ReadAsStringAsync();
                var tokenAuthentication = JsonConvert.DeserializeObject<UserTokenCommandResult>(str);

                if (response.IsSuccessStatusCode)
                    result.Data = tokenAuthentication;
                else
                    result.AddError(tokenAuthentication.Error_description);
            }

            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody]UserRefreshTokenCommand authentication)
        {
            NotificationResult result = new NotificationResult();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSettings.Site.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var contentData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("refresh_token", authentication.Refresh_token),
                    new KeyValuePair<string, string>("grant_type", "refresh_token")
                });

                var response = await client.PostAsync("connect/token", contentData);

                string str = await response.Content.ReadAsStringAsync();
                var tokenAuthentication = JsonConvert.DeserializeObject<UserTokenCommandResult>(str);

                if (response.IsSuccessStatusCode)
                    result.Data = tokenAuthentication;
                else
                    result.AddError(tokenAuthentication.Error_description);
            }

            return Ok(result);
        }

        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            Guid idUser = base.IdUser.Value;
            var user = await userApp.GetBasicByIdAsync(idUser);
            return Ok(user);
        }
    }
}