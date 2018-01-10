using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Meteo.Services.Models
{
    public class Master
    {
        [Key]
        public int IdMaster { get; set; }
        public bool Choice5DayOrNow { get; set; }
        public DateTime DateOfRequist { get; set; }

        [ForeignKey("Users")]
        int IdUser { get; set; }
        public User User { get; set; }

        [ForeignKey("Cities")]
        int IdCity { get; set; }
        public City Cities { get; set; }

    }
}