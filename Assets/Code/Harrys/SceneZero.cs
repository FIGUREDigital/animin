using UnityEngine;
using System.Collections;

public class SceneZero : MonoBehaviour {

    private static bool LoadedOBB = false;

    void Start()
    {
        #if DEBUG
            #if UNITY_ANDROID
                if(Application.isEditor)return;
                if (!GooglePlayDownloader.RunningOnAndroid())
                {
                    Debug.Log("Use GooglePlayDownloader only on Android device!");
                    return;
                }

                string expPath = GooglePlayDownloader.GetExpansionFilePath();
                if (expPath == null)
                {
                    Debug.Log("External storage is not available!");
                }
                else
                {
                    string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
                    string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);

                    Debug.Log("Main = ..."  + ( mainPath == null ? " NOT AVAILABLE" :  mainPath.Substring(expPath.Length)));
                    Debug.Log("Patch = ..." + (patchPath == null ? " NOT AVAILABLE" : patchPath.Substring(expPath.Length)));
                    if (mainPath == null || patchPath == null)
                    if (!LoadedOBB)
                    {
                        GooglePlayDownloader.FetchOBB();
                        LoadedOBB = true;
                    }
                }
            #endif
        #endif
        Debug.Log("Hey, if you're seeing this that means SCENE ZERO was fired! Not fired like that, I mean it was triggered. Oh happy day. I want to go home");
        Application.LoadLevel("Menu");
    }
}
