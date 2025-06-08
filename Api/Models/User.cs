using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class User : IdentityUser
    {
        public int Age { get; set; }

        [MaxLength(100)]
        public string Nickname { get; set; } = string.Empty;
    }
}
