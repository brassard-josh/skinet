using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {
        private const string REGEX_PATTERN = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";
        private const string PWD_REGEX_ERROR_MESSAGE = "Password must have at least 1 uppercase, 1 lowercase, 1 numeric and 1 special character and be at least 6 characters long";

        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(REGEX_PATTERN, ErrorMessage = PWD_REGEX_ERROR_MESSAGE)]
        public string Password { get; set; }
    }
}