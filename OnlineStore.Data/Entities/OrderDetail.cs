namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Hóa đơn")]
        public int? OrderId { get; set; }

        [DisplayName("Sản phẩm")]
        public int? ProductId { get; set; }

        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }

        [DisplayName("Giá")]
        public decimal? Price { get; set; }

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
        [ForeignKey("ProductId")]
        public virtual Order Order { get; set; }
    }
}
