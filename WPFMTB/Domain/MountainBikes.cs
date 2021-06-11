using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPFMTB.Domain
{
    public partial class MountainBikes
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string TireSize { get; set; }
        public string FrameMaterial { get; set; }
        public bool HasSuspension { get; set; }
        public string FrameSize { get; set; }
    }
}
