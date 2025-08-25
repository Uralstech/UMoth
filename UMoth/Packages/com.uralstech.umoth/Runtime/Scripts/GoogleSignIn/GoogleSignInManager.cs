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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using Uralstech.Utils.Loggers;
using Uralstech.Utils.Singleton;

#nullable enable
namespace Uralstech.UMoth.GoogleSignIn
{
    /// <summary>
    /// Class to handle Google account sign-in.
    /// </summary>
    [AddComponentMenu("Uralstech/UMoth/Google Sign-In Manager")]
    public class GoogleSignInManager : DontCreateNewSingleton<GoogleSignInManager>, IGoogleAuthCallbackReceiver
    {
        /// <summary>
        /// The fully qualified name of the Kotlin plugin class.
        /// </summary>
        public const string AndroidNativeClass = "com.uralstech.umoth.GoogleAuth";

        private static readonly string s_loggerTag = $"{nameof(UMoth)}.{nameof(GoogleSignInManager)}";
        private static readonly TaggedRALogger s_logger = new(s_loggerTag);

        /// <summary>
        /// The server's client ID to use as the audience for Google ID tokens generated during the sign-in.
        /// </summary>
        [Tooltip("The server's client ID to use as the audience for Google ID tokens generated during the sign-in.")]
        [field: SerializeField] public string ServerClientId { protected get; set; } = string.Empty;

        /// <summary>
        /// Called when the sign-in flow succeeds, with the Google ID Token credential.
        /// </summary>
        [Tooltip("Called when the sign-in flow succeeds, with the Google ID Token credential.")]
        public UnityEvent<GoogleIdTokenCredential> OnSignedIn = new();

        /// <summary>
        /// Called when the sign-in flow fails, with the failure reason.
        /// </summary>
        [Tooltip("Called when the sign-in flow fails, with the failure reason.")]
        public UnityEvent<GoogleSignInErrorCode> OnSignInFailed = new();

        /// <summary>
        /// Called when the sign-out flow succeeds.
        /// </summary>
        [Tooltip("Called when the sign-out flow succeeds.")]
        public UnityEvent OnSignedOut = new();

        /// <summary>
        /// Called when the sign-out flow fails.
        /// </summary>
        [Tooltip("Called when the sign-out flow fails.")]
        public UnityEvent OnSignOutFailed = new();

#if UNITY_ANDROID
        /// <summary>
        /// The native callback receiver.
        /// </summary>
        protected GoogleAuthCallbackReceiver _callbackReceiver;

        /// <summary>
        /// The native plugin instance.
        /// </summary>
        protected AndroidJavaObject? _pluginInstance;
#endif

        private Action<GoogleIdTokenCredential>? _onSignedIn;
        private Action<GoogleSignInErrorCode>? _onSignInFailed;
        private Action? _onSignedOut;
        private Action? _onSignOutFailed;

        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
            s_logger.Log("Initializing for Android.");

            using AndroidJavaClass classObject = new(AndroidNativeClass);
            _pluginInstance = classObject.CallStatic<AndroidJavaObject>("getInstance", AndroidApplication.currentContext);
            _callbackReceiver = new GoogleAuthCallbackReceiver(this);
#endif
        }

#if UNITY_ANDROID
        protected void OnDestroy()
        {
            s_logger.Log("Releasing native resources for Android");
            _pluginInstance?.Dispose();
            _pluginInstance = null;
        }
#endif

