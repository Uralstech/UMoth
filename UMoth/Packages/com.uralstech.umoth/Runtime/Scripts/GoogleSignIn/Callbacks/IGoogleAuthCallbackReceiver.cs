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
    /// Callback receiver object for Google auth events.
    /// </summary>
    public interface IGoogleAuthCallbackReceiver
    {
        /// <summary>
        /// Called when the sign-in flow succeeds.
        /// </summary>
        /// <param name="credential">The Google ID Token credential.</param>
        public void OnSignedIn(GoogleIdTokenCredential credential);

        /// <summary>
        /// Called when the sign-in flow fails.
        /// </summary>
        /// <param name="reason">The reason for failure.</param>
        public void OnSignInFailed(GoogleSignInErrorCode reason);

        /// <summary>
        /// Called when the sign-out flow succeeds.
        /// </summary>
        public void OnSignedOut();

        /// <summary>
        /// Called when the sign-out flow fails.
        /// </summary>
        public void OnSignOutFailed();
    }
}
