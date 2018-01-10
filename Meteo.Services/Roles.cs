using System.ComponentModel.DataAnnotations;
namespace Meteo.Services
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string RoleType { get; set; }
    }
}