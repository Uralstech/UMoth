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
    /// Wrapper for ASUserAgeRange.
    /// </summary>
    public enum AppleIdUserAgeRange
    {
        /// <summary>
        /// The age range was not returned during the sign-in operation.
        /// </summary>
        /// <remarks>
        /// This is not a part of ASUserAgeRange, but is returned if the device or OS does not support ASUserAgeRange.
        /// </remarks>
        NotAvailable = 0,

        /// <summary>
        /// This is returned if the project is missing the required entitlement to support child accounts.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// The user is a child.
        /// </summary>
        Child = 2,

        /// <summary>
        /// The user is not a child.
        /// </summary>
        NotChild = 3
    }
}