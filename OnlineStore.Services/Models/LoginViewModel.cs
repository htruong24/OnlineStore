using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Services.Models
{
    public class LoginViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ")]
        public bool RememberMe { get; set; }
    }
}
