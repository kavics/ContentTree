using SenseNet.Client;
using SenseNet.Tools.CommandLineArguments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTree
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { "-s:https://demo.sensenet.com", "-i:\"..\"" };

            var settings = new Settings();
            try
            {
                var result = ArgumentParser.Parse(args, settings);
                if (result.IsHelp)
                    Console.WriteLine(result.GetHelpText());
                else
                    Run(settings);
            }
            catch (ParsingException e)
            {
                Console.WriteLine(e.FormattedMessage);
                Console.WriteLine(e.Result.GetHelpText());
            }

            if (Debugger.IsAttached)
            {
                Console.Write("Press <enter> to exit ...");
                Console.ReadLine();
            }
        }

        private static void Run(Settings settings)
        {
            var servers = settings.SiteUrls
                .Select(u => new ServerContext { Url = u, Username = "admin", Password = "admin" })
                .ToArray();
            ClientContext.Initialize(servers);

            var contents = GetContentTypeTreeAsync().Result;
            var forest = CreateForest(contents);
            DisplayForest(forest, settings);
        }

        private async static Task<IEnumerable<Content>> GetContentTypeTreeAsync()
        {
            return await Content.QueryAsync("+TypeIs:ContentType",
                select: new[] { "Id", "Name", "Path", "DisplayName", "Description" },
                settings: new QuerySettings
                {
                    EnableAutofilters = FilterStatus.Disabled,
                });
        }

        private static string _contentTypeRoot = "/Root/System/Schema/ContentTypes";
        private static TreeNode[] CreateForest(IEnumerable<Content> contents)
        {
            var treeNodes = contents.Select(c => new TreeNode { Content = c }).ToList();
            var rootNodes = new List<TreeNode>();
            foreach(var treeNode in treeNodes)
            {
                var parentPath = RepositoryPath.GetParentPath(treeNode.Content.Path);
                if (parentPath == _contentTypeRoot)
                {
                    rootNodes.Add(treeNode);
                }
                else
                {
                    var parent = treeNodes.FirstOrDefault(t => t.Content.Path == parentPath);
                    if (parent != null)
                        treeNode.SetParent(parent);
                }
            }
            return rootNodes.ToArray();
        }

        private static void DisplayForest(TreeNode[] forest, Settings settings)
        {
            foreach (var tree in forest)
                DisplayTree(tree, "", settings);
        }
        private static void DisplayTree(TreeNode tree, string level, Settings settings)
        {
            Console.Write(level);
            Console.WriteLine(tree.Name);
            var childLevel = level + settings.Indent;
            foreach (var child in tree.Children)
                DisplayTree(child, childLevel, settings);
        }
    }
}
