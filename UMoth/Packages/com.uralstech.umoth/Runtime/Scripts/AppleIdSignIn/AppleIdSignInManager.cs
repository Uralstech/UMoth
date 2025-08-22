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

using AOT;
using System;
using UnityEngine;
using UnityEngine.Events;
using Uralstech.Utils.Loggers;
using Uralstech.Utils.Singleton;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

#if UNITY_IOS
using Uralstech.UMoth.AppleIdSignIn.Native;
#endif

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn
{
    /// <summary>
    /// Class to handle AppleID account sign-in.
    /// </summary>
    [AddComponentMenu("Uralstech/UMoth/AppleID Sign-In Manager")]
    public class AppleIdSignInManager : DontCreateNewSingleton<AppleIdSignInManager>
    {
        private static readonly string s_loggerTag = $"{nameof(UMoth)}.{nameof(AppleIdSignInManager)}";
        private static readonly TaggedRALogger s_logger = new(s_loggerTag);

        /// <summary>
        /// Called when the sign-in flow succeeds, with the AppleID credential.
        /// </summary>
        [Tooltip("Called when the sign-in flow succeeds, with the AppleID credential.")]
        public UnityEvent<AppleIdCredential> OnSignedIn = new();

        /// <summary>
        /// Called when the sign-in flow fails, with the failure reason.
        /// </summary>
        [Tooltip("Called when the sign-in flow fails, with the failure reason.")]
        public UnityEvent<AppleIdSignInErrorCode> OnSignInFailed = new();

        private Action<AppleIdCredential>? _onSignedIn;
        private Action<AppleIdSignInErrorCode>? _onSignInFailed;

        #region iOS Native Interface
#if UNITY_IOS
        /// <summary>
        /// Reads a <see cref="NativePersonNameComponents"/> object into an instance of <see cref="PersonNameComponents"/>.
        /// </summary>
        /// <remarks>
        /// While this method DOES NOT dispose of <paramref name="nativeComponents"/>, it does dispose of
        /// its <see cref="NativePersonNameComponents.PhoneticRepresentation"/>.
        /// </remarks>
        /// <param name="nativeComponents">The native object to read.</param>
        /// <returns>The managed object or <see langword="null"/> if <paramref name="nativeComponents"/> is <see langword="null"/>.</returns>
        [return: NotNullIfNotNull(nameof(nativeComponents))]
        protected static PersonNameComponents? ReadNameComponentsToManagedType(ref NativePersonNameComponents? nativeComponents)
        {
            if (nativeComponents is null)
                return null;

            NativePersonNameComponents? nativePhoneticRepresentation = nativeComponents?.UnwrapPhoneticRepresentation();
            PersonNameComponents managedComponents = new(
                namePrefix: nativeComponents?.NamePrefix,
                givenName: nativeComponents?.GivenName,
                middleName: nativeComponents?.MiddleName,
                familyName: nativeComponents?.FamilyName,
                nameSuffix: nativeComponents?.NameSuffix,
                nickname: nativeComponents?.Nickname,
                phoneticRepresentation: ReadNameComponentsToManagedType(ref nativePhoneticRepresentation)
            );

            nativePhoneticRepresentation?.Dispose();
            return managedComponents;
        }

        [MonoPInvokeCallback(typeof(NativeCalls.OnSignedInCallback))]
        private static async void OnSignedInCallback(NativeAppleIdCredential nativeCredential)
        {
            s_logger.Log("Signed in successfully, wrapping and releasing native data.");
            NativePersonNameComponents? nativeFullName = nativeCredential.UnwrapFullName();
            AppleIdCredential managedCredential = new(
                userId: nativeCredential.UserId,
                state: nativeCredential.State,
                scopes: (AppleIdScope)nativeCredential.Scopes,
                authorizationCode: nativeCredential.AuthorizationCode.ReadIntoManagedArray(nativeCredential.AuthorizationCodeSize),
                identityToken: nativeCredential.IdentityToken.ReadIntoManagedArray(nativeCredential.IdentityTokenSize),
                email: nativeCredential.Email,
                fullName: ReadNameComponentsToManagedType(ref nativeFullName),
                realUserStatus: (AppleIdUserDetectionStatus)nativeCredential.RealUserStatus,
                userAgeRange: (AppleIdUserAgeRange)nativeCredential.UserAgeRange
            );

            nativeCredential.Dispose();
            nativeFullName?.Dispose();

            s_logger.Log("Native data wrapped and disposed, calling listeners.");

            await Awaitable.MainThreadAsync();
            Instance._onSignedIn?.Invoke(managedCredential);
            Instance.OnSignedIn.Invoke(managedCredential);
        }

        [MonoPInvokeCallback(typeof(NativeCalls.OnSignInFailedCallback))]
        private static async void OnSignInFailedCallback(short nativeErrorCode)
        {
            AppleIdSignInErrorCode managedErrorCode = (AppleIdSignInErrorCode)nativeErrorCode;
            s_logger.LogError($"Sign in failed with error code: {managedErrorCode}");

            await Awaitable.MainThreadAsync();
            Instance._onSignInFailed?.Invoke(managedErrorCode);
            Instance.OnSignInFailed.Invoke(managedErrorCode);
        }
#endif
        #endregion

        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Starts the AppleID Sign-In flow.
        /// </summary>
        /// <param name="requestedScopes">The kinds of contact information that can be requested from the user.</param>
        /// <param name="nonce">A string value to pass to the identity provider.</param>
        /// <param name="state">Data that's returned to you unmodified in the corresponding credential after a successful authentication.</param>
        /// <returns>The AppleID credential or the failure reason.</returns>
        public async Awaitable<(AppleIdCredential?, AppleIdSignInErrorCode)> SignInAsync(AppleIdScope requestedScopes, string? nonce = null, string? state = null)
        {
            TaskCompletionSource<(AppleIdCredential?, AppleIdSignInErrorCode)> tcs = new();
            void OnSuccess(AppleIdCredential token) => tcs.SetResult((token, default));
            void OnFailure(AppleIdSignInErrorCode failReason) => tcs.SetResult((null, failReason));

            await Awaitable.MainThreadAsync();
            _onSignedIn += OnSuccess;
            _onSignInFailed += OnFailure;

            SignIn(requestedScopes, nonce, state);
            await tcs.Task;

            _onSignedIn -= OnSuccess;
            _onSignInFailed -= OnFailure;
            return tcs.Task.Result;
        }

        /// <summary>
        /// Starts the AppleID Sign-In flow.
        /// </summary>
        /// <param name="requestedScopes">The kinds of contact information that can be requested from the user.</param>
        /// <param name="nonce">A string value to pass to the identity provider.</param>
        /// <param name="state">Data that's returned to you unmodified in the corresponding credential after a successful authentication.</param>
        public void SignIn(AppleIdScope requestedScopes, string? nonce = null, string? state = null)
        {
#if UNITY_IOS
            s_logger.Log("Starting the sign in process for iOS.");
            NativeCalls.umoth_appleid_auth_start_sign_in((byte)requestedScopes, nonce, state, OnSignedInCallback, OnSignInFailedCallback);
#else
            throw new NotSupportedException($"{nameof(AppleIdSignInManager)} does not have an implementation for {nameof(SignIn)} for the current platform.");
#endif
        }
    }
}