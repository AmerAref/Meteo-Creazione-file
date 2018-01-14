using System.ComponentModel.DataAnnotations;
namespace Meteo.Services.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string RoleType { get; set; }
    }
}
