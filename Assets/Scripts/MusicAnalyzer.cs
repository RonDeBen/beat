using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MusicAnalyzer : MonoBehaviour {

    public class SoundWave{
        public AudioSource source;
        public float rms;
        public float db;
        public float pitch;
        public float minPitch;
        public float maxPitch; 

        public float deltaPitch(){
            return maxPitch - minPitch;
        }

        public void resetDeltaPitch(){
            minPitch = 0;
            maxPitch = 0;
        }
    }

    public AudioClip square1Clip, square2Clip, triangleClip, noiseClip;
    private SoundWave square1, square2, triangle, noise;
    [HideInInspector]
    public static AudioSource square1Music, square2Music, triangleMusic, noiseMusic;

     int qSamples = 1024;  // array size
     float refValue = 0.1f; // RMS value for 0 dB
     float threshold = 0.02f;      // minimum amplitude to extract pitch
     float rmsValue;   // sound level - RMS
     float dbValue;    // sound level - dB
     float pitchValue; // sound pitch - Hz
 
     private float[] samples; // audio samples
     private float[] spectrum; // audio spectrum
     private float fSample;

     private float deltaVariance = 50f;
 
     void  Start (){
         samples = new float[qSamples];
         spectrum = new float[qSamples];
         fSample = AudioSettings.outputSampleRate;

         square1 = new SoundWave();
         square2 = new SoundWave();
         triangle = new SoundWave();
         noise = new SoundWave();

         square1Music = gameObject.AddComponent<AudioSource>() as AudioSource;
         square1Music.clip = square1Clip;
         square2Music = gameObject.AddComponent<AudioSource>() as AudioSource;
         square2Music.clip = square2Clip;
         triangleMusic = gameObject.AddComponent<AudioSource>() as AudioSource;
         triangleMusic.clip = triangleClip;
         noiseMusic = gameObject.AddComponent<AudioSource>() as AudioSource;
         noiseMusic.clip = noiseClip;

         square1.source = gameObject.AddComponent<AudioSource>() as AudioSource;
         square1.source.clip = square1Clip;
         square1.source.mute = true;
         square2.source = gameObject.AddComponent<AudioSource>() as AudioSource;
         square2.source.clip = square2Clip;
         square2.source.mute = true;
         triangle.source = gameObject.AddComponent<AudioSource>() as AudioSource;
         triangle.source.clip = triangleClip;
         triangle.source.mute = true;
         noise.source = gameObject.AddComponent<AudioSource>() as AudioSource;
         noise.source.clip = noiseClip;
         noise.source.mute = true;

         square1Music.Play();
         square2Music.Play();
         triangleMusic.Play();
         noiseMusic.Play();
     }
     
     void  AnalyzeSound (SoundWave wave){
         wave.source.GetOutputData(samples, 0); // fill array with samples
         int i;
         float sum = 0;
         for (i=0; i < qSamples; i++){
             sum += samples[i]*samples[i]; // sum squared samples
         }
         wave.rms = Mathf.Sqrt(sum/qSamples); // rms = square root of average
         wave.db = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
         if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
         // get sound spectrum
         wave.source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
         float maxV = 0;
         int maxN = 0;
         for (i=0; i < qSamples; i++){ // find max 
             if (spectrum[i] > maxV && spectrum[i] > threshold){
                 maxV = spectrum[i];
                 maxN = i; // maxN is the index of max
             }
         }
         float freqN = maxN; // pass the index to a float variable
         if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours
             float dL= spectrum[maxN-1]/spectrum[maxN];
             float dR= spectrum[maxN+1]/spectrum[maxN];
             freqN += 0.5f*(dR*dR - dL*dL);
         }
         wave.pitch = freqN*(fSample/2)/qSamples; // convert index to frequency
         MinMaxPitch(wave);
     }

     public void StartFakeMusic(){
        square1.source.Play();
        square2.source.Play();
        triangle.source.Play();
        noise.source.Play();
     }

     public static void MuteChannel(string channel){
        switch(channel){
            case "first":
                square1Music.mute = true;
                break;
            case "second":
                square2Music.mute = true;
                break;
            case "third":
                triangleMusic.mute = true;
                break;
            case "fourth":
                noiseMusic.mute = true;
                break;
        }
     }

     public static void UnmuteChannel(string channel){
        switch(channel){
            case "first":
                square1Music.mute = false;
                break;
            case "second":
                square2Music.mute = false;
                break;
            case "third":
                triangleMusic.mute = false;
                break;
            case "fourth":
                noiseMusic.mute = false;
                break;
        }
     }

     public void MinMaxPitch(SoundWave wave){
        float newPitch = wave.pitch;
        if(newPitch < wave.minPitch){
            wave.minPitch = newPitch;
        }else if(newPitch > wave.maxPitch){
            wave.maxPitch = newPitch;
        }
     }

     public void AnalyzeAllChannels(){
        AnalyzeSound(square1);
        AnalyzeSound(square2);
        AnalyzeSound(triangle);
        AnalyzeSound(noise);
     }

     public void ResetAllPitches(){
        square1.resetDeltaPitch();
        square2.resetDeltaPitch();
        triangle.resetDeltaPitch();
        noise.resetDeltaPitch();
     }

     public List<int> DeltaWavePossibilities(){
        List<int> possibilities = new List<int>();

        if(square1.deltaPitch() > deltaVariance && !ChannelManager.IsThisChannelInfected("first")){
            possibilities.Add(0);
        }
        if(square2.deltaPitch() > deltaVariance && !ChannelManager.IsThisChannelInfected("second")){
            possibilities.Add(1);
        }
        if(triangle.deltaPitch() > deltaVariance && !ChannelManager.IsThisChannelInfected("third")){
            possibilities.Add(2);
        }
        if(noise.deltaPitch() > deltaVariance && !ChannelManager.IsThisChannelInfected("fourth")){
            possibilities.Add(3);
        }

        ResetAllPitches();
        return possibilities;
     }
     
     void  Update (){
         AnalyzeAllChannels();   
     }

}
