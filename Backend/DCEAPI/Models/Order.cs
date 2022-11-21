using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DCE_API.Models
{
    [DataContract]
    public class Order
    {
        [DataMember(Name = "OrderId")]
        public Guid OrderId { get; set; }

        [DataMember(Name = "ProductId")]
        public Guid ProductId { get; set; }

        [DataMember(Name = "OrderStatus")]
        public int OrderStatus { get; set; }

        [DataMember(Name = "OrderType")]
        public int OrderType { get; set; }

        [DataMember(Name = "OrderBy")]
        public Guid OrderBy { get; set; }

        [DataMember(Name = "OrderedOn")]
        public DateTime OrderedOn { get; set; }

        [DataMember(Name = "ShippedOn")]
        public DateTime ShippedOn { get; set; }

        [DataMember(Name = "IsActive")]
        public int IsActive { get; set; }
    }
}
