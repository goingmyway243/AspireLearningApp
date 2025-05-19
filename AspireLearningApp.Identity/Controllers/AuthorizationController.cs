using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace AspireLearningApp.Identity.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost("~/connect/token"), Produces("application/json")]
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
            else
            {
                return Task.FromResult<IActionResult>(BadRequest(new { error = "unsupported_grant_type" }));
            }
        }
    }
}
