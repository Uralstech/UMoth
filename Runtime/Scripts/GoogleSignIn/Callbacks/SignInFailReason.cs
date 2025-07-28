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
namespace Uralstech.UMoth.GoogleSignIn
{
    /// <summary>
    /// Reason for Sign-In failures.
    /// </summary>
    public enum SignInFailReason
    {
        /// <summary>
        /// Generic error from the native credential request.
        /// </summary>
        GenericError = 0,

        /// <summary>
        /// The returned credential was not recognized.
        /// </summary>
        UnknownCredentialType = 1,

        /// <summary>
        /// The Google ID token was not valid.
        /// </summary>
        InvalidGoogleIdResponse = 2,
    }
}
