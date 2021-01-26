
using strange.extensions.command.impl;
using strange.extensions.context.api;

using UnityEngine;

public class InstantiateObjectsFromBundleCommand : Command
{
    [Inject] public IAssetBundleProvider AssetBundleProvider { get; set; }
    [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject ContextRoot { get; set; }

    public override void Execute()
    {
        if (AssetBundleProvider.AssetBundles.ContainsKey("gamelvl"))
        {
            var gameLVL = AssetBundleProvider.AssetBundles["gamelvl"].LoadAsset("LVL");
            GameObject.Instantiate(gameLVL, ContextRoot.transform);

            var canvasGO = AssetBundleProvider.AssetBundles["gamelvl"].LoadAsset("GamePlayHUD");
            GameObject.Instantiate(canvasGO, ContextRoot.transform);


        }
    }
}
