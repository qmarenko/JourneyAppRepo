using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        [MaxLength(70)]
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(70)]
        public string ModifiedUser { get; set; }
    }
}