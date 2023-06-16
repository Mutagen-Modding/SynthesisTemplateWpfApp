using System.Threading.Tasks;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynthesisTemplateWpfApp.Engine;

public class RunPatchLogic
{
    public async Task Run(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
    {
        // Normal patcher logic you would have in a patcher without a custom UI
    }
}