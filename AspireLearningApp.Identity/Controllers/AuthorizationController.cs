using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace AspireLearningApp.Identity.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost("/connect/token"), Produces("application/json")]
        public Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenIddict server request cannot be retrieved.");

            if (request.IsPasswordGrantType())
            {
                // Handle the password grant type.
                // Validate the user credentials and issue a token.
                // This is just a placeholder for demonstration purposes.
                return Task.FromResult<IActionResult>(Ok(new { access_token = "dummy_token" }));
            }
            else if (request.IsClientCredentialsGrantType())
            {
                // Handle the client credentials grant type.
                // Validate the client credentials and issue a token.
                // This is just a placeholder for demonstration purposes.
                return Task.FromResult<IActionResult>(Ok(new { access_token = "dummy_client_token" }));
            }
            else if (request.IsAuthorizationCodeGrantType())
            {
                // Handle the authorization code grant type.
                // Validate the authorization code and issue a token.
                // This is just a placeholder for demonstration purposes.
                return Task.FromResult<IActionResult>(Ok(new { access_token = "dummy_auth_code_token" }));
            }
            else
            {
                return Task.FromResult<IActionResult>(BadRequest(new { error = "unsupported_grant_type" }));
            }
        }

        [HttpGet("/test")]
        public async Task<IActionResult> TestToken()
        {
            var httpClient = new HttpClient();
            // Prepare the request parameters
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "password" },         // or "client_credentials", etc.
                { "username", "alice" },              // required for password flow
                { "password", "Pass123!" },           // required for password flow
                { "client_id", "my-client" },          // your client id
                { "client_secret", "my-secret" },   // if your client requires a secret
                { "scope", "api" }                   // requested scopes
            };

            // Create the request content
            var content = new FormUrlEncodedContent(parameters);


            var response = await httpClient.PostAsync("https://localhost:7022/connect/token", content);

            return Ok(response);
        }
    }
}
