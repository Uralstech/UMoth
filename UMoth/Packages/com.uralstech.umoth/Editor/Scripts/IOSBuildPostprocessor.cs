// Copyright 2025 URAV ADVANCED LEARNING SYSTEMS PRIVATE LIMITED
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if UNITY_IOS

using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

/// <summary>
/// Patches the built XCode project to include the Sign In with Apple capability.
/// </summary>
public class IOSBuildPostprocessor : IPostprocessBuildWithReport
{
    /// <inheritdoc/>
    public int callbackOrder => 999;

    /// <inheritdoc/>
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