using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntershipTaskApi.Models
{
    public class Item
    {
        [Key]
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public double Rate { get; set; }
    }
}