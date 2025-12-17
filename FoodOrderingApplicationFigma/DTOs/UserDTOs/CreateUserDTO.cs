using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]{2,}$", ErrorMessage = "Full Name must start with a capital letter and contain only letters and spaces")]
        public string FullName { get; set; } = "";
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage="Please enter a valid email address")]
        public string Email { get; set; } = "";
        [Required]
        [RegularExpression(@"^[6-9][0-9]{9}$",ErrorMessage ="Phone number must follow pattern of starting with 6-9 and 9 digits")]
        public string Phone { get; set; } = "";
        [Required]
        public int Role { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#^()[\]{}<>~`_+=|:;.,\/-])[A-Za-z\d@$!%*?&#^()[\]{}<>~`_+=|:;.,\/-]{8,12}$",ErrorMessage ="Password must have minimum 8 charcters and max 12 chaarcters should contain a capital letter,small letter,special character,a digit")]
        public string Password { get; set; } = "";
        public bool? isActive {  get; set; }=true;
    }
}
