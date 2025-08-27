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
    /// Possible values for the credential state of a user.
    /// </summary>
    public enum AppleIdCredentialState
    {
        /// <summary>
        /// The given user’s authorization has been revoked and they should be signed out.
        /// </summary>
        Revoked,

        /// <summary>
        /// The user is authorized.
        /// </summary>
        Authorized,

        /// <summary>
        /// The user hasn’t established a relationship with Sign in with Apple.
        /// </summary>
        NotFound,

        /// <summary>
        /// The app has been transferred to a different team, and you need to migrate the user’s identifier.
        /// </summary>
        Transferred,

        /// <summary>
        /// An error was raised by the native plugin.
        /// </summary>
        /// <remarks>
        /// This is not a part of AppleIdCredentialState.
        /// </remarks>
        PluginError = 255,
    }
}