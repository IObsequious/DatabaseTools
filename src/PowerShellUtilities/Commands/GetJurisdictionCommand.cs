using System;
using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using PowerShellUtilities.JsonModel;
using PowerShellUtilities.JsonModel.Jurisdictions;
using PowerShellUtilities.JsonModel.List;

namespace PowerShellUtilities.Commands
{
    [Cmdlet(VerbsCommon.Get, "Jurisdiction")]
    public class GetJurisdictionCommand : AbstractPSCmdlet
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter(ParameterSetName = "Pipeline", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public JurisdictionListDataItem InputObject { get; set; }

        protected override void ProcessRecord()
        {
            string json = string.Empty;

            if (ParameterSetName == "Pipeline")
            {
                json = InvokeWebRequest(InputObject);
            }
            else
            {
                json = InvokeWebRequest();
            }

            string directory = "C:\\ProgramData\\sped.mobi\\";

            Jurisdiction jurisdiction = JsonModelSerializer.DeserializeJurisdiction(json);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, Regex.Replace(jurisdiction.Data.Title, @"(\\|\/)", string.Empty) + ".json"), JObject.Parse(json).ToString());


            WriteObject(jurisdiction);
        }
    }
}
