namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;

    [Table("Brand")]
    public partial class Brand
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Đường dẫn")]
        public string MetaTitle { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Danh mục")]
        public string SubCategoryIds { get; set; }

        [NotMapped]
        public List<SubCategory> SubCategoryList { get; set; }

        [NotMapped]
        [DisplayName("Danh mục")]
        public string SubCategoryDisplayText { get; set; }

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
