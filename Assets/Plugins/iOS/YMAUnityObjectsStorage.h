#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <Foundation/Foundation.h>

@interface YMAUnityObjectsStorage: NSObject

+ (instancetype)sharedInstance;

- (id)objectWithID:(const char*)objectID;
- (void)setObject:(id)object withID:(const char*)objectID;
- (void)removeObjectWithID:(const char*)objectID;

@end
