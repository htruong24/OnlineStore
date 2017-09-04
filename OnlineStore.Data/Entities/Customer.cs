namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;

    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            ShippingAddresses = new HashSet<ShippingAddress>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [NotMapped]
        [DisplayName("Mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DisplayName("Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        [DisplayName("Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DisplayName("Giới tính")]
        public int? Gender { get; set; }

        [DisplayName("Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [DisplayName("Điện thoại bàn")]
        [StringLength(20)]
        public string CellPhone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [NotMapped]
        [DisplayName("Email hiện tại")]
        public string CurrentEmail { get; set; }

        [NotMapped]
        [DisplayName("Email mới")]
        public string NewEmail { get; set; }

        [DisplayName("Fax")]
        [StringLength(50)]
        public string Fax { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Ngày tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(30)]
        [DisplayName("Người tạo")]
        public string CreatedById { get; set; }

        [DisplayName("Ngày cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ModifiedOn { get; set; }

        [StringLength(30)]
        [DisplayName("Người cập nhật")]
        public string ModifiedById { get; set; }

        [ForeignKey("CreatedById")]
        [DisplayName("Người tạo")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("ModifiedById")]
        [DisplayName("Người cập nhật")]
        public virtual User ModifiedBy { get; set; }

        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}
