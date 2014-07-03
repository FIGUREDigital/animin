void _setVideo(const char *);
void _play();
void _pause() ;
void _initMediaPlayer();

@interface AniminMediaPlayer : UIViewController <MPMediaPickerControllerDelegate>
{
    MPMusicPlayerController *musicPlayer;
    
}

@end