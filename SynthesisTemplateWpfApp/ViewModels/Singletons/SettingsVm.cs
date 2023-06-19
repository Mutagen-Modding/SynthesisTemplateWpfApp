using System.Reactive.Linq;
using Noggog.WPF;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SynthesisTemplateWpfApp.ViewModels.Singletons;

public class SettingsVm : ViewModel
{
    [Reactive] public bool SomeBoolean { get; set; }

    [Reactive] public string SomeString { get; set; } = string.Empty;

    // Showing off RxUI's ability to combine other properties and make derivative properties
    private readonly ObservableAsPropertyHelper<string> _derivativeSummary;
    public string DerivativeSummary => _derivativeSummary.Value;

    public SettingsVm()
    {
        _derivativeSummary = Observable.CombineLatest(
                this.WhenAnyValue(x => x.SomeBoolean),
                this.WhenAnyValue(x => x.SomeString),
                (b, s) => $"Bool is {b}, String is {s}")
            .ToProperty(this, nameof(DerivativeSummary));
    }
}