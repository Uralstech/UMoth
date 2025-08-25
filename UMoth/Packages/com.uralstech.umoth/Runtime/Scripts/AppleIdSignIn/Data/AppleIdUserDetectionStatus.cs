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

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn
{
    /// <summary>
    /// Wrapper for ASUserDetectionStatus.
    /// </summary>
    public enum AppleIdUserDetectionStatus
    {
        /// <summary>
        /// Not supported on current platform, ignore the value.
        /// </summary>
        Unsupported = 0,

        /// <summary>
        /// We could not determine the value. New users in the ecosystem will get this value as well, so you should not block these users, but instead treat them as any new user through standard email sign up flows.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// A hint that we have high confidence that the user is real.
        /// </summary>
        LikelyReal = 2
    }
}