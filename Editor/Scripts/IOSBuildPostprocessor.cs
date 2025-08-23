#if UNITY_IOS

using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

/// <summary>
/// Patches the built XCode project to include the Sign In with Apple capability.
/// </summary>
public class IOSBuildPostprocessor : IPostprocessBuildWithReport
{
    public int callbackOrder => 999;

    public void OnPostprocessBuild(BuildReport report)
    {
        string projectPath = PBXProject.GetPBXProjectPath(report.summary.outputPath);

        PBXProject project = new();
        project.ReadFromFile(projectPath);

        string mainTargetGuid = project.GetUnityMainTargetGuid();
        string entitlementFilePath = project.GetEntitlementFilePathForTarget(mainTargetGuid) ?? "Unity-iPhone/Entitlements.entitlements";

        ProjectCapabilityManager capabilityManager = new(projectPath, entitlementFilePath, targetGuid: mainTargetGuid);
        capabilityManager.AddSignInWithApple();
        capabilityManager.WriteToFile();
    }
}

#endif