using SecimTutanaklariTarama.Models;
using Google.Cloud.DocumentAI.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.WireFormat;
using static Google.Rpc.Context.AttributeContext.Types;

namespace SecimTutanaklariTarama.Services {
    internal interface IDocProcess {
        public ProcessResponse CreateProcess(string localPath);
    }

    internal class DocProcess : IDocProcess {
        DocumentProcessorServiceClient client;
        GoogleModel configs;
        static ProcessResponse processResponse;
        public DocProcess(GoogleModel _configs) {
            configs = _configs;
            client = new DocumentProcessorServiceClientBuilder {
                Endpoint = $"{configs.locationId}-documentai.googleapis.com"
            }.Build();
        }

        public ProcessResponse CreateProcess(string localPath) {
            using var fileStream = File.OpenRead(localPath);
            var rawDocument = new RawDocument {
                Content = ByteString.FromStream(fileStream),
                MimeType = configs.mimeType
            };

            var request = new ProcessRequest {
                Name = ProcessorName.FromProjectLocationProcessor(
                    configs.projectId, 
                    configs.locationId, 
                    configs.processorId
                ).ToString(),
                RawDocument = rawDocument
            };
            processResponse = client.ProcessDocument(request);
            return processResponse;
        }
    }
}
