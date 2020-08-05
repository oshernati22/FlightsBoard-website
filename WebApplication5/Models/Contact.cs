using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [DisplayName("Name")]
        public String name { get; set; }
        [DisplayName("Email")]
        public String email { get; set; }
        [DisplayName("Subject")]
        public String subject { get; set; }
        [DisplayName("Message")]
        public String messenge { get; set; }
    }
}