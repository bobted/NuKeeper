using System.Threading.Tasks;
using Microsoft;
using NuGet.Configuration;
using NuGet.Versioning;
using NuKeeper.Abstractions.NuGet;
using NuKeeper.Abstractions.RepositoryInspection;
using NuKeeper.Update.ProcessRunner;

namespace NuKeeper.Update.Process
{
    public class DotNetRestoreCommand : IDotNetRestoreCommand
    {
        private readonly IExternalProcess _externalProcess;

        public DotNetRestoreCommand(IExternalProcess externalProcess)
        {
            _externalProcess = externalProcess;
        }

        public Task Invoke(PackageInProject currentPackage, NuGetSources allSources)
        {
            Requires.NotNull(currentPackage, nameof(currentPackage));
            Requires.NotNull(allSources, nameof(allSources));

            var projectPath = currentPackage.Path.Info.DirectoryName;
            var projectFileName = currentPackage.Path.Info.Name;
            var sources = allSources.CommandLine("-s");

            var restoreCommand = $"restore {projectFileName} {sources}";
            return _externalProcess.Run(projectPath, "dotnet", restoreCommand, true);
        }

        public Task Invoke(PackageInProject currentPackage, NuGetVersion newVersion, PackageSource packageSource, NuGetSources allSources)
        {
            return Invoke(currentPackage, allSources);
        }
    }
}
