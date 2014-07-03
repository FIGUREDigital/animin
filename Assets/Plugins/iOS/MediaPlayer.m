#import <UIKit/UIKit.h>
#import <MediaPlayer/MediaPlayer.h>
#import "MediaPlayer.h"


void _setVideo(const char * filename)
{
    NSLog(@"Native plugin: %@", [NSString stringWithUTF8String:filename]);
}

void _play()
{
    
}

void _pause()
{
}

void _initMediaPlayer()
{
    [[AniminMediaPlayer sharedInstance] initializeAll];
}




@implementation AniminMediaPlayer

static AniminMediaPlayer *sharedInstance = nil;
+(AniminMediaPlayer*)sharedInstance {
    if( !sharedInstance )
        sharedInstance = [[AniminMediaPlayer alloc] init];
    
    return sharedInstance;
}


- (void) initializeAll
{
    
    musicPlayer = [MPMusicPlayerController iPodMusicPlayer];
    /*
     
     [volumeSlider setValue:[musicPlayer volume]];
     
     if ([musicPlayer playbackState] == MPMusicPlaybackStatePlaying) {
     
     [playPauseButton setImage:[UIImage imageNamed:@"pauseButton.png"] forState:UIControlStateNormal];
     
     } else {
     
     [playPauseButton setImage:[UIImage imageNamed:@"playButton.png"] forState:UIControlStateNormal];
     }
     */
    
    NSNotificationCenter *notificationCenter = [NSNotificationCenter defaultCenter];
    
    [notificationCenter addObserver: self
                           selector: @selector (handle_NowPlayingItemChanged:)
                               name: MPMusicPlayerControllerNowPlayingItemDidChangeNotification
                             object: musicPlayer];
    
    [notificationCenter addObserver: self
                           selector: @selector (handle_PlaybackStateChanged:)
                               name: MPMusicPlayerControllerPlaybackStateDidChangeNotification
                             object: musicPlayer];
    
    [notificationCenter addObserver: self
                           selector: @selector (handle_VolumeChanged:)
                               name: MPMusicPlayerControllerVolumeDidChangeNotification
                             object: musicPlayer];
    
    [musicPlayer beginGeneratingPlaybackNotifications];
    
}

- (void) handle_NowPlayingItemChanged: (id) notification
{
    MPMediaItem *currentItem = [musicPlayer nowPlayingItem];
    UIImage *artworkImage = [UIImage imageNamed:@"noArtworkImage.png"];
    MPMediaItemArtwork *artwork = [currentItem valueForProperty: MPMediaItemPropertyArtwork];
    
    /* if (artwork) {
     artworkImage = [artwork imageWithSize: CGSizeMake (200, 200)];
     }*/
    
    //[artworkImageView setImage:artworkImage];
    
    NSString *titleString = [currentItem valueForProperty:MPMediaItemPropertyTitle];
    /*if (titleString) {
     titleLabel.text = [NSString stringWithFormat:@"Title: %@",titleString];
     } else {
     titleLabel.text = @"Title: Unknown title";
     }*/
    
    NSString *artistString = [currentItem valueForProperty:MPMediaItemPropertyArtist];
    /* if (artistString) {
     artistLabel.text = [NSString stringWithFormat:@"Artist: %@",artistString];
     } else {
     artistLabel.text = @"Artist: Unknown artist";
     }*/
    
    NSString *albumString = [currentItem valueForProperty:MPMediaItemPropertyAlbumTitle];
    /*if (albumString) {
     albumLabel.text = [NSString stringWithFormat:@"Album: %@",albumString];
     } else {
     albumLabel.text = @"Album: Unknown album";
     }*/
    
}

- (void) handle_VolumeChanged: (id) notification
{
    //[volumeSlider setValue:[musicPlayer volume]];
}

- (IBAction)volumeChanged:(id)sender
{
    //[musicPlayer setVolume:[volumeSlider value]];
}

- (IBAction)previousSong:(id)sender
{
    // [musicPlayer skipToPreviousItem];
}

- (IBAction)playPause:(id)sender
{
    /*
     if ([musicPlayer playbackState] == MPMusicPlaybackStatePlaying) {
     [musicPlayer pause];
     
     } else {
     [musicPlayer play];
     
     }
     */
}

- (IBAction)showMediaPicker:(id)sender
{
    MPMediaPickerController *mediaPicker = [[MPMediaPickerController alloc] initWithMediaTypes: MPMediaTypeAny];
    
    mediaPicker.delegate = self;
    mediaPicker.allowsPickingMultipleItems = YES;
    mediaPicker.prompt = @"Select songs to play";
    
    [self presentModalViewController:mediaPicker animated:YES];
    [mediaPicker release];
    
}

- (void) mediaPicker: (MPMediaPickerController *) mediaPicker didPickMediaItems: (MPMediaItemCollection *) mediaItemCollection
{
    
    if (mediaItemCollection) {
        
        [musicPlayer setQueueWithItemCollection: mediaItemCollection];
        [musicPlayer play];
    }
    
    [self dismissModalViewControllerAnimated: YES];
    
}

- (void) mediaPickerDidCancel: (MPMediaPickerController *) mediaPicker
{
    [self dismissModalViewControllerAnimated: YES];
}

- (IBAction)nextSong:(id)sender
{
    [musicPlayer skipToNextItem];
}

- (void) handle_PlaybackStateChanged: (id) notification
{
    
    MPMusicPlaybackState playbackState = [musicPlayer playbackState];
    
    if (playbackState == MPMusicPlaybackStatePaused) {
        // [playPauseButton setImage:[UIImage imageNamed:@"playButton.png"] forState:UIControlStateNormal];
        
    }
    else if (playbackState == MPMusicPlaybackStatePlaying) {
        //[playPauseButton setImage:[UIImage imageNamed:@"pauseButton.png"] forState:UIControlStateNormal];
        
    } else if (playbackState == MPMusicPlaybackStateStopped) {
        
        //[playPauseButton setImage:[UIImage imageNamed:@"playButton.png"] forState:UIControlStateNormal];
        [musicPlayer stop];
        
    }
    
}

- (void)  DestroyAll
{
    [[NSNotificationCenter defaultCenter] removeObserver: self
                                                    name: MPMusicPlayerControllerNowPlayingItemDidChangeNotification
                                                  object: musicPlayer];
    
    [[NSNotificationCenter defaultCenter] removeObserver: self
                                                    name: MPMusicPlayerControllerPlaybackStateDidChangeNotification
                                                  object: musicPlayer];
    
    [[NSNotificationCenter defaultCenter] removeObserver: self
                                                    name: MPMusicPlayerControllerVolumeDidChangeNotification
                                                  object: musicPlayer];
    
    [musicPlayer endGeneratingPlaybackNotifications];
    
    [musicPlayer release];
}

@end