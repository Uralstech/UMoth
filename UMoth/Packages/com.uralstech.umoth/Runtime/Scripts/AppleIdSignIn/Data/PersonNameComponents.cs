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
    /// The components of the user's name.
    /// </summary>
    public record PersonNameComponents
    {
        /// <summary>
        /// Prefix to the user's name, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? NamePrefix;

        /// <summary>
        /// The user's given name, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? GivenName;

        /// <summary>
        /// The user's middle name, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? MiddleName;

        /// <summary>
        /// The user's family name, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? FamilyName;

        /// <summary>
        /// Suffix to the user's name, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? NameSuffix;

        /// <summary>
        /// The user's nickname, or its phonetic representation if this object is from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public readonly string? Nickname;

        /// <summary>
        /// Phonetic representation of this object's values.
        /// </summary>
        public readonly PersonNameComponents? PhoneticRepresentation;

        internal protected PersonNameComponents(string? namePrefix, string? givenName, string? middleName, string? familyName, string? nameSuffix, string? nickname, PersonNameComponents? phoneticRepresentation)
        {
            NamePrefix = namePrefix;
            GivenName = givenName;
            MiddleName = middleName;
            FamilyName = familyName;
            NameSuffix = nameSuffix;
            Nickname = nickname;
            PhoneticRepresentation = phoneticRepresentation;
        }
    }
}