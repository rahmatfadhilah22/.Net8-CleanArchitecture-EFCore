using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Territories : BaseEntities
    {
        [Key]
        public string? TerritoryID { get; set; }
        [ForeignKey("RegionID")]
        public int? RegionID { get; set; }
        public string? TerritoryDescription { get; set; }
        
    }
}
