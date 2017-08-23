namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [Table("User")]
    public partial class User
    {
        [Key]
        [StringLength(30)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public int? Gender { get; set; }

        [DisplayName("Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(20)]
        public string CellPhone { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string EmailPassword { get; set; }

        [StringLength(30)]
        public string GroupId { get; set; }

        [StringLength(30)]
        public string TypeId { get; set; }

        [StringLength(50)]
        public string TaxCode { get; set; }

        public bool? Active { get; set; }

        public string Image { get; set; }

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

        //[ForeignKey("CreatedById")]
        //[DisplayName("Người tạo")]
        //public virtual User CreatedBy { get; set; }

        //[ForeignKey("ModifiedById")]
        //[DisplayName("Người cập nhật")]
        //public virtual User ModifiedBy { get; set; }
    }
}
