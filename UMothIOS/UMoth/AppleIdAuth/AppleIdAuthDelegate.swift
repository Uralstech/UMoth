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

internal final class AppleIdAuthDelegate : NSObject, ASAuthorizationControllerDelegate {

    public func authorizationController(controller: ASAuthorizationController, didCompleteWithAuthorization authorization: ASAuthorization) {
        let onSuccessCallback = AppleIdAuthShared.onSignedIn
        let onFailureCallback = AppleIdAuthShared.onSignInFailed
        AppleIdAuthShared.clearSharedData()
        
        guard let appleCredentials = authorization.credential as? ASAuthorizationAppleIDCredential else {
            logger.error("Got unexpected credential type for Apple Sign-In.")
            onFailureCallback?(AppleIdAuthorizationErrorCode_UnknownCredentialType)
            return
        }
        
        logger.log("Apple Sign-In succeeded.")
        guard let onSuccessCallback = onSuccessCallback else {
            logger.log("Callback is nil, skipping allocations.")
            return
        }
        
        let credentialWrapper = wrapAppleIdCredential(credential: appleCredentials)
        onSuccessCallback(credentialWrapper)
    }
    
    private func wrapAppleIdCredential(credential: ASAuthorizationAppleIDCredential) -> AppleIdCredentialWrapper {
        let userAgeRange: UserAgeRangeWrapper
        if #available(iOS 17.0, *) {
            userAgeRange = UserAgeRangeWrapper(UInt8(credential.userAgeRange.rawValue + 1))
        } else {
            userAgeRange = UserAgeRangeWrapper_NotAvailable
        }
        
        var authorizedScopes = AppleIdScopeWrapper_None.rawValue
        if credential.authorizedScopes.contains(.fullName) {
            authorizedScopes |= AppleIdScopeWrapper_FullName.rawValue
        }
        if credential.authorizedScopes.contains(.email) {
            authorizedScopes |= AppleIdScopeWrapper_Email.rawValue
        }
        
        return AppleIdCredentialWrapper(
            userId: strdupC(credential.user),
            state: strdupC(credential.state),
            scopes: AppleIdScopeWrapper(authorizedScopes),
            authorizationCode: decodeUtf8Data(from: credential.authorizationCode),
            identityToken: decodeUtf8Data(from: credential.identityToken),
            email: strdupC(credential.email),
            fullName: wrapPersonNameComponents(components: credential.fullName),
            realUserStatus: UserDetectionStatusWrapper(UInt8(credential.realUserStatus.rawValue)),
            userAgeRange: userAgeRange
        )
    }
    
    private func wrapPersonNameComponents(components: PersonNameComponents?) -> UnsafePointer<PersonNameComponentsWrapper>? {
        guard let components = components else {
            return nil
        }
        
        let wrapper = PersonNameComponentsWrapper(
            namePrefix: strdupC(components.namePrefix),
            givenName: strdupC(components.givenName),
            middleName: strdupC(components.middleName),
            familyName: strdupC(components.familyName),
            nameSuffix: strdupC(components.nameSuffix),
            nickname: strdupC(components.nickname),
            phoneticRepresentation: wrapPersonNameComponents(components: components.phoneticRepresentation)
        )
        
        let wrapperPtr = UnsafeMutablePointer<PersonNameComponentsWrapper>.allocate(capacity: 1)
        wrapperPtr.initialize(to: wrapper)
        return UnsafePointer(wrapperPtr)
    }

    public func authorizationController(controller: ASAuthorizationController, didCompleteWithError error: any Error) {
        let onFailureCallback = AppleIdAuthShared.onSignInFailed
        AppleIdAuthShared.clearSharedData()
        
        guard let authorizationError = error as? ASAuthorizationError else {
            logger.error("Apple Sign-In failed due to error: \(error.localizedDescription)")
            onFailureCallback?(AppleIdAuthorizationErrorCode_Unknown)
            return;
        }
        
        logger.error("Apple Sign-In failed due to error, code: \(authorizationError.code.rawValue), description: \(authorizationError.localizedDescription)")
        onFailureCallback?(AppleIdAuthorizationErrorCode(Int16(authorizationError.code.rawValue)))
    }
}
