using UnityEditor;

public class CreateAssetBindles 
{
    [MenuItem("Assets/Build AssetBundle")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

}
