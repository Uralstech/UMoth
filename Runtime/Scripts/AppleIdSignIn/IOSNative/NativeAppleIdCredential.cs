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
    /// Wrapper for iOS's native ASAuthorizationAppleIDCredential.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeAppleIdCredential
    {
        /// <summary>
        /// An opaque user ID associated with the AppleID used for the sign in. This identifier will be stable across the 'developer team'.
        /// </summary>
        public string UserId;

        /// <summary>
        /// Data that's returned to you unmodified in the corresponding credential after a successful authentication.
        /// </summary>
        public string? State;

        /// <summary>
        /// This value will contain a list of scopes for which the user provided authorization. 
        /// These may contain a subset of the requested scopes on @see ASAuthorizationAppleIDRequest.
        /// The application should query this value to identify which scopes were returned as it maybe different from ones requested.
        /// </summary>
        /// <remarks>
        /// TODO: Fix this, it always returns 0.
        /// </remarks>
        public byte Scopes;

        /// <summary>
        /// A short-lived, one-time valid token that provides proof of authorization to the server component of the app.
        /// The authorization code is bound to the specific transaction using the state attribute passed in the authorization request.
        /// The server component of the app can validate the code using Apple's identity service endpoint provided for this purpose.
        /// </summary>
        public string? AuthorizationCode;

        /// <summary>
        /// A JSON Web Token (JWT) used to communicate information about the identity of the user in a secure way to the app.
        /// The ID token will contain the following information: Issuer Identifier, Subject Identifier, Audience, Expiry Time and Issuance Time signed by Apple's identity service.
        /// </summary>
        public string? IdentityToken;

        /// <summary>
        /// An optional email shared by the user. This field is populated with a value that the user authorized.
        /// </summary>
        public string? Email;

        /// <summary>
        /// An optional full name shared by the user. This field is populated with a value that the user authorized.
        /// </summary>
        /// <remarks>
        /// This is an instance of <see cref="NativePersonNameComponents"/>.
        /// If it has not been unwrapped, it will be freed when this struct's <see cref="Dispose()"/> is called.
        /// If unwrapped using <see cref="UnwrapFullName()"/>, the caller is responsible for disposing the returned object.
        /// </remarks>
        public IntPtr FullName;

        /// <summary>
        /// Check this property for a hint as to whether the current user is a "real user".
        /// See the documentation for ASUserDetectionStatus for guidelines on handling each status.
        /// </summary>
        public byte RealUserStatus;

        /// <summary>
        /// Check this property to determine whether the current user is a child.
        /// See the documentation for ASUserAgeRange for guidelines on handling each status.
        /// </summary>
        public byte UserAgeRange;

        /// <summary>
        /// Unwraps <see cref="FullName"/> into an instance of <see cref="NativePersonNameComponents"/>.
        /// </summary>
        /// <remarks>
        /// This can only be called once, as after <see cref="FullName"/> is unwrapped, the reference is
        /// deallocated. This is to prevent multiple copies of the same memory from being created.
        /// </remarks>
        /// <returns>The unwrapped struct or <see langword="null"/> if <see cref="FullName"/> is a nullptr.</returns>
        public NativePersonNameComponents? UnwrapFullName()
        {
            if (FullName == IntPtr.Zero)
                return null;

            NativePersonNameComponents structure = Marshal.PtrToStructure<NativePersonNameComponents>(FullName);
            NativeCalls.umoth_free_native_buffer(FullName);
            FullName = IntPtr.Zero;
            return structure;
        }

        /// <summary>
        /// Deallocates native resources.
        /// </summary>
        /// <remarks>
        /// This struct has been intentionally not set as IDisposable to prevent
        /// accidental struct copies caused by "using" statements.
        /// </remarks>
        public void Dispose()
        {
            if (UnwrapFullName() is NativePersonNameComponents components)
            {
                // If at this point FullName has not been accessed, manually deallocate it.
                components.Dispose();
            }
        }
    }
}
