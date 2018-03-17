using SenseNet.Tools.CommandLineArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTree
{
    public class Arguments
    {
        public string[] SiteUrls { get; private set; }
        private string _siteUrlArg;

        [CommandLineArgument(name: "Site", required: true, aliases: "S", helpText: "Comma separated url list (e.g.: 'http://mysite1,http://mysite1').")]
        private string SiteUrlArg
        {
            get { return _siteUrlArg; }
            set
            {
                _siteUrlArg = value;

                var urls = value.Split(';');
                if (urls.Length == 1 && string.IsNullOrEmpty(urls[0]))
                    throw new ArgumentException("Invalid site urls.");

                SiteUrls = urls;
            }
        }

        public string Indent { get; private set; } = "  ";
        [CommandLineArgument(name: "Indent", required: false, aliases: "I", helpText: "Indent characters. Default: \"  \"")]
        private string IndentArg {
            get { return Indent; }
            set { Indent = value.Trim('\'', '"'); }
        }
    }
}
