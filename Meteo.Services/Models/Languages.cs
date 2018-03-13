using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Meteo.Services.Models
{
    public class Languages
    {
        [Key]
        public int IdLanguage { get; set; }

        public string LanguageType { get; set; }
    }
}
