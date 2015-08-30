using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyScavenger.Models
{
    public class TechnologyViewModel
    {
        [Required]
        public string Name { get; set; }

        public List<string> URLs { get; set; }
    }
}