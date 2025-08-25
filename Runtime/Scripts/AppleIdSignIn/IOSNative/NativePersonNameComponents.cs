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
using System.Runtime.InteropServices;

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn.Native
{
    /// <summary>
    /// Wrapper for iOS's PersonNameComponents.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativePersonNameComponents
    {
        /// <summary>
        /// Prefix to the user's name, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr NamePrefix;

        /// <summary>
        /// The user's given name, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr GivenName;

        /// <summary>
        /// The user's middle name, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr MiddleName;

        /// <summary>
        /// The user's family name, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr FamilyName;

        /// <summary>
        /// Suffix to the user's name, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr NameSuffix;

        /// <summary>
        /// The user's nickname, or its phonetic representation if this struct is created from <see cref="PhoneticRepresentation"/>.
        /// </summary>
        public IntPtr Nickname;

        /// <summary>
        /// Phonetic representation of this struct's values.
        /// </summary>
        /// <remarks>
        /// This is an instance of <see cref="NativePersonNameComponents"/>.
        /// If it has not been unwrapped, it will be freed when this struct's <see cref="Dispose"/> is called.
        /// If unwrapped using <see cref="UnwrapPhoneticRepresentation"/>, the caller is responsible for disposing the returned object.
        /// </remarks>
        public IntPtr PhoneticRepresentation;

        /// <summary>
        /// Unwraps <see cref="PhoneticRepresentation"/> into an instance of <see cref="NativePersonNameComponents"/>.
        /// </summary>
        /// <remarks>
        /// This can only be called once, as after <see cref="PhoneticRepresentation"/> is unwrapped, the reference is
        /// deallocated. This is to prevent multiple copies of the same memory from being created.
        /// </remarks>
        /// <returns>The unwrapped struct or <see langword="null"/> if <see cref="PhoneticRepresentation"/> is a nullptr.</returns>
        public NativePersonNameComponents? UnwrapPhoneticRepresentation()
        {
            if (PhoneticRepresentation == IntPtr.Zero)
                return null;

            NativePersonNameComponents structure = Marshal.PtrToStructure<NativePersonNameComponents>(PhoneticRepresentation);
            NativeCalls.umoth_free_native_buffer(PhoneticRepresentation);
            PhoneticRepresentation = IntPtr.Zero;
            return structure;
        }

        /// <summary>
        /// Deallocates native resources.
        /// </summary>
        /// <remarks>
        /// This struct has been intentionally not set as IDisposable to prevent
        /// accidental struct copies caused by "using" statements.
        /// </remarks>
        public void Dispose()
        {
            if (UnwrapPhoneticRepresentation() is NativePersonNameComponents components)
            {
                // If at this point PhoneticRepresentation has not been accessed, manually deallocate it.
                components.Dispose();
            }

            MemoryUtils.TryReleaseString(NamePrefix);
            NamePrefix = IntPtr.Zero;

            MemoryUtils.TryReleaseString(GivenName);
            GivenName = IntPtr.Zero;

            MemoryUtils.TryReleaseString(MiddleName);
            MiddleName = IntPtr.Zero;

            MemoryUtils.TryReleaseString(FamilyName);
            FamilyName = IntPtr.Zero;

            MemoryUtils.TryReleaseString(NameSuffix);
            NameSuffix = IntPtr.Zero;

            MemoryUtils.TryReleaseString(Nickname);
            Nickname = IntPtr.Zero;
        }
    }
}