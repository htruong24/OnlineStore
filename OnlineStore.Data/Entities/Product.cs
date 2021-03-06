﻿namespace OnlineStore.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            ProductPhotos = new HashSet<ProductPhoto>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Đường dẫn")]
        public string MetaTitle { get; set; }

        [DisplayName("Mô tả ngắn")]
        public string ShortDescrition { get; set; }

        [AllowHtml]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Giá")]
        public decimal? Price { get; set; }

        [DisplayName("Hiển thị sản phẩm")]
        public bool? Publish { get; set; }

        [DisplayName("Sản phẩm nổi bật")]
        public bool? Featured { get; set; }

        [StringLength(50)]
        [DisplayName("Màu")]
        public string Color { get; set; }

        [DisplayName("Trạng thái")]
        public int? StatusId { get; set; }

        [DisplayName("Danh mục")]
        public int? SubCategoryId { get; set; }

        [DisplayName("Nhãn hiệu")]
        public int? BrandId { get; set; }

        [DisplayName("Đơn vị tính")]
        public int? UnitId { get; set; }

        [DisplayName("Nhà cung cấp")]
        public int? SupplierId { get; set; }

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

        [ForeignKey("SubCategoryId")]
        [DisplayName("Danh mục")]
        public virtual SubCategory SubCategory { get; set; }

        [ForeignKey("BrandId")]
        [DisplayName("Nhãn hiệu")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("UnitId")]
        [DisplayName("Đơn vị tính")]
        public virtual Unit Unit { get; set; }

        public int? NumberOfClicks { get; set; }

        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
