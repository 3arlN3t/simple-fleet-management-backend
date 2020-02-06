using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagementAPI.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ChassisId { get; set; }
        public Chassis Chassis { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        public byte NumberOfPassengers { get; set; }
        [Required]
        [StringLength(50)]
        public string Color { get; set; }
    }
}
