
using strange.extensions.context.impl;

using UnityEngine;

public class PlayContext : MVCSContext
{
    public PlayContext() : base()
    {
    }

    public PlayContext(MonoBehaviour view, bool autoStartup)
        : base(view, autoStartup)
    {
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<CollectedCoinsUpdateSignal>()
            .ToSingleton();

        injectionBinder.Bind<IUpdateLoopController>()
            .To<UpdateLoopController>()
            .ToSingleton();

        injectionBinder.Bind<ICoinsController>()
            .To<CoinsController>()
            .ToSingleton();

        commandBinder.Bind<UnitModelInteractedSignal>()
            .To<ProcessInteractionCommand>();

        commandBinder.Bind<ContextStartSignal>()
            .To<InstantiateObjectsFromBundleCommand>()
            .To<InitializeUpdateLoopCommand>()
            .InSequence()
            .Once();
    }
}
