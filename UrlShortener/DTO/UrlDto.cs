using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.DTO
{
    public class UrlDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Url { get; set; }
    }
}
