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

#ifndef AppleIdAuthWrappers_h
#define AppleIdAuthWrappers_h

#include <stdint.h>

typedef enum : uint8_t {
    AppleIdCredentialStateWrapper_Revoked,
    AppleIdCredentialStateWrapper_Authorized,
    AppleIdCredentialStateWrapper_NotFound,
    AppleIdCredentialStateWrapper_Transferred,
    
    AppleIdCredentialStateWrapper_PluginError = 255
} AppleIdCredentialStateWrapper;

typedef enum : int16_t {
    AppleIdAuthorizationErrorCode_UnknownCredentialType = -1,
    AppleIdAuthorizationErrorCode_Unknown = 1000,
    AppleIdAuthorizationErrorCode_Canceled = 1001,
    AppleIdAuthorizationErrorCode_InvalidResponse = 1002,
    AppleIdAuthorizationErrorCode_NotHandled = 1003,
    AppleIdAuthorizationErrorCode_Failed = 1004,
    AppleIdAuthorizationErrorCode_NotInteractive = 1005,
    AppleIdAuthorizationErrorCode_MatchedExcludedCredential = 1006,
    AppleIdAuthorizationErrorCode_CredentialImport = 1007,
    AppleIdAuthorizationErrorCode_CredentialExport = 1008
} AppleIdAuthorizationErrorCode;

typedef enum : uint8_t {
    AppleIdScopeWrapper_None        = 0,
    AppleIdScopeWrapper_FullName    = 1 << 0,
    AppleIdScopeWrapper_Email       = 1 << 1
} AppleIdScopeWrapper;

typedef enum : uint8_t {
    UserDetectionStatusWrapper_Unsupported  = 0,
    UserDetectionStatusWrapper_Unknown      = 1,
    UserDetectionStatusWrapper_LikelyReal   = 2
} UserDetectionStatusWrapper;

typedef enum : uint8_t {
    UserAgeRangeWrapper_NotAvailable    = 0,
    UserAgeRangeWrapper_Unknown         = 1,
    UserAgeRangeWrapper_Child           = 2,
    UserAgeRangeWrapper_NotChild        = 3
} UserAgeRangeWrapper;

typedef struct PersonNameComponentsWrapper {
    const char* namePrefix;
    const char* givenName;
    const char* middleName;
    const char* familyName;
    const char* nameSuffix;
    const char* nickname;
    const struct PersonNameComponentsWrapper* phoneticRepresentation;
} PersonNameComponentsWrapper;

typedef struct {
    const char* userId;
    const char* state;
    const AppleIdScopeWrapper scopes;
    
    const char* authorizationCode;
    const char* identityToken;
    
    const char* email;
    const PersonNameComponentsWrapper* fullName;
    
    const UserDetectionStatusWrapper realUserStatus;
    const UserAgeRangeWrapper userAgeRange;
} AppleIdCredentialWrapper;

#endif /* AppleIdAuthWrappers_h */
