using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployManager
{
    internal class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public string Created { get; set; }
    }
}
