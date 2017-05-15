namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(50)]
        public string ThumbnailUrl { get; set; }

        [StringLength(50)]
        public string Tite { get; set; }

        public int? AlbumId { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }
    }
}
