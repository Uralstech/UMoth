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
    /// Errors that can occur during authorization.
    /// </summary>
    public enum AppleIdSignInErrorCode
    {
        /// <summary>
        /// The native plugin is already processing a sign in request.
        /// </summary>
        /// <remarks>
        /// This is not a part of ASAuthorizationError.Code but a custom error returned by the native plugin interface.
        /// </remarks>
        PluginBusy = -2,

        /// <summary>
        /// An unknown credential type was returned.
        /// </summary>
        /// <remarks>
        /// This is not a part of ASAuthorizationError.Code but a custom error returned by the native plugin.
        /// </remarks>
        UnknownCredentialType = -1,

        /// <summary>
        /// The authorization attempt failed for an unknown reason.
        /// </summary>
        Unknown = 1000,

        /// <summary>
        /// The user canceled the authorization attempt.
        /// </summary>
        Canceled = 1001,

        /// <summary>
        /// The authorization request received an invalid response.
        /// </summary>
        InvalidResponse = 1002,

        /// <summary>
        /// The authorization request wasn't handled.
        /// </summary>
        NotHandled = 1003,

        /// <summary>
        /// The authorization attempt failed.
        /// </summary>
        Failed = 1004,

        /// <summary>
        /// The authorization request isn’t interactive.
        /// </summary>
        NotInteractive = 1005,

        /// <summary>
        /// This error should only be returned when specifying @c excludedCredentials on a public key credential registration request.
        /// </summary>
        MatchedExcludedCredential = 1006,

        /// <summary>
        /// The credential import request failed.
        /// </summary>
        CredentialImport = 1007,

        /// <summary>
        /// The credential export request failed.
        /// </summary>
        CredentialExport = 1008
    }
}