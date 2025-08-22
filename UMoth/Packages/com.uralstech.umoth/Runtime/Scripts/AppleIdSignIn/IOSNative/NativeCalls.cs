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

using System;
using System.Runtime.InteropServices;

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn.Native
{
    /// <summary>
    /// Interface for the native Swift plugin.
    /// </summary>
    public static class NativeCalls
    {
        /// <summary>
        /// Frees up a buffer allocated from native code using Swift's UnsafeMutablePointer.allocate.
        /// </summary>
        /// <param name="ptr">The buffer to deallocate.</param>
        [DllImport("__Internal")]
        public static extern void umoth_free_native_buffer(IntPtr ptr);

        /// <summary>
        /// Callback delegate for when AppleID Sign-In completes successfully.
        /// </summary>
        /// <param name="credential">The native ASAuthorizationAppleIDCredential wrapper.</param>
        public delegate void OnSignedInCallback(NativeAppleIdCredential credential);

        /// <summary>
        /// Callback delegate for when AppleID Sign-In fails.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public delegate void OnSignInFailedCallback(short errorCode);

        /// <summary>
        /// Starts an AppleID Sign-In request.
        /// </summary>
        /// <param name="requestScopes">The data scopes to request from the user.</param>
        /// <param name="nonce">An optional nonce for the request.</param>
        /// <param name="state">An optional state for the request which will be returned in the resulting ASAuthorizationAppleIDCredential.</param>
        /// <param name="onSignedInCallback">Success callback.</param>
        /// <param name="onSignInFailedCallback">Failure callback.</param>
        [DllImport("__Internal")]
        public static extern void umoth_appleid_auth_start_sign_in(
            byte requestScopes,
            string? nonce,
            string? state,
            OnSignedInCallback onSignedInCallback,
            OnSignInFailedCallback onSignInFailedCallback
        );
    }
}