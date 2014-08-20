//
//  Aperture.h
//  ApertureDevelopment
//
//  Created by  on 2013-07-06.
//  Copyright (c) 2013 Christopher Baltzer. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern bool apertureCaptureFrame;

// Unity bridge
void _setSaveToCameraRoll(bool);
void _setSaveToDisk(bool);
void _setPreviewAnimation(int);
void _setPauseUnityOnPreview(bool);

void _photo();

void _showPreview();
void _hidePreview();

void _destroy();


    
@interface Aperture : NSObject

@property (nonatomic, retain) UIImage* currentFrame;
@property (nonatomic, assign) BOOL photoCapture;

// Config settings
@property (nonatomic, assign) BOOL saveToCameraRoll;
@property (nonatomic, assign) BOOL saveToDisk;
@property (nonatomic, assign) BOOL saveToTexture;
@property (nonatomic, assign) GLint targetTex;

@property (nonatomic, assign) int previewAnimation;
@property (nonatomic, assign) BOOL pauseUnityOnPreview;

+(Aperture*)sharedInstance;

-(void)captureAntiAliasedFrame:(GLuint)msaaFB targetFB:(GLuint)targetFB;
-(void)captureFrame;
-(void)endCaptureFrame;
-(void)beginSaveFrameAsync;

-(void)startPhoto;

-(void)saveCurrentFrameToTexture;
-(void)saveCurrentFrame;
-(void)captureComplete;

-(void)showPreview;
-(void)hidePreview;
-(void)acceptPreview;
-(void)cancelPreview;

@end