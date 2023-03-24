using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace monacos.us_mvc.Models
{
    public class ContactWorkflow_Model
    {


        [Required]
        public string FirstName
        {
            get;
            set;

        }

        [Required]
        public string LastName
        {
            get;
            set;

        }

        [Required]
        public string EmailAddress
        {
            get;
            set;

        }

        [Required]
        public string CommunicationContent
        {
            get;
            set;

        }


    }
}
