using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundlws : MonoBehaviour
{
    public string assetbundle_download_url;
    public string load_asset_path;

    IEnumerator Start()
    {
        Debug.Log("Download Start");
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(assetbundle_download_url, 0);
        yield return req.SendWebRequest();
        Debug.Log("Download Finished");

        Debug.Log("Get AssetBundle Instance");
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(req);

        Debug.Log("bundle.LoadAsset() from AssetBundle");
        GameObject obj = bundle.LoadAsset<GameObject>(load_asset_path);

        Instantiate(obj);
    }
}
