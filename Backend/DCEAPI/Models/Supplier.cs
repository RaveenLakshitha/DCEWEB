using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DCE_API.Models
{
    [DataContract]
    public class Supplier
    {
        [DataMember(Name = "SupplierId")]
        public Guid SupplierId { get; set; }

        [DataMember(Name = "SupplierName")]
        public string SupplierName { get; set; }

        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "IsActive")]
        public int IsActive { get; set; }
    }
}
