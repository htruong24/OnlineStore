namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecommendProduct")]
    public partial class RecommendProduct
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? NumberOfClicks { get; set; }
    }
}
