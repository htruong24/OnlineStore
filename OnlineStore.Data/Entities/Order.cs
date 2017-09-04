namespace OnlineStore.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Mã số")]
        public string Code { get; set; }

        [DisplayName("Tên khách hàng")]
        [StringLength(30)]
        public string CustomerId { get; set; }

        [DisplayName("Tên khách hàng")]
        public string CustomerName { get; set; }

        [DisplayName("Đối tượng thanh toán")]
        public string PaymentObject { get; set; }

        [DisplayName("Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [DisplayName("Phương thức giao hàng")]
        public string DeliveryMethod { get; set; }

        [DisplayName("Số điện thoại")]
        public string Telephone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Thành phố")]
        public string City { get; set; }

        [DisplayName("Địa chỉ giao hàng")]
        public string Address { get; set; }

        [DisplayName("Mã bưu điện")]
        public string PostalCode { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }

        [DisplayName("Trạng thái")]
        public string Status { get; set; }

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

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
