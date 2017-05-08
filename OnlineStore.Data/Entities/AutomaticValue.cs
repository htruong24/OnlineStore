namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AutomaticValue")]
    public partial class AutomaticValue
    {
        [Key]
        [StringLength(50)]
        public string TableName { get; set; }

        [StringLength(50)]
        public string Prefix { get; set; }

        public int? Length { get; set; }

        [StringLength(50)]
        public string LastValue { get; set; }

        [StringLength(1)]
        public string Character { get; set; }
    }
}
