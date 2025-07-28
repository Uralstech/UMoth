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
    /// Google ID token based credential.
    /// </summary>
    public record GoogleIdTokenCredential
    {
        /// <summary>
        /// Display name to show on the entry.
        /// </summary>
        public readonly string? DisplayName;

        /// <summary>
        /// User's family name.
        /// </summary>
        public readonly string? FamilyName;

        /// <summary>
        /// User's given name.
        /// </summary>
        public readonly string? GivenName;

        /// <summary>
        /// The email address associated with user's Google Account.
        /// </summary>
        public readonly string EmailId;

        /// <summary>
        /// User's Google ID Token.
        /// </summary>
        public readonly string IdToken;

        /// <summary>
        /// User's stored phone number.
        /// </summary>
        public readonly string? PhoneNumber;

        /// <summary>
        /// User's profile picture uri.
        /// </summary>
        public readonly string? ProfilePictureUri;

        internal protected GoogleIdTokenCredential(string? displayName, string? familyName, string? givenName, string emailId, string idToken, string? phoneNumber, string? profilePictureUri)
        {
            DisplayName = displayName;
            FamilyName = familyName;
            GivenName = givenName;
            EmailId = emailId;
            IdToken = idToken;
            PhoneNumber = phoneNumber;
            ProfilePictureUri = profilePictureUri;
        }
    }
}
