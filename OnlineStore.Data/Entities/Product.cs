namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string ShortDescrition { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public bool? Active { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public int? StatusId { get; set; }

        public int? SubCategoryId { get; set; }

        public int? BrandId { get; set; }

        public int? UnitId { get; set; }

        public int? SupplierId { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }
    }
}
