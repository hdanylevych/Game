
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadPlaySceneCommand : Command
{
    private readonly string AssetBundleURL = "https://srv-store5.gofile.io/download/uAuuKx/gamelvl";

    [Inject] public IAssetBundleLoader AssetBundleLoader { get; set; }
    [Inject] public IAssetBundleProvider BundleProvider { get; set; }
    [Inject] public DisableContextViewSignal DisableContextViewSignal { get; set; }
    [Inject] public PlaySceneLoadedSignal PlaySceneLoadedSignal { get; set; }
    [Inject] public NoInternetConnectionSignal NoInternetConnectionSignal { get; set; }
    [Inject] public ReturnToTheMainMenuSignal ReturnToTheMainMenuSignal { get; set; }

    public override void Execute()
    {
        if (AssetBundleProvider.NetworkReachability == NetworkReachability.NotReachable &&
            BundleProvider.AssetBundles.ContainsKey("gamelvl") == false)
        {
            ReturnToTheMainMenuSignal.Dispatch();
            NoInternetConnectionSignal.Dispatch();
            return;
        }

        if (AssetBundleLoader.LoadWebBundle("gamelvl", AssetBundleURL))
        {
            SceneManager.sceneLoaded += (scene, loadSceneMode) =>
                {
                    PlaySceneLoadedSignal.Dispatch();
                    DisableContextViewSignal.Dispatch();
                };

            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        else
        {
            ReturnToTheMainMenuSignal.Dispatch();
        }
    }
}
