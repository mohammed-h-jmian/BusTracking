using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusTracking.Core.Dtos.StopPointDtos
{
    public class UpdateStopPointDto
    {

        [Required(ErrorMessage = "The Stop Point Id field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Stop Point Name field is required.")]
        [Display(Name = "Stop Point Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Latitude field is required.")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "The Longitude field is required.")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
        [Required(ErrorMessage = "The Address field is required.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        [Display(Name = "City")]
        public int CityId { get; set; }
    }
}
