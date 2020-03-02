# Beat Virus
![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BVGif.gif)

[Full Gameplay Demo](https://www.youtube.com/watch?v=9JyZwx7B5Ws)
___
## Overview
Beat Virus is a rhythm based shooter game in virtual reality developed for a Windows Immersive Headset with MRTK in Unity. This game was made in a little under a week's time for a Mixed Reality XR game jam. 

In Beat Virus, multi colored germ-like enemies spawn to the beat of the music and float towards the player. In order to survive and score points, the player must fight back with two different colored projectile weapons and hit the the enemy with the matching color. On top of that, one will build a combo multiplier if he or she shoots the enemies on beat.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/BeatVirusThumbnail%203.png)
___
## Audio Visualization
My main goal with this was to explore audio visualization in Unity and how I could integrate it with mixed reality gameplay. In Beat Virus, almost every element of the world around the player is reacting to the music in some shape or form. To do this, I used Unity's built in Fast Fourier transform method which Unity calls [GetSpectrumData](https://docs.unity3d.com/ScriptReference/AudioSource.GetSpectrumData.html).

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FFT.png)

I pass in a float array of size 512 (samplesLeft and samplesRight in the above image) which is filled up with the audio spectrum data in the current timestamp. In this case, the whole frequency spectrum gets split up in 512 parts, or samples, from low to high accordingly. Higher numbers in the float array will represent bigger amplitudes for the frequency band of its index.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FFTTest.gif)

So, thanks to Unity, a lot of hard work is done already, however, 512 different freqeuency bands is a bit difficult to work with, and we want to use sample float arrays of about 512 or 1024 to get accurate representations of the audio from GetSpectrumData(). At this point, it's up to the programmer how he or she wants to shape the data for the application, but I decided to condense it into 8 frequency bands (low to high). With this setup, kick and bass sounds would generally be in the first or second frequency band, and it wasn't too difficult to isolate mids and highs either. I used this math to separate them.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FreqMath.png)

If my song is exported with a sample rate of 22050 and we're using a sample spectrum data array of 512, then each sample, or index of the array, will represent roughly 43hz on the frequency spectrum. So, the first two samples would represent 0hz-83hz, the next four samples (indexes 2-5) would represent 87hz-258hz, and so on. Using powers of 2 separated the frequency bands pretty nicely but ended up with a total of 510, so I added an extra two samples to the last frequency band. The C# code for this looks like this:

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/FreqFunc.png)

Looping from 0 to 7, I get the sample count I wanted from the math above by taking the current index of the loop squared multiplied by 2. Then, I use that sample count to loop through the number of samples, or float array indexes, belonging to that frequency band and average them. Finally, I scale the average up by 10 as the number was a bit low to work with. Lastly, to make the frequency bands a little more Unity friendly, I created a matching-sized AudioBand array which represents the frequency bands as a number between 0 and 1 by dividing by the current max for that frequency band.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/AudioBands.png)

After doing these calculations, I'm able to multiply the frequency band, audio band, or average of the entire audio band array (which gives me overall amplitude) with any parameter to get cool audio visualization effects. The results are seen in the game with the tunnel warping to the kick drums, the tunnel's colors shifting to the beat, and the enemies bouncing around to different frequency bands.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/Tunnel.gif)
![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/Ball.gif)
___
## Gameplay
At this stage of development, the gameplay code is fairly simple. The player is placed in the center of the tunnel where germ-like spheres of different colors spawn in 4 different lanes about 20 meters away and float towards the player. The player points at the spheres with hands or controllers, also of different colors, and clicks/taps on spheres of matching colors to explode them.  

Enemy sphere spawning is handled with an object pool, so I only create a handful at the beginning of play and destroy at the end of a session. After every two beats (quarter notes) of the song, my [RhythmGenerator](https://github.com/dylanmpeck/Beat-Virus/blob/master/Assets/Scripts/RhythmGenerator.cs) class makes a decision of what to spawn. Currently its spawn choices are nothing, one random colored ball placed in one of four lanes, two balls of both colors placed in two separate lanes, or a different sphere type which the player must drag. The weights of what the spawner is most likely to choose are affected by the amplitude of the music. On lower amplitudes, it's more likely to throw nothing while on higher amplitudes its more likely to spawn more spheres.  

The explosion effect is done with a prefab of minispheres that in total match the size of the original, big sphere. When the sphere is shot, the minispheres become active, an explosion force is applied to them, they reset to original position after a certain amount of time, and then they are deactivated again.

The bulk of the controls/XR functionality is done with Mixed Reality Toolkit and Open XR components. While developing this game, I had the opportunity to try it out on a HoloLens 2, so I wanted all the code and controls to function on both a Windows Immersive VR Headset and the HoloLens 2. So, in most cases, I use MRTK's [pointer interface](https://microsoft.github.io/MixedRealityToolkit-Unity/api/Microsoft.MixedReality.Toolkit.Input.IMixedRealityPointerHandler.html) and [manipulation handler](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/README_ManipulationHandler.html) component because they automatically worked well enough with both devices. But, when I was scripting my own logic, I would check for hands using Microsoft's [HandJointUtils](https://microsoft.github.io/MixedRealityToolkit-Unity/api/Microsoft.MixedReality.Toolkit.Input.HandJointUtils.html) first and then use controllers from the proper [XR Node](https://docs.unity3d.com/ScriptReference/XR.XRNode.html). Here's some sample code of me spawning projectile particles when you shoot in the correct hand or controller.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/HandsAndControllers.png)

The score system is calculated based off the bpm of the song playing. The player will get a percentage of how many points the enemy sphere is worth based off how precise he or she shot it to the beat. If the player clicked close enough to the beat within a certain error margin, a combo multiplier to the previous points will rise up to x8. Missing a beat causes the combo to drop.

![alt-text](https://github.com/dylanmpeck/Beat-Virus/blob/master/ReadmeImages/Score.png)
___
## TODOS
This game could go in a few different directions. At the moment, my idea is to integrate spotify, use the Spotify API's deeper music analysis tools, and convert the game into an arcade shooter. Or, go down a music generative path similar to Rez where the player would be generating musical sounds through his or her actions.

Either way, the XR facets of the game could be improved with punching, grabbing, and dodging mechanics. More interesting enemy types like a red and blue sphere that needs to be combined into a purple one so it can be shot would be fun. Also, an upgrade system with collectible currency would be great in this.
