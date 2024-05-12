using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Region : BaseEntities
    {
        [Key]
        public int? RegionID { get; set; }
        public string? RegionDescription { get; set; }

        public ICollection<Territories>? Territories { get; set; }
      
    }
}
