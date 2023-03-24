using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace monacos.us.dtos
{
   
    [DataContract(Name = "Content")]
    public class ContentDTO
    {
        [DataMember(Name = "Content_ID", Order = 1)]
        public Int32 Content_ID { get; set; }
        [DataMember(Name = "ContentArea_ID", Order = 2)]
        public Int32 ContentArea_ID { get; set; }
        [DataMember(Name = "ContentValue", Order = 3)]
        public String ContentValue { get; set; }
        [DataMember(Name = "Create_Date", Order = 4)]
        public String Create_Date { get; set; }
        [DataMember(Name = "Publish_Date", Order = 5)]
        public String Publish_Date { get; set; }
        [DataMember(Name = "Expiration_Date", Order = 6)]
        public String Expiration_Date { get; set; }
        [DataMember(Name = "Description", Order = 7)]
        public string Description { get; set; }
        [DataMember(Name = "Active", Order = 8)]
        public bool Active { get; set; }
    }

}
