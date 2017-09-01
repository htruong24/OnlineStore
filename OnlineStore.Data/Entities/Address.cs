namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Value { get; set; }

        [StringLength(150)]
        public string PostalCode { get; set; }

        public int? CityId { get; set; }

        public int? CustomerId { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(30)]
        public string CreatedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(30)]
        public string ModifiedById { get; set; }
    }
}
