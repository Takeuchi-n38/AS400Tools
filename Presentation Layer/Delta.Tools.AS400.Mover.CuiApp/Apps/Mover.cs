using Delta.AS400.Libraries;
using Delta.Modernization;
using Delta.Tools.AS400.Configs;

using Delta.Tools.Modernization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Mover.Apps
{
    class Mover : IHostedService
    {
        readonly PathResolver PathResolver;
        readonly Library TargetLibrary;

        public Mover(IToolConfig toolConfig,Library TargetLibrary)
        {
            this.PathResolver= toolConfig.PathResolver;
            this.TargetLibrary= TargetLibrary;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            var usings = new StringBuilder();
            var persPath = Path.Combine(PathResolver.SolutionsDirectory.FullName, @"Delta.AS400.Tools\Presentation Layer\Delta.Tools.AS400.Mover.CuiApp");
            Directory.GetFiles(persPath).Where(fn => fn.EndsWith("cs") && !fn.Contains("Context") && !fn.Contains("Mover.cs") && !fn.Contains("Program.cs")).ToList().ForEach(filePath =>
            {
                var fileName = Path.GetFileName(filePath).Split('.')[0];
                var toFolderPath = Path.Combine(PathResolver.DomainFolderPath(TargetLibrary), $"{fileName}s");
                if (!Directory.Exists(toFolderPath))
                {
                    Directory.CreateDirectory(toFolderPath);
                }
                var replaceNamespace = $"Delta.AS400.{TargetLibrary.Partition.Name.ToPascalCase()}.{TargetLibrary.Name.ToPascalCase()}.{fileName}s";
                var contents = File.ReadAllText(filePath).Replace($"namespace Delta.Tools.AS400.Mover", "namespace " + replaceNamespace);
                var toPath = $"{toFolderPath}\\{fileName}.cs";
                File.WriteAllText(toPath, contents);

                usings.AppendLine("using " + replaceNamespace + ";");
            });

            Debug.Write(usings.ToString());//for paste to dbcontext
            Console.WriteLine("Moved.");
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
