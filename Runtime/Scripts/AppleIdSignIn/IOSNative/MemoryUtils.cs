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
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

#nullable enable
namespace Uralstech.UMoth.AppleIdSignIn.Native
{
    /// <summary>
    /// Utilities for handling native memory.
    /// </summary>
    public static class MemoryUtils
    {
        /// <summary>
        /// Reads a native buffer into managed memory as a byte array.
        /// </summary>
        /// <param name="ptr">The buffer to read.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns>The buffer's contents or <see langword="null"/> if <paramref name="ptr"/> was a nullptr.</returns>
        public static byte[]? ReadIntoManagedArray(this IntPtr ptr, uint size)
        {
            if (ptr == IntPtr.Zero || size == 0)
                return null;

            byte[] buffer = new byte[size];
            Marshal.Copy(ptr, buffer, 0, (int)size);
            return buffer;
        }

        /// <summary>
        /// Reads a native buffer into managed memory as an <see cref="IMemoryOwner{T}"/>.
        /// </summary>
        /// <param name="ptr">The buffer to read.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns>The buffer's contents or <see langword="null"/> if <paramref name="ptr"/> was a nullptr.</returns>
        public static unsafe IMemoryOwner<byte>? ReadIntoManagedMemoryOwner(this IntPtr ptr, uint size)
        {
            if (ptr == IntPtr.Zero || size == 0)
                return null;

            IMemoryOwner<byte> memoryOwner = MemoryPool<byte>.Shared.Rent((int)size);
            fixed (byte* destinationPtr = memoryOwner.Memory.Span)
            {
                Buffer.MemoryCopy(ptr.ToPointer(), destinationPtr, size, size);
            }

            return memoryOwner;
        }

        /// <summary>
        /// Decodes a UTF-8 byte memory into a string.
        /// </summary>
        /// <param name="bytes">The data to decode.</param>
        /// <returns>The decoded string.</returns>
        public static string DecodeUtf8(this ReadOnlyMemory<byte> bytes)
        {
            return Encoding.UTF8.GetString(bytes.Span);
        }

        /// <summary>
        /// Decodes a UTF-8 byte array into a string.
        /// </summary>
        /// <param name="bytes">The data to decode.</param>
        /// <returns>The decoded string.</returns>
        public static string DecodeUtf8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}