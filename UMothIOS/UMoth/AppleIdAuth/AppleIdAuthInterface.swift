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

import AuthenticationServices
import Security

@_cdecl("umoth_appleid_auth_get_credential_state")
public func getCredentialState(userId: UnsafePointer<CChar>, onResult: @convention(c) @escaping (AppleIdCredentialStateWrapper, UnsafePointer<CChar>?) -> Void) {
    logger.log("Getting AppleID credential state.")
    
    let appleIDProvider = ASAuthorizationAppleIDProvider()
    appleIDProvider.getCredentialState(forUserID: String(cString: userId)) { (state, error) in
        if error != nil {
            logger.error("Failed to get AppleID credential state due to error, state: \(state.rawValue), error message: \(error?.localizedDescription ?? "")")
        } else {
            logger.log("Got AppleID credential state: \(state.rawValue)")
        }
        
        onResult(AppleIdCredentialStateWrapper(UInt8(state.rawValue)), strdupC(error?.localizedDescription))
    }
}

@_cdecl("umoth_appleid_auth_start_sign_in")
public func startSignIn(
    requestScopes: AppleIdScopeWrapper,
    nonce: UnsafePointer<CChar>?,
    state: UnsafePointer<CChar>?,
    onSignedInCallback: @convention(c) @escaping (AppleIdCredentialWrapper) -> Void,
    onSignInFailedCallback: @convention(c) @escaping (AppleIdAuthorizationErrorCode) -> Void
) -> Bool {

    guard AppleIdAuthShared.authDelegate == nil else {
        logger.error("startSignIn called while an existing operation was ongoing.")
        return false
    }

    logger.log("Starting Apple Sign-In.")
    let appleIDProvider = ASAuthorizationAppleIDProvider()
    let request = appleIDProvider.createRequest()

    var scopes: [ASAuthorization.Scope] = []
    if (requestScopes.rawValue & AppleIdScopeWrapper_FullName.rawValue) != 0 {
        scopes.append(.fullName)
    }
    if (requestScopes.rawValue & AppleIdScopeWrapper_Email.rawValue) != 0 {
        scopes.append(.email)
    }
    
    request.requestedScopes = scopes

    if let nonce = nonce {
        request.nonce = String(cString: nonce)
    }
    if let state = state {
        request.state = String(cString: state)
    }

    let authorizationController = ASAuthorizationController(authorizationRequests: [request])
    
    AppleIdAuthShared.onSignedIn = onSignedInCallback
    AppleIdAuthShared.onSignInFailed = onSignInFailedCallback
    
    AppleIdAuthShared.authPresentationContextProvider = AuthControllerPresentationProvider()
    AppleIdAuthShared.authDelegate = AppleIdAuthDelegate()

    authorizationController.presentationContextProvider = AppleIdAuthShared.authPresentationContextProvider
    authorizationController.delegate = AppleIdAuthShared.authDelegate
    authorizationController.performRequests()
    
    logger.log("Apple Sign-In request executed.")
    return true
}

