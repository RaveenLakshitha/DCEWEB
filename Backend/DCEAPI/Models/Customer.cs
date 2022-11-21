using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DCE_API.Models
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "Id")]
        public Guid UserId { get; set; }

        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "IsActive")]
        public int IsActive { get; set; }


    }
}
