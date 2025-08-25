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
import os.log
import UIKit

private let loggerTag = "UMoth_Native"
internal let logger: Logger = {
    return Bundle.main.bundleIdentifier.map {
        Logger(subsystem: $0, category: loggerTag)
    } ?? Logger()
}()

internal func getUnityBaseClass() -> NSObject.Type? {
    let bundlePath = Bundle.main.bundlePath.appending("/Frameworks/UnityFramework.framework")
    guard let bundle = Bundle(path: bundlePath) else {
        return nil
    }
    
    return (bundle.principalClass as! NSObject.Type)
}

internal func getUnityRootViewController() -> UIViewController? {
    guard let unityBaseClass = getUnityBaseClass() else {
        return nil
    }
    
    let instance = unityBaseClass.value(forKey: "getInstance") as! NSObject
    let appController = instance.value(forKey: "appController") as! NSObject
    return (appController.value(forKey: "rootViewController") as! UIViewController)
}
