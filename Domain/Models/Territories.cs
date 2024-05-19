using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Territories : BaseEntities
    {
        [Key]
        public string TerritoryID { get; set; }
        [ForeignKey("RegionID")]
        public int RegionID { get; set; }
        public string? TerritoryDescription { get; set; }

        //public Region? Region { get; set; }
        
    }
}
