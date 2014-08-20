//
//  Aperture.m
//  ApertureDevelopment
//
//  Created by Christopher Baltzer on 2013-07-06.
//  Copyright (c) 2013 Christopher Baltzer. All rights reserved.
//

#import "Aperture.h"
#import <GLKit/GLKit.h>
#import <QuartzCore/QuartzCore.h>


#pragma mark Unity Bridge

void UnitySendMessage(const char *, const char *, const char *);

void _setSaveToCameraRoll(bool shouldSave) {
    [[Aperture sharedInstance] setSaveToCameraRoll:shouldSave];
}

void _setSaveToDisk(bool shouldSave) {
    [[Aperture sharedInstance] setSaveToDisk:shouldSave];
}

void _setSaveToTexture(bool shouldSave) {
    [[Aperture sharedInstance] setSaveToTexture:shouldSave];
}

void _setTargetTextureID(GLuint texID) {
    [[Aperture sharedInstance] setTargetTex:texID];
}

void _setPreviewAnimation(int animation) {
    [[Aperture sharedInstance] setPreviewAnimation:animation];
}

void _setPauseUnityOnPreview(bool shouldPause) {
    [[Aperture sharedInstance] setPauseUnityOnPreview:shouldPause];
}

void _photo() {
    [[Aperture sharedInstance] startPhoto];
}

void _showPreview() {
    [[Aperture sharedInstance] showPreview];
}

void _hidePreview() {
    [[Aperture sharedInstance] hidePreview];
}

void _destroy() {
    [[Aperture sharedInstance] dealloc];
}



#pragma mark - Static

bool apertureCaptureFrame = false;



#pragma mark - Interface

@interface Aperture()

@property NSDateFormatter *dateFormatter;

@property (nonatomic, assign) NSInteger imageWidth;
@property (nonatomic, assign) NSInteger imageHeight;
@property (nonatomic, assign) CGSize imageSize;
@property (nonatomic, assign) CGFloat scale;


@property (nonatomic, assign) CGRect drawRect;
@property (nonatomic, assign) CGColorSpaceRef colorspace;
@property (nonatomic, assign) CGDataProviderRef dataRef;
@property (nonatomic, assign) CGImageRef imageRef;


@property (nonatomic, assign) NSInteger dataLength;
@property (nonatomic, assign) GLubyte *data;

@end



#pragma mark - Setup

@implementation Aperture

static Aperture *sharedInstance = nil;
+(Aperture*)sharedInstance {
	if( !sharedInstance )
		sharedInstance = [[Aperture alloc] init];
    
	return sharedInstance;
}


-(id)init {
    if (self = [super init]) {
        apertureCaptureFrame = false;
        self.photoCapture = NO;
        
        // setup date formatter for filename generation
        self.dateFormatter = [[NSDateFormatter alloc] init];
        [self.dateFormatter setDateFormat:@"HH-mm-ss"];
        
        
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(deviceOrientationDidChangeNotification:)
                                                     name:UIDeviceOrientationDidChangeNotification
                                                   object:nil];
        
        
        [self configureBuffers];
        
    }
    
    return self;
}

- (void)deviceOrientationDidChangeNotification:(NSNotification*)note
{
    [self performSelector:@selector(reconfigureAfterRotation) withObject:nil afterDelay:0.2];
}

-(void)reconfigureAfterRotation {
    [self cleanupBuffers];
    [self configureBuffers];
}

-(void)configureBuffers {
    NSLog(@"Reconfiguring Aperture");
    GLint backingWidth, backingHeight;
    
    glGetRenderbufferParameterivOES(GL_RENDERBUFFER_OES, GL_RENDERBUFFER_WIDTH_OES, &backingWidth);
    glGetRenderbufferParameterivOES(GL_RENDERBUFFER_OES, GL_RENDERBUFFER_HEIGHT_OES, &backingHeight);
    
    
    self.imageWidth = backingWidth;
    self.imageHeight = backingHeight;
    self.dataLength = self.imageWidth * self.imageHeight * 4;
    self.data = (GLubyte*)malloc(self.dataLength * sizeof(GLubyte));
    
    self.scale = 1.0;
    NSInteger widthInPoints = self.imageWidth / self.scale;
    NSInteger heightInPoints = self.imageHeight / self.scale;
    self.imageSize = CGSizeMake(widthInPoints, heightInPoints);
    
    self.drawRect = CGRectMake(0.0, 0.0, widthInPoints, heightInPoints);
    self.colorspace = CGColorSpaceCreateDeviceRGB();
    
    self.dataRef = CGDataProviderCreateWithData(NULL, self.data, self.dataLength, NULL);
    
    
    self.imageRef = CGImageCreate(self.imageWidth, self.imageHeight, 8, 32, self.imageWidth * 4,
                                  self.colorspace,
                                  kCGBitmapByteOrder32Big | kCGImageAlphaNoneSkipLast,
                                  self.dataRef,
                                  NULL,
                                  true,
                                  kCGRenderingIntentDefault);
}



#pragma mark - Teardown

-(void)cleanupBuffers {
    // Clean up
    free(self.data);
    CFRelease(self.dataRef);
    CFRelease(self.colorspace);
    CGImageRelease(self.imageRef);
}

