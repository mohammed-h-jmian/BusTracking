using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.Dtos.APIDtos
{
    /// <summary>
    /// DTO for setting the BusId in a line using the LineCode.
    /// </summary>
    public class SetBusInLineCodeDto
    {
        /// <summary>
        /// The code of the line.
        /// </summary>
        [Required]
        public string LineCode { get; set; }

        /// <summary>
        /// The ID of the bus.
        /// </summary>
        [Required]
        public int BusId { get; set; }
    }

    /// <summary>
    /// DTO for setting the BusId in a line.
    /// </summary>
    public class SetBusInLineIdDto
    {
        /// <summary>
        /// The ID of the line.
        /// </summary>
        [Required]
        public int LineId { get; set; }
        /// <summary>
        /// The ID of the bus.
        /// </summary>
        [Required]
        public int BusId { get; set; }
    }
}
