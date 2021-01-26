using strange.extensions.context.impl;
using UnityEngine;

public class MainContext : MVCSContext
{
    public MainContext() : base()
    {
    }

    public MainContext(MonoBehaviour view, bool autoStartup)
        : base(view, autoStartup)
    {
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<ReturnToTheMainMenuSceneSignal>()
            .ToSingleton()
            .CrossContext();

        injectionBinder.Bind<EnableContextViewSignal>()
            .ToSingleton();

        injectionBinder.Bind<DisableContextViewSignal>()
            .ToSingleton();
        
        injectionBinder.Bind<ReturnToTheMainMenuSignal>()
            .ToSingleton();

        injectionBinder.Bind<NoInternetConnectionSignal>()
            .ToSingleton();

        injectionBinder.Bind<PlaySceneLoadedSignal>()
            .ToSingleton();

        injectionBinder.Bind<ButtonClickedSignal>()
            .ToSingleton();
        
        injectionBinder.Bind<ChangeScreenToLoadingSignal>()
            .ToSingleton();

        injectionBinder.Bind<UpdateLoadingProgressSignal>()
            .ToSingleton();

        injectionBinder.Bind<IAssetBundleProvider>()
            .Bind<IAssetBundleLoader>()
            .To<AssetBundleProvider>()
            .ToSingleton()
            .CrossContext();

        injectionBinder.Bind<IAudioManager>()
            .Bind<IAudioSettingsProvider>()
            .To<AudioManager>()
            .ToSingleton();

        commandBinder.Bind<ReturnToTheMainMenuSceneSignal>()
            .To<ReturnToTheMainMenuSceneCommand>();

        commandBinder.Bind<StartLoadPlaySceneSignal>()
            .To<StartLoadPlaySceneCommand>();

        commandBinder.Bind<ChangeAudioSettingsSignal>()
            .To<ChangeAudioSettingsCommand>();

        commandBinder.Bind<ContextStartSignal>()
            .To<SetScreenOrientationSettingsCommand>()
            .To<InitializeViewCommand>()
            .To<InitializeAudioCommand>()
            .InSequence()
            .Once();
    }
}
