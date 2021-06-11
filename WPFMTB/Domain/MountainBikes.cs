using System;
using System.Collections.Generic;

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
