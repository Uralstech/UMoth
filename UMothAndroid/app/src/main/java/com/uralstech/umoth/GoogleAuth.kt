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

import android.content.Context
import android.util.Log
import androidx.credentials.ClearCredentialStateRequest
import androidx.credentials.CredentialManager
import androidx.credentials.CredentialManagerCallback
import androidx.credentials.CustomCredential
import androidx.credentials.GetCredentialRequest
import androidx.credentials.GetCredentialResponse
import androidx.credentials.exceptions.ClearCredentialException
import androidx.credentials.exceptions.GetCredentialException
import androidx.credentials.exceptions.NoCredentialException
import com.google.android.libraries.identity.googleid.GetGoogleIdOption
import com.google.android.libraries.identity.googleid.GoogleIdTokenCredential
import com.google.android.libraries.identity.googleid.GoogleIdTokenParsingException
import java.lang.ref.WeakReference
import java.util.concurrent.Executors

/** Script to help with Google account authentication. */
class GoogleAuth(private val currentContext: Context) {
    companion object {
        private const val  LOGGING_TAG = "UMoth_GoogleAuth"

        /** Reason for Sign-In failures. */
        private enum class SignInFailReason(val code: Int) {
            /** Generic error from the getCredentialAsync request. */
            GENERIC_ERROR(0),

            /** The returned credential was not recognized. */
            UNKNOWN_CREDENTIAL_TYPE(1),

            /** The Google ID token was not valid. */
            INVALID_GOOGLE_ID_RESPONSE(2)
        }

        private var Instance: WeakReference<GoogleAuth> = WeakReference(null)

        /** Gets the static instance of [GoogleAuth]. */
        @JvmStatic
        fun getInstance(context: Context): GoogleAuth {
            val instance = Instance.get()
            return if (instance != null) {
                instance
            } else {
                val newInstance = GoogleAuth(context)
                Instance = WeakReference(newInstance)

                newInstance
            }
        }
    }

    private val credentialManager = CredentialManager.create(currentContext)

    /**
     * Starts the Google Sign-In flow.
     * @param callbacks Unity callback receiver.
     * @param serverClientId The server's client ID to use as the audience for Google ID tokens generated during the sign-in.
     * @param nonce The nonce to use when generating a Google ID token.
     * @param filterByAuthorizedAccount Whether to only allow the user to select from Google accounts that are already authorized to sign in to your application.
     * @param autoSelectSignIn The auto-select behavior in the request.
     */
    fun startSignIn(callbacks: GoogleAuthCallbackReceiver, serverClientId: String, nonce: String?, filterByAuthorizedAccount: Boolean, autoSelectSignIn: Boolean) {
         Log.i(LOGGING_TAG, "Starting Google Sign-In process.")

         val googleIdOption = GetGoogleIdOption.Builder()
             .setServerClientId(serverClientId)
             .setFilterByAuthorizedAccounts(filterByAuthorizedAccount)
             .setAutoSelectEnabled(autoSelectSignIn)
             .setNonce(nonce)
             .build()

         val request = GetCredentialRequest.Builder()
             .addCredentialOption(googleIdOption)
             .build()

         val requestExecutor = Executors.newSingleThreadExecutor()
         credentialManager.getCredentialAsync(
             context = currentContext,
             request = request,
             cancellationSignal = null,
             executor = requestExecutor,
             callback = object : CredentialManagerCallback<GetCredentialResponse, GetCredentialException>
             {
                 override fun onError(e: GetCredentialException) {
                     requestExecutor.shutdown()

                     if (e is NoCredentialException && filterByAuthorizedAccount) {
                         Log.e(LOGGING_TAG, "Failed to get credentials as none were found. filterByAuthorizedAccount is true, so trying again with it false.", e)
                         startSignIn(callbacks, serverClientId, nonce, false, autoSelectSignIn)
                         return
                     }

                     Log.e(LOGGING_TAG,"Failed to get credentials due to error.", e)
                     callbacks.onSignInFailed(SignInFailReason.GENERIC_ERROR.code)
                 }

                 override fun onResult(result: GetCredentialResponse) {
                     requestExecutor.shutdown()

                     val credential = result.credential
                     if (credential !is CustomCredential || credential.type != GoogleIdTokenCredential.TYPE_GOOGLE_ID_TOKEN_CREDENTIAL) {
                         Log.e(LOGGING_TAG, "Got unrecognized credential of type \"${credential.type}\".")
                         callbacks.onSignInFailed(SignInFailReason.UNKNOWN_CREDENTIAL_TYPE.code)
                         return
                     }

                     try {
                         val googleIdTokenCredential = GoogleIdTokenCredential.createFrom(credential.data)
                         Log.i(LOGGING_TAG, "Got credentials successfully.")
                         callbacks.onSignedIn(GoogleIdTokenCredentialWrapper(googleIdTokenCredential))
                     } catch (e: GoogleIdTokenParsingException) {
                         Log.e(LOGGING_TAG, "Received an invalid Google Id token response.", e)
                         callbacks.onSignInFailed(SignInFailReason.INVALID_GOOGLE_ID_RESPONSE.code)
                     }
                 }
             })
    }

    /**
     * Starts the sign-out flow.
     * @param callbacks Unity callback receiver.
     */
    fun startSignOut(callbacks: GoogleAuthCallbackReceiver) {
        Log.i(LOGGING_TAG, "Starting Google Sign-Out process.")

        val requestExecutor = Executors.newSingleThreadExecutor()
        credentialManager.clearCredentialStateAsync(
            request = ClearCredentialStateRequest(),
            cancellationSignal = null,
            executor = requestExecutor,
            callback = object : CredentialManagerCallback<Void?, ClearCredentialException>
            {
                override fun onError(e: ClearCredentialException) {
                    requestExecutor.shutdown()

                    Log.i(LOGGING_TAG, "Could not sign out due to error.", e)
                    callbacks.onSignOutFailed()
                }

                override fun onResult(result: Void?) {
                    requestExecutor.shutdown()

                    Log.i(LOGGING_TAG, "Signed out successfully.")
                    callbacks.onSignedOut()
                }
            })
    }
}