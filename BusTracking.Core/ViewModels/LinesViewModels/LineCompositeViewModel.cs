using BusTracking.Core.Dtos.LineDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Core.ViewModels.LinesViewModels
{
    public class LineCompositeViewModel
    {
        public List<LineViewModel> ListLines { get; set; }
        public LineViewModel Line { get; set; }
        public CreateLineDto CreateDto { get; set; }
        public UpdateLineDto UpdateDto { get; set; }
    }
}
