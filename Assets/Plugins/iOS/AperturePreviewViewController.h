//
//  AperturePreviewViewController.h
//  ApertureDevelopment
//
//  Created by Christopher Baltzer on 2013-08-30.
//  Copyright (c) 2013 Christopher Baltzer. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface AperturePreviewViewController : UIViewController

@property (retain, nonatomic) IBOutlet UIImageView *imageView;

- (IBAction)acceptButton:(UIBarButtonItem *)sender;
- (IBAction)cancelButton:(UIBarButtonItem *)sender;

@end
