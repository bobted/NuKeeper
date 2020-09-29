using System.Threading.Tasks;
using NuKeeper.Abstractions.Configuration;

namespace NuKeeper.Local
{
    public interface ILocalEngine
    {
        Task Run(ISettingsContainer settings, bool write);
    }
}
