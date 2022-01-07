using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{

    [MenuItem("Build/Build Mac")]
    public static void BuildMac()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "Build/" + Application.productName + ".app";
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.scenes = GetScenes();

        Debug.Log("Building StandaloneOSX");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Built StandaloneOSX");
    }

    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "Build/" + Application.productName + ".exe";
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.scenes = GetScenes();

        Debug.Log("Building StandaloneOSX");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Built StandaloneOSX");
    }

    private static string[] GetScenes()
    {
        return (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
    }

}
