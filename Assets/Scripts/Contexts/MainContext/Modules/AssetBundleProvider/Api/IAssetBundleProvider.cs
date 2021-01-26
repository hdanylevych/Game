using System.Collections.Generic;
using UnityEngine;

public interface IAssetBundleProvider
{
    IReadOnlyDictionary<string, AssetBundle> AssetBundles { get; }

    GameObject GetAsset(string bundleName, string assetName);
}
