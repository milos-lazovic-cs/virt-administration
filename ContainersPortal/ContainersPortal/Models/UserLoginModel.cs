

using System.ComponentModel.DataAnnotations;

namespace ContainersPortal.Models;

public class UserLoginModel
{
    [Required]
    //[EmailAddress]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}