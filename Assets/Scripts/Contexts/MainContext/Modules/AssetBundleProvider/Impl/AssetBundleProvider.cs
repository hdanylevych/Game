using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleProvider : IAssetBundleProvider, IAssetBundleLoader
{
    private readonly Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle>(3);

    public static NetworkReachability NetworkReachability => Application.internetReachability;

    public IReadOnlyDictionary<string, AssetBundle> AssetBundles => _assetBundles;

    [Inject] public UpdateLoadingProgressSignal UpdateLoadingProgressSignal { get; set; }

    public bool LoadWebBundle(string bundleName, string bundleUrl)
    {
        if (_assetBundles.ContainsKey(bundleName))
            return true;

        var request = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);

        request.SendWebRequest();
        int oldLoadingProgress = 0;

        while (!request.isDone)
        {
            int loadingProgress = Mathf.RoundToInt(request.downloadProgress * 100f);

            if (loadingProgress != oldLoadingProgress)
            {
                UpdateLoadingProgressSignal.Dispatch(loadingProgress);
            }
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"AssetBundleProvider: request failed with result: {request.result.ToString()}");
            return false;
        }

        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);

        if (assetBundle != null)
        {
            _assetBundles.Add(assetBundle.name, assetBundle);
            return true;
        }

        return false;
    }

    public GameObject GetAsset(string bundleName, string assetName)
    {
        if (_assetBundles.ContainsKey(bundleName) == false || _assetBundles[bundleName] == null)
        {
            Debug.LogError($"AssetBundleProvider: cannot find bundle with name {bundleName}.");
            return null;
        }

        var asset = _assetBundles[bundleName].LoadAsset<GameObject>(assetName);

        if (asset == null)
        {
            Debug.LogError($"AssetBundleProvider: cannot find asset with name {assetName} in bundle with name {bundleName}.");
            return null;
        }

        return asset;
    }
}
