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

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn
{
    /// <summary>
    /// The kinds of contact information that can be requested from the user.
    /// </summary>
    [Flags]
    public enum AppleIdScope
    {
        /// <summary>
        /// No scopes have been authorized.
        /// </summary>
        None = 0,

        /// <summary>
        /// A scope that includes the user's email address.
        /// </summary>
        FullName = 1 << 0,

        /// <summary>
        /// A scope that includes the user's full name.
        /// </summary>
        Email = 1 << 1
    }
}