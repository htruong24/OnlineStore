namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Group")]
    public partial class Group
    {
        [StringLength(30)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }
    }
}