        /// <summary>
        /// Starts the Google Sign-In flow.
        /// </summary>
        /// <param name="nonce">The nonce to use when generating a Google ID token.</param>
        /// <param name="filterByAuthorizedAccount">Whether to only allow the user to select from Google accounts that are already authorized to sign in to your application.</param>
        /// <param name="autoSelectSignIn">The auto-select behavior in the request.</param>
        /// <returns>The ID token credential or the failure reason.</returns>
        public async Awaitable<(GoogleIdTokenCredential?, GoogleSignInErrorCode)> SignInAsync(string? nonce = null, bool filterByAuthorizedAccount = true, bool autoSelectSignIn = true)
        {
            TaskCompletionSource<(GoogleIdTokenCredential?, GoogleSignInErrorCode)> tcs = new();
            void OnSuccess(GoogleIdTokenCredential token) => tcs.SetResult((token, default));
            void OnFailure(GoogleSignInErrorCode failReason) => tcs.SetResult((null, failReason));

            await Awaitable.MainThreadAsync();
            _onSignedIn += OnSuccess;
            _onSignInFailed += OnFailure;

            SignIn(nonce, filterByAuthorizedAccount, autoSelectSignIn);
            await tcs.Task;

            _onSignedIn -= OnSuccess;
            _onSignInFailed -= OnFailure;
            return tcs.Task.Result;
        }

        /// <summary>
        /// Starts the Google Sign-In flow.
        /// </summary>
        /// <param name="nonce">The nonce to use when generating a Google ID token.</param>
        /// <param name="filterByAuthorizedAccount">Whether to only allow the user to select from Google accounts that are already authorized to sign in to your application.</param>
        /// <param name="autoSelectSignIn">The auto-select behavior in the request.</param>
        public void SignIn(string? nonce = null, bool filterByAuthorizedAccount = true, bool autoSelectSignIn = true)
        {
#if UNITY_ANDROID
            s_logger.Log("Starting the sign in process for Android.");
            _pluginInstance!.Call("startSignIn", _callbackReceiver, ServerClientId, nonce, filterByAuthorizedAccount, autoSelectSignIn);
#else
            throw new NotSupportedException($"{nameof(GoogleSignInManager)} does not have an implementation for {nameof(SignIn)} for the current platform.");
#endif
        }

        /// <summary>
        /// Starts the sign-out flow.
        /// </summary>
        /// <returns>If the operation was successful.</returns>
        public async Awaitable<bool> SignOutAsync()
        {
            TaskCompletionSource<bool> tcs = new();
            void OnSuccess() => tcs.SetResult(true);
            void OnFailure() => tcs.SetResult(false);

            await Awaitable.MainThreadAsync();
            _onSignedOut += OnSuccess;
            _onSignOutFailed += OnFailure;

            SignOut();
            await tcs.Task;

            _onSignedOut -= OnSuccess;
            _onSignOutFailed -= OnFailure;
            return tcs.Task.Result;
        }

        /// <summary>
        /// Starts the sign-out flow.
        /// </summary>
        public void SignOut()
        {
#if UNITY_ANDROID
            s_logger.Log("Starting the sign out process for Android.");
            _pluginInstance!.Call("startSignOut", _callbackReceiver);
#else
            throw new NotSupportedException($"{nameof(GoogleSignInManager)} does not have an implementation for {nameof(SignOut)} for the current platform.");
#endif
        }

        #region IGoogleAuthCallbackReceiver Implementation
        /// <inheritdoc/>
        void IGoogleAuthCallbackReceiver.OnSignedIn(GoogleIdTokenCredential credential)
        {
            s_logger.Log("Signed in with Google account.");
            _onSignedIn?.Invoke(credential);
            OnSignedIn.Invoke(credential);
        }

        /// <inheritdoc/>
        void IGoogleAuthCallbackReceiver.OnSignInFailed(GoogleSignInErrorCode reason)
        {
            s_logger.Log("Could not sign in with Google account, failure reason: {0}", reason);
            _onSignInFailed?.Invoke(reason);
            OnSignInFailed.Invoke(reason);
        }

        /// <inheritdoc/>
        void IGoogleAuthCallbackReceiver.OnSignedOut()
        {
            s_logger.Log("Signed out of Google account.");
            _onSignedOut?.Invoke();
            OnSignedOut.Invoke();
        }

        /// <inheritdoc/>
        void IGoogleAuthCallbackReceiver.OnSignOutFailed()
        {
            s_logger.Log("Could not sign out of Google account.");
            _onSignOutFailed?.Invoke();
            OnSignOutFailed.Invoke();
        }
        #endregion
    }
}