-(void) dealloc {
    if (self.currentFrame != nil) {
        [self.currentFrame release];
    }
    
    [self.dateFormatter release];
    self.dateFormatter = nil;
    
    [self cleanupBuffers];
    
    [[NSNotificationCenter defaultCenter] removeObserver:self];
    
    // destroy singleton
    sharedInstance = nil;
    
    [super dealloc];
}



#pragma mark - Capture controls

-(void)startPhoto {
    apertureCaptureFrame = true;
    self.photoCapture = YES;
}


-(void)stopPhoto {
    [self saveCurrentFrame];
    self.photoCapture = NO;
}



#pragma mark - Frame capture

extern GLint gDefaultFBO;
-(void)captureAntiAliasedFrame:(GLuint)msaaFB targetFB:(GLuint)targetFB
{
    glBindFramebufferOES(GL_READ_FRAMEBUFFER_APPLE, msaaFB);
    glBindFramebufferOES(GL_DRAW_FRAMEBUFFER_APPLE, targetFB);
    glResolveMultisampleFramebufferAPPLE();
        
    glBindFramebufferOES(GL_FRAMEBUFFER_OES, targetFB);
        
    glPixelStorei(GL_PACK_ALIGNMENT, 4);
    glReadPixels(0, 0, self.imageWidth, self.imageHeight, GL_RGBA, GL_UNSIGNED_BYTE, self.data);
    
    [self endCaptureFrame];
}


-(void)captureFrame {
    
    glBindFramebufferOES(GL_FRAMEBUFFER_OES, gDefaultFBO);
    
    glPixelStorei(GL_PACK_ALIGNMENT, 4);
    glReadPixels(0, 0, self.imageWidth, self.imageHeight, GL_RGBA, GL_UNSIGNED_BYTE, self.data);
    
    [self endCaptureFrame];
}


-(void)endCaptureFrame {
    if (self.saveToTexture) {
        // do this here, synchronously, or you only get black
        [self saveCurrentFrameToTexture];
    }
    
    [self beginSaveFrameAsync];
    
    apertureCaptureFrame = false;
}


-(void)beginSaveFrameAsync {
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_LOW, 0), ^{
        
        if (self.photoCapture) {
                        
            UIGraphicsBeginImageContextWithOptions(self.imageSize, NO, self.scale);
            CGContextRef cgcontext = UIGraphicsGetCurrentContext();
            
            CGContextSetBlendMode(cgcontext, kCGBlendModeCopy);
            CGContextDrawImage(cgcontext, self.drawRect, self.imageRef);
            
            UIImage *image = UIGraphicsGetImageFromCurrentImageContext();
            
            UIGraphicsEndImageContext();
            
            self.currentFrame = image;
            [self stopPhoto];
        }
        
    });
}



#pragma mark - Output

-(void)saveCurrentFrameToTexture {
    // abort if we're writing into nothing
    if (self.targetTex == 0) return;
    
    glBindTexture(GL_TEXTURE_2D, self.targetTex);
    
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, self.imageWidth, self.imageHeight, 0, GL_RGBA, GL_UNSIGNED_BYTE, self.data);
    
}

-(void)saveCurrentFrame {
    if (self.saveToCameraRoll) {
        UIImageWriteToSavedPhotosAlbum(self.currentFrame, nil, nil, nil);
    }
    
    if (self.saveToDisk) {
        dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_LOW, 0), ^{
            // Get date-time for filename
            NSDate * now = [NSDate date];
            NSString *path = [NSString stringWithFormat:@"Documents/Photo-%@.png",[self.dateFormatter stringFromDate:now]];
            
            // Write to docs directory
            NSString *outputPath = [NSHomeDirectory() stringByAppendingPathComponent:path];
            [UIImagePNGRepresentation(self.currentFrame) writeToFile:outputPath atomically:YES];
            
            UnitySendMessage([@"Aperture" UTF8String], [@"SavedToDisk" UTF8String], [outputPath UTF8String]);
        });
    }
    
    // Complete
    [self captureComplete];
}


-(void)captureComplete {
    UnitySendMessage([@"Aperture" UTF8String], [@"OnComplete" UTF8String], [@"" UTF8String]);
}



#pragma mark - Preview control

-(void)showPreview {
    UIViewController *vc = [[NSClassFromString(@"AperturePreviewViewController") alloc] initWithNibName:@"AperturePreviewiPhone" bundle:nil];
    
    BOOL animated = YES;
    switch (self.previewAnimation) {
        case 0:
            animated = NO;
            break;
        case 1:
            vc.modalTransitionStyle = UIModalTransitionStyleCoverVertical;
            break;
        case 2:
            vc.modalTransitionStyle = UIModalTransitionStyleCrossDissolve;
            break;
    
    }
    
    UIWindow *window = [UIApplication sharedApplication].keyWindow;
    [window.rootViewController presentViewController:vc animated:animated completion:nil];
}


-(void)hidePreview {
    UIWindow *window = [UIApplication sharedApplication].keyWindow;
    [window.rootViewController dismissViewControllerAnimated:NO completion:nil];
}

-(void)acceptPreview {
    [self hidePreview];
    UnitySendMessage([@"Aperture" UTF8String], [@"AcceptPreview" UTF8String], [@"" UTF8String]);
}

-(void)cancelPreview {
    [self hidePreview];
    UnitySendMessage([@"Aperture" UTF8String], [@"CancelPreview" UTF8String], [@"" UTF8String]);
}

@end