using SenseNet.Tools.CommandLineArguments;
using System;
// ReSharper disable UnusedMember.Local

namespace ContentTree
{
    public class Settings
    {
        public string[] SiteUrls { get; private set; }
        private string _siteUrlArg;
        [CommandLineArgument(name: "Site", required: true, aliases: "S", helpText: "Comma separated url list (e.g.: 'http://mysite1,http://mysite1').")]
        private string SiteUrlArg
        {
            get => _siteUrlArg;
            set
            {
                _siteUrlArg = value;

                var urls = value.Trim('\'', '"').Split(';');
                if (urls.Length == 1 && string.IsNullOrEmpty(urls[0]))
                    throw new ArgumentException("Invalid site urls.", "Site");

                SiteUrls = urls;
            }
        }

        public string Indent { get; private set; } = "  ";
        private string _indentArg;
        [CommandLineArgument(name: "Indent", required: false, aliases: "I", helpText: "Indent characters. Default: \"  \"")]
        private string IndentArg {
            get => _indentArg;
            set
            {
                _indentArg = value;
                Indent = value.Trim('\'', '"');
            }
        }
    }
}
