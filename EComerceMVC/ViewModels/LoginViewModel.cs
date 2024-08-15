using System.ComponentModel.DataAnnotations;

namespace EComerceMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20, ErrorMessage ="Tối đa 20 kí tự")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
