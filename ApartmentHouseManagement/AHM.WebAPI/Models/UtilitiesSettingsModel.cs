using System.ComponentModel.DataAnnotations;

namespace AHM.WebAPI.Models
{
    public class UtilitiesSettingsModel
    {
        [Required]
        public double FinePercent { get; set; }

        [Range(1, 29)]
        public int LastPayUtilitiesDay { get; set; }
    }
}