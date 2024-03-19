using System.ComponentModel.DataAnnotations;

namespace RegLogin.Models
{
    public class UpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhNo { get; set; }
        //public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        //public string ConfirmPassword { get; set; }
    }
}
