using System.ComponentModel.DataAnnotations;

namespace Meteo.Services
{
    public class Question
    {
        public Question() { }

        [Key]
        public int IdDomanda{ get; set; }
        public string Domande { get; set; }

    }
}