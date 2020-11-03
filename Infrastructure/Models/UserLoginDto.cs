using IntegrationApiSynchroniser.Infrastructure.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public class UserLoginDto
    {
        [Required, MaxLength(120)]
        public string Username { get; set; }

        [Required, MaxLength(120), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?"), DefaultValue(false)]
        public bool RememberMe { get; set; }

        public string Token { get; set; }

        public SignInresult Status { get; set; }
    }
}
