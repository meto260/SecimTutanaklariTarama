using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecimTutanaklariTarama.Models {
    internal class GoogleModel {
        public string projectId { get; set; }
        public string locationId { get; set; } = "us";
        public string processorId { get; set; }
        public string localPath { get; set; }
        public string mimeType { get; set; } = "image/jpeg";
    }
}
