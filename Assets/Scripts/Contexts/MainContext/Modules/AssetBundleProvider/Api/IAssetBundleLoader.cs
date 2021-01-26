using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetBundleLoader
{
    bool LoadWebBundle(string bundleName, string bundleUrl);
}
