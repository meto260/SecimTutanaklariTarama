using SecimTutanaklariTarama.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecimTutanaklariTarama.Services {
    internal interface IParseActions {
        public GoogleModel ParseFromConfiguration();
        public ImmutableList<Dictionary<string, string>> ParseResultDocument(string document);

    }

    internal class ParseActions : IParseActions {
        public GoogleModel ParseFromConfiguration() {
            string readedFileSource = File.ReadAllText("google.json");
            return JsonSerializer.Deserialize<GoogleModel>(readedFileSource);
        }

        public ImmutableList<Dictionary<string, string>> ParseResultDocument(string document) {
            var result = new List<Dictionary<string, string>>();
            var inputJson = File.ReadAllText("lineFilters.json");
            var inputs = JsonSerializer.Deserialize<List<string>>(inputJson);
            var doclines = document.Split('\n').ToList();
            for (int i=0; i < doclines.Count; i++) {
                var item = new Dictionary<string, string>();
                foreach (string key in inputs) {
                    if (doclines[i].Contains(key)) {
                        item.Add(key, doclines[i+1].Replace(":", "").Trim());
                    }
                }
                result.Add(item);
            }
            return result.ToImmutableList();
        }
    }
}
