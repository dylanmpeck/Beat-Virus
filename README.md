# Beat Virus
![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BVGif.gif)

[Full Gameplay Demo](https://www.youtube.com/watch?v=9JyZwx7B5Ws)

## Overview
Beat Virus is a rhythm based shooter game in virtual reality developed with MRTK in Unity. In Beat Virus, multi colored germ-like enemies spawn to the beat of the music and float towards the player. In order to survive and score points, the player must fight back with two different colored projectile weapons and hit the the enemy with the matching color. On top of that, one will build a combo multiplier if he or she shoots the enemies on beat.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BeatVirusThumbnail%20(2).png)

## Audio Visualization
My main goal with this was to explore audio visualization in Unity and how I could integrate it with mixed reality gameplay. In Beat Virus, almost every element of the world around the player is reacting to the music in some shape or form. To do this, I used Unity's built in Fast Fourier transform method which they call [GetSpectrumData](https://docs.unity3d.com/ScriptReference/AudioSource.GetSpectrumData.html).

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FFT.png)

I pass in a float array of size 512 (samplesLeft and samplesRight in the image) which is filled up with the audio spectrum data in the current timestamp. In this case, the whole frequency spectrum gets split up in 512 parts, or samples, from low to high accordingly and higher numbers in the float array will represent bigger amplitudes for the frequency band of its index.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FFTTest.gif)

So, thanks to Unity, a lot of hard work is done already, however, 512 different freqeuency bands is a bit difficult to work with, and we want to use sample float arrays of about 512 or 1024 to get accurate representations of the audio from GetSpectrumData(). At this point, it's up to the programmer how he or she wants to shape the data for the application, but I decided to condense it into 8 frequency bands (low to high). With this, kick and bass sounds would generally be in the first or second frequency band, and it wasn't too difficult to isolate mids and highs either. I used this math to separate them.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FreqMath.png)

If my song is exported with a sample rate of 22050 and we're using a sample spectrum data array of 512, then each sample, or index of the array, will represent roughly 43hz on the frequency spectrum. So, the first two samples would represent 0hz-83hz, the next four samples (indexes 2-5) would represent 87hz-258hz, and so on. Using powers of 2 separated the frequency bands pretty nicely but ended up with a total of 510, so I added an extra two samples to the last frequency band. The C# code for this looks like this:

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FreqFunc.png)

I average the samples, or indexes, previously assigned to each frequency band, and then scale them up by 10 as the number was a bit low to work with. Lastly, to make the frequency bands a little more Unity friendly, I created a matching-sized AudioBand array which represents the frequency bands as a number between 0 and 1 by dividing by the current max for that frequency band.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/AudioBands.png)

With this, I'm able to multiply the frequency band, audio band, or average of the entire audio band array (which gives me overall amplitude) with any parameter to get cool audio visualization effects. The results are seen in the game with the tunnel warping to the kick drums, the tunnel's colors shifting to the beat, and the enemies bouncing around to different frequency bands.




