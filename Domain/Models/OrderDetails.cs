using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderDetails : BaseEntities
    {
        [Key]
        public int? OrderID { get; set; }
        [ForeignKey("ProductID")]
        public int? ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? Quantity { get; set; }
        public float? Discount { get; set; }
        
    }
}
