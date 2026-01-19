using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGruppÖvning.Models
{
    public class AzureOpenAiSettings
    {

        public string Endpoint { get; set; } = App.Config["ExternalApi:Endpoint"] ?? string.Empty;
        public string ApiKey { get; set; } = App.Config["ExternalApi:ApiKey"] ?? string.Empty;
        public string DeploymentName { get; set; } = App.Config["ExternalApi:Deployment"] ?? string.Empty;
        public string ApiVersion { get; set; } = string.Empty;
    }
}
