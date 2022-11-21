using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DCE_API.Models
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "ProductId")]
        public Guid ProductId { get; set; }

        [DataMember(Name = "ProductName")]
        public string ProductName { get; set; }

        [DataMember(Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        [DataMember(Name = "SupplierId")]
        public Guid SupplierId { get; set; }

        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "IsActive")]
        public int IsActive { get; set; }
    }
}
