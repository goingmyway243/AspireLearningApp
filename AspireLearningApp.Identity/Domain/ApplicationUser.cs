using Microsoft.AspNetCore.Identity;

namespace AspireLearningApp.Identity.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
    }
}
