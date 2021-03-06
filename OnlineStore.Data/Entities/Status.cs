namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? StatusGroupId { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }
    }
}
