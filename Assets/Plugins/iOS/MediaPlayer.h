void _setVideo(const char *);
void _play();
void _pause() ;
int _initMediaPlayer();
void _moveToNextSong();
void _moveToPreviousSong();
float _getProgress();
const char* _getNextSongFromList();
void _playSongAtIndex(int index);

@interface AniminMediaPlayer : UIViewController <MPMediaPickerControllerDelegate>
{
    MPMusicPlayerController *musicPlayer;
    NSArray* itemsFromGenericQuery;
    int nextSongIndex;
    
}

+ (AniminMediaPlayer*)sharedInstance;
- (int) initializeAll;
- (void)showMediaPicker;
-(void)playPause;
-(void)previousSong;
-(void)nextSong;
-(float)getProgress;
-(const char*)getNextSongFromList;
-(void)playSongAtIndex:(int)index;

@end