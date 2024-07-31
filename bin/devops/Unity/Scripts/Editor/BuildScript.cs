using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

using UnityEditor.Build.Reporting;

public class BuildScript : MonoBehaviour
{

    [MenuItem("Tools/Mazda/Build UAAL")]
    public static void BuildUAAL()
    {
        EditorPrefs.SetBool("JdkUseEmbedded", true);
        EditorPrefs.SetBool("NdkUseEmbedded", true);
        EditorPrefs.SetBool("SdkUseEmbedded", true);
        EditorPrefs.SetBool("GradleUseEmbedded", true);
        EditorPrefs.SetBool("AndroidGradleStopDaemonsOnExit", true);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = "../buildAndroid";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.Development;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4;

        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

        // PlayerSettings.Android.applicationEntry = AndroidApplicationEntry.Activity | AndroidApplicationEntry.GameActivity;
        // PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.activeBuildTargetGroup, "il2cpp")

        AndroidArchitecture aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.Android.targetArchitectures = aac;

        Debug.Log("Building app...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
