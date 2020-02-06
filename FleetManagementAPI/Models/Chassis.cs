using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagementAPI.Models
{
    public class Chassis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string Series { get; set; }
        [Required]
        public byte Number { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
