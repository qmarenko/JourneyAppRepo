using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class Journey : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(70)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        [MaxLength(100)]
        [Required]
        public string UserLogin { get; set; }

        public bool IsDeleted { get; set; }
    }
}