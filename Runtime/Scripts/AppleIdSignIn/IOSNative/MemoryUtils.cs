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
using System;
using System.Runtime.InteropServices;

namespace Uralstech.UMoth.AppleIdSignIn.Native
{
    /// <summary>
    /// Utilities for handling native memory.
    /// </summary>
    public static class MemoryUtils
    {
        /// <summary>
        /// Read an ANSI string from a pointer.
        /// </summary>
        /// <param name="ptr">The pointer to read from.</param>
        /// <returns>The string read from the pointer, <see langword="null"/> if <paramref name="ptr"/> is <see langword="null"/>.</returns>
        public static string? ReadAnsiString(IntPtr ptr)
        {
            return ptr != IntPtr.Zero
                ? Marshal.PtrToStringAnsi(ptr)
                : null;
        }

        /// <summary>
        /// Tries to release a native string using <see cref="NativeCalls.umoth_free_native_string(IntPtr)"/>
        /// </summary>
        /// <param name="ptr">The pointer the string to release.</param>
        public static void TryReleaseString(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
                NativeCalls.umoth_free_native_string(ptr);
        }
    }
}