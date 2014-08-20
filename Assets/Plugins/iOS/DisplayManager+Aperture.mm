//
//  DisplayManager+Aperture.m
//
//  Created by Christopher Baltzer on 2013-07-18.
//
//

#import "DisplayManager+Aperture.h"
#import <objc/runtime.h>
#import <objc/message.h>

@implementation DisplayConnection (Aperture)

+ (void)load {
    Method original, swizzled;
    
    original = class_getInstanceMethod(self, @selector(present));
    swizzled = class_getInstanceMethod(self, @selector(presentWithFrameCapture));
    method_exchangeImplementations(original, swizzled);
}

extern bool apertureCaptureFrame;
- (void)presentWithFrameCapture
{
    if (apertureCaptureFrame) {
        
        GLuint targetFB = surface.targetFB ? surface.targetFB : surface.systemFB;
        [[Aperture sharedInstance] captureAntiAliasedFrame:surface.msaaFB targetFB:targetFB];

    }
    [self presentWithFrameCapture];
}

@end
