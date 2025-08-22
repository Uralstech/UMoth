#if UNITY_IOS

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

/// <summary>
/// Patches the built XCode project to include the Sign In with Apple capability.
/// </summary>
public class BuildPostprocessor : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget _, string path)
    {
        string projectPath = PBXProject.GetPBXProjectPath(path);

        PBXProject project = new();
        project.ReadFromFile(projectPath);

        string mainTargetGuid = project.GetUnityMainTargetGuid();
        string entitlementFilePath = project.GetEntitlementFilePathForTarget(mainTargetGuid);

        ProjectCapabilityManager capabilityManager = new(projectPath, entitlementFilePath, targetGuid: mainTargetGuid);
        capabilityManager.AddSignInWithApple();
        capabilityManager.WriteToFile();
    }
}

#endif