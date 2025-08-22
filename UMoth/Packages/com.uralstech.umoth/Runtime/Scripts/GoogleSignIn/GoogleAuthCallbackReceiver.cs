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
using UnityEngine;

#if UNITY_ANDROID

#nullable enable
namespace Uralstech.UMoth.GoogleSignIn
{
    /// <summary>
    /// Callback receiver for events from the native plugin.
    /// </summary>
    public class GoogleAuthCallbackReceiver : AndroidJavaProxy
    {
        /// <summary>
        /// The fully qualified name of the Kotlin interface this class is implementing.
        /// </summary>
        public const string NativeInterface = "com.uralstech.umoth.GoogleAuthCallbackReceiver";

        /// <summary>
        /// The C# callback receiver for native plugin events.
        /// </summary>
        protected IGoogleAuthCallbackReceiver _callbackReceiver;

        /// <param name="callbackReceiver">The C# callback receiver for native plugin events.</param>
        public GoogleAuthCallbackReceiver(IGoogleAuthCallbackReceiver callbackReceiver) : base(NativeInterface)
        {
            _callbackReceiver = callbackReceiver;
        }

        /// <inheritdoc/>
        public override IntPtr Invoke(string methodName, IntPtr javaArgs)
        {
            switch (methodName)
            {
                case "onSignedIn":
                    using (AndroidJavaObject credential = new(AndroidJNI.GetObjectArrayElement(javaArgs, 0)))
                    {
                        _callbackReceiver.OnSignedIn(new GoogleIdTokenCredential(
                            displayName: credential.Get<string?>("displayName"),
                            familyName: credential.Get<string?>("familyName"),
                            givenName: credential.Get<string?>("givenName"),
                            emailId: credential.Get<string>("emailId"),
                            idToken: credential.Get<string>("idToken"),
                            phoneNumber: credential.Get<string?>("phoneNumber"),
                            profilePictureUri: credential.Get<string?>("profilePictureUri")
                        ));
                    }

                    return IntPtr.Zero;

                case "onSignInFailed":
                    AndroidJNIHelper.Unbox(AndroidJNI.GetObjectArrayElement(javaArgs, 0), out int reason);
                    _callbackReceiver.OnSignInFailed((GoogleSignInErrorCode)reason);
                    return IntPtr.Zero;

                case "onSignedOut":
                    _callbackReceiver.OnSignedOut();
                    return IntPtr.Zero;

                case "onSignOutFailed":
                    _callbackReceiver.OnSignOutFailed();
                    return IntPtr.Zero;

                default:
                    return base.Invoke(methodName, javaArgs);
            }
        }
    }
}

#endif