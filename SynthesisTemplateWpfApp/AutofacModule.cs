using Autofac;
using Noggog.Autofac;
using SynthesisTemplateWpfApp.Engine.Singletons;
using SynthesisTemplateWpfApp.ViewModels.Singletons;

namespace SynthesisTemplateWpfApp;

/// <summary>
/// A class with instructions for how the Inversion of Control container
/// should wire things up
/// </summary>
public class AutofacModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register all classes in engine's singleton area as singletons
        builder.RegisterAssemblyTypes(typeof(RunPatchLogic).Assembly)
            .InNamespacesOf(
                typeof(RunPatchLogic))
            .AsImplementedInterfaces()
            .AsSelf()
            .SingleInstance();
        
        // Register all classes in engine's singleton area as singletons
        builder.RegisterAssemblyTypes(typeof(MainVm).Assembly)
            .InNamespacesOf(
                typeof(MainVm))
            .AsImplementedInterfaces()
            .AsSelf()
            .SingleInstance();
    }
}