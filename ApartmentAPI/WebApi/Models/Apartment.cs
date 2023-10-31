using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }

    }
}
