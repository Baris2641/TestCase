using System;
using System.ComponentModel.DataAnnotations;

namespace Alpata.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Document { get; set; }
    }
}
