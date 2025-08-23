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
    /// An AppleID account credential.
    /// </summary>
    public record AppleIdCredential
    {
        /// <summary>
        /// An opaque user ID associated with the AppleID used for the sign in. This identifier will be stable across the 'developer team'.
        /// </summary>
        public readonly string UserId;

        /// <summary>
        /// Data that's returned to you unmodified in the corresponding credential after a successful authentication.
        /// </summary>
        public readonly string? State;

        /// <summary>
        /// This value will contain a list of scopes for which the user provided authorization. 
        /// These may contain a subset of the requested scopes on @see ASAuthorizationAppleIDRequest.
        /// The application should query this value to identify which scopes were returned as it maybe different from ones requested.
        /// </summary>
        /// <remarks>
        /// TODO: Fix this, it always returns 0.
        /// </remarks>
        public readonly AppleIdScope Scopes;

        /// <summary>
        /// A short-lived, one-time valid token that provides proof of authorization to the server component of the app.
        /// The authorization code is bound to the specific transaction using the state attribute passed in the authorization request.
        /// The server component of the app can validate the code using Apple's identity service endpoint provided for this purpose.
        /// </summary>
        public readonly string? AuthorizationCode;

        /// <summary>
        /// A JSON Web Token (JWT) used to communicate information about the identity of the user in a secure way to the app.
        /// The ID token will contain the following information: Issuer Identifier, Subject Identifier, Audience, Expiry Time and Issuance Time signed by Apple's identity service.
        /// </summary>
        public readonly string? IdentityToken;

        /// <summary>
        /// An optional email shared by the user. This field is populated with a value that the user authorized.
        /// </summary>
        public readonly string? Email;

        /// <summary>
        /// An optional full name shared by the user. This field is populated with a value that the user authorized.
        /// </summary>
        public readonly PersonNameComponents? FullName;

        /// <summary>
        /// Check this property for a hint as to whether the current user is a "real user".
        /// See the documentation for ASUserDetectionStatus for guidelines on handling each status.
        /// </summary>
        public readonly AppleIdUserDetectionStatus RealUserStatus;

        /// <summary>
        /// Check this property to determine whether the current user is a child.
        /// See the documentation for ASUserAgeRange for guidelines on handling each status.
        /// </summary>
        public readonly AppleIdUserAgeRange UserAgeRange;

        internal protected AppleIdCredential(string userId, string? state, AppleIdScope scopes, string? authorizationCode, string? identityToken, string? email, PersonNameComponents? fullName, AppleIdUserDetectionStatus realUserStatus, AppleIdUserAgeRange userAgeRange)
        {
            UserId = userId;
            State = state;
            Scopes = scopes;
            AuthorizationCode = authorizationCode;
            IdentityToken = identityToken;
            Email = email;
            FullName = fullName;
            RealUserStatus = realUserStatus;
            UserAgeRange = userAgeRange;
        }
    }
}
