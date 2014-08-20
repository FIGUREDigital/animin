//
//  AperturePreviewViewController.m
//
//  Created by Christopher Baltzer on 2013-08-30.
//  Copyright (c) 2013 Christopher Baltzer. All rights reserved.
//

#import "AperturePreviewViewController.h"
#import "Aperture.h"

void UnitySendMessage(const char*, const char*, const char*);
extern void UnityPause(bool pause);

@interface AperturePreviewViewController ()

@end

@implementation AperturePreviewViewController

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    self.imageView.image = [Aperture sharedInstance].currentFrame;
    if ([Aperture sharedInstance].pauseUnityOnPreview) {
        UnityPause(true);
    }
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)dealloc {
    [_imageView release];
    [super dealloc];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)toInterfaceOrientation {
    return YES;
}

-(NSUInteger)supportedInterfaceOrientations
{
    return UIInterfaceOrientationMaskAll;
}

- (UIInterfaceOrientation)preferredInterfaceOrientationForPresentation
{
    return [UIApplication sharedApplication].statusBarOrientation;
}


- (IBAction)acceptButton:(UIBarButtonItem *)sender {
    UnityPause(false);
    [[Aperture sharedInstance] acceptPreview];
}


- (IBAction)cancelButton:(UIBarButtonItem *)sender {
    UnityPause(false);
    [[Aperture sharedInstance] cancelPreview];
}

@end
