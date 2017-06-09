namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [Table("Photo")]
    public partial class Photo
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public int? FileSize { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }

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

        [NotMapped]
        public bool? Featured { get; set; }

        [NotMapped]
        public string Status { get; set; }
    }
}
