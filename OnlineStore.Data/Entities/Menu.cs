namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [StringLength(200)]
        [DisplayName("Tên")]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [StringLength(500)]
        [DisplayName("Đường dẫn")]
        public string Url { get; set; }

        [DisplayName("Thứ tự")]
        public int? OrderNumber { get; set; }

        [DisplayName("Kích hoạt")]
        public bool? Active { get; set; }

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
    }
}
