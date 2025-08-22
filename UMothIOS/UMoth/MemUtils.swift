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

import Foundation

internal func strdupC(_ str: String?) -> UnsafePointer<CChar>? {
    guard let str = str else {
        return nil
    }
    
    let cStr = strdup(str)
    return UnsafePointer(cStr)
}

internal func allocateNativeBuffer(from data: Data?) -> UnsafePointer<UInt8>? {
    guard let data = data else {
        return nil
    }
    
    let buffer = UnsafeMutablePointer<UInt8>.allocate(capacity: data.count)
    data.copyBytes(to: buffer, count: data.count)
    return UnsafePointer(buffer)
}

@_cdecl("umoth_free_native_buffer")
public func freeNativeBuffer(ptr: UnsafeMutableRawPointer) {
    ptr.deallocate()
}
