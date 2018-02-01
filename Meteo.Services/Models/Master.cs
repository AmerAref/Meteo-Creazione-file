using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Meteo.Services.Models
{
    public class Master
    {
        [Key]
        public int IdMaster { get; set; }
        public string Choice5DayOrNow { get; set; }
        public DateTime DateOfRequist { get; set; }

        [ForeignKey("Users")]
        public int IdUser { get; set; }

        [ForeignKey("Cities")]
        public int IdCity { get; set; }
    }
}