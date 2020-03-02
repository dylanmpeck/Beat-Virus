# Beat Virus
![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BVGif.gif)

[Full Gameplay Demo](https://www.youtube.com/watch?v=9JyZwx7B5Ws)

## Overview
Beat Virus is a rhythm based shooter game in virtual reality developed with MRTK in Unity. In Beat Virus, multi colored germ-like enemies spawn to the beat of the music and float towards the player. In order to survive and score points, the player must fight back with two different colored projectile weapons and hit the the enemy with the matching color. On top of that, one will build a combo multiplier if he or she shoots the enemies on beat.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BeatVirusThumbnail%20(2).png)

## Audio Visualization
My main goal with this was to explore audio visualization in Unity and how I could integrate it with mixed reality gameplay. In Beat Virus, almost every element of the world around the player is reacting to the music in some shape or form. To do this, I used Unity's built in Fast Fourier transform method which they call [GetSpectrumData](https://docs.unity3d.com/ScriptReference/AudioSource.GetSpectrumData.html).

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FFT.png)

I pass in a float array of size 512 which is filled up with the audio spectrum data in the current timestamp. In this case, the whole frequency spectrum gets split up in 512 parts, or samples, from low to high accordingly and higher numbers in the float array will represent bigger amplitudes for the frequency band of its index.
