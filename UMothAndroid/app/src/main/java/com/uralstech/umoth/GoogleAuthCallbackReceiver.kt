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

/** Callback receiver for events from [GoogleAuth]. */
interface GoogleAuthCallbackReceiver {
    /**
     * Called when the sign-in flow succeeds.
     * @param credential The Google ID Token credential.
     */
    fun onSignedIn(credential: GoogleIdTokenCredentialWrapper)

    /**
     * Called when the sign-in flow fails.
     * @param reason The reason for the failure.
     */
    fun onSignInFailed(reason: Int)

    /** Called when the sign-out flow succeeds. */
    fun onSignedOut()

    /** Called when the sign-out flow fails. */
    fun onSignOutFailed()
}