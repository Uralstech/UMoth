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

package com.uralstech.umoth

import com.google.android.libraries.identity.googleid.GoogleIdTokenCredential

/** A wrapper for [GoogleIdTokenCredential]. */
class GoogleIdTokenCredentialWrapper(credential: GoogleIdTokenCredential) {
    /** Display name to show on the entry. */
    val displayName: String? = credential.displayName

    /** User's family name. */
    val familyName: String? = credential.familyName

    /** User's given name. */
    val givenName: String? = credential.givenName

    /** The email address associated with user's Google Account. */
    val emailId: String = credential.id

    /** User's Google ID Token. */
    val idToken: String = credential.idToken

    /** User's stored phone number. */
    val phoneNumber: String? = credential.phoneNumber

    /** User's profile picture uri. */
    val profilePictureUri: String? = credential.profilePictureUri?.toString()
}