using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Services.Models
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public short Gender { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }
    }
}
