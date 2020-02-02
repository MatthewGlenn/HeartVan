using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioMixer mixer;
    //public GameManager gm;

    public float fadeTimeDefault;

    public Sound[] MusicSounds;
    public Sound[] SFXSounds;
    public static AudioManager instance;

    public string currMusicName;

    enum SoundType { FX, Music, Both };

    private int currTrackNumber;
    private Sound currentMusicSound;
    private Sound nextMusicSound;

    private void Awake()
    {
        //Ensure that only one instance of this class is created (we don't want a new AudioManager everytime a scene loads)
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        currTrackNumber = 1;
        currentMusicSound = GetSound("track1");
        MakeSource((currentMusicSound));
        nextMusicSound = GetSound(("track2"));
        MakeSource((nextMusicSound));
        nextMusicSound.source.volume = 0;

        currentMusicSound.source.Play();
        nextMusicSound.source.Play();
    }

    public void Success()
    {
        PlayFX("success");
        PlayNextTrack();
    }

    public void Failure()
    {
        
    }

    private void InitializeMusicSounds()
    {
   
    }

    private IEnumerator PlayNextTrack()
    {
        Debug.LogWarning("playing next track");
        if (nextMusicSound == null) { yield break;} 
        if (!nextMusicSound.source.isPlaying){}
        //Debug.LogWarning("nextS: " + nextMusicSound.name);

        //yield return new WaitForSeconds(currentS.source.clip.samples - currentS.source.timeSamples);

        nextMusicSound.source.time = currentMusicSound.source.time;
        //subtracting extra to try to start transition a bit early
        yield return new WaitForSeconds(currentMusicSound.clip.length - currentMusicSound.source.time - (fadeTimeDefault/2));

        FindObjectOfType<GameManager>().resetLoop();
        
        StartCoroutine(FadeIn(nextMusicSound.source, fadeTimeDefault, nextMusicSound.volume));
        StartCoroutine(FadeOut(currentMusicSound.source, fadeTimeDefault));

        currTrackNumber = currTrackNumber + 1;

        yield return new WaitForSeconds(2);
        
        //unload old audio
        UnloadAudio(currentMusicSound);

        currentMusicSound = nextMusicSound;

        nextMusicSound = GetSound("track" + (currTrackNumber + 1)); 
        MakeSource(nextMusicSound);
        nextMusicSound.source.volume = 0;

        Debug.Log("current track number: "+currTrackNumber);
        Debug.Log("track" + (currTrackNumber + 1));
        if (nextMusicSound != null)
        {
            PlayMusic(nextMusicSound, currentMusicSound.source.time);
        }
    }

    public bool IsMusicPlaying()
    {
        foreach (Sound s in MusicSounds)
            if (s.source != null)
            {
                if (s.source.isPlaying) { return true; }
            }
        return false;
    }

    public void SetMasterVolume(float vol)
    {
        mixer.SetFloat("MasterVolume", vol);
    }
    public void SetMusicVolume(float vol)
    {
        mixer.SetFloat("MusicVolume", vol);
    }
    public void SetFxVolume(float vol)
    {
        mixer.SetFloat("SFXVolume", vol);
    }

    public void SetSourceOutput(string soundName, string groupName)
    {
        AudioMixerGroup[] aga = mixer.FindMatchingGroups(groupName);
        if (aga != null)
        {
            AudioMixerGroup ag = Array.Find(aga, group => group.name == groupName);
            if (ag != null)
            {
                GetSource(GetSound(soundName)).outputAudioMixerGroup = ag;
                return;
            }
        }
        Debug.LogWarning("could not find AudioMixerGroup name: " + groupName);

        return;
    }

    /*public void SceneTransition(string nextSceneName)
    {
        Sound nextS = GetSound(nextSceneName, SoundType.Music);
        Sound currentS = GetSound(currMusicName, SoundType.Music);

        if (nextS.clip == currentS.clip)
        {
            return;
        }

        AudioSource nextSource = GetSource(nextS);

        StartCoroutine(FadeIn(nextSource, fadeTimeDefault, nextS.volume));
        StartCoroutine(FadeOutAndUnload(currentS, fadeTimeDefault));

        currMusicName = nextSceneName;

        return;
    }*/

    public void SceneEnd()
    {
        foreach (Sound s in SFXSounds)
        {
            if (s.source != null)
            {
                if (s.source.isPlaying)
                {
                    StartCoroutine(FadeOut(s.source, fadeTimeDefault));
                }
            }
        }
        return;
    }

    public void LoadMusic(string sceneName)
    {
        Sound sound = GetSound(sceneName);
        sound.clip.LoadAudioData();
    }

    public void PlayFX(string s)
    {
        Sound currentS = GetSound(s, SoundType.FX);
        if (currentS != null) { fxSource.PlayOneShot(currentS.clip, currentS.volume); }
    }

    /// <summary>
    /// Play sound effect at random pitch
    /// </summary>
    /// <param name="s">Name of sound</param>
    /// <param name="randomPitch">Total range of randomness. ex) 0.2f = +-0.1f in either direction</param>
    public void PlayFX(string s, float randomPitch)
    {
        Sound currentS = GetSound(s, SoundType.FX);
        if (currentS.source == null) { MakeSource(currentS); }
        currentS.source.pitch = currentS.pitch * (1 + UnityEngine.Random.Range(-randomPitch / 2f, randomPitch / 2f));
        currentS.source.Play();
    }

    public void Play(string s)
    {
        Sound currentS = GetSound(s);
        if (currentS.source == null) { MakeSource(currentS); }
        currentS.source.Play();
        Debug.Log("Playing audio: " + s);
    }

    public void PlayMusic(string s)
    {
        Sound currentS = GetSound(s, SoundType.Music);
        if (currentS.source == null) { MakeSource(currentS); }
        currentS.source.Play();
        Debug.Log("Playing music: " + s);
    }

    private void PlayMusic(Sound s, float playTime)
    {
        if (s.source == null) { MakeSource(s); }
        s.source.time = playTime;
        s.source.Play();
        Debug.Log("Playing music: " + s);
    }

    public void Stop(string name)
    {
        Sound sound = GetSound(name);
        if (sound.source == null) { return; }
        sound.source.Stop();
    }

    public void FadeInSFX(string name)
    {
        Sound s = GetSound(name);
        StartCoroutine(FadeIn(GetSource(s), fadeTimeDefault, s.volume));
    }

    public void FadeOutSFX(string name)
    {

        Sound s = GetSound(name);
        if (s.source == null)
        {
            return;
        }
        StartCoroutine(FadeOut(s.source, fadeTimeDefault));
    }

    public void FadeOutMusic(string soundName, float fadeTime)
    {
        Sound s = GetSound(soundName, SoundType.Music);
        if (s.source == null)
        {
            return;
        }
        StartCoroutine(FadeOutAndUnload(s, fadeTime));
    }

    private void MakeSource(Sound s)
    {
        if (s.source == null)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.output;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playonawake;
            s.source.ignoreListenerPause = s.ignoreListenerPause;
        }
        return;
    }

    private AudioSource GetSource(Sound s)
    {
        if (s.source == null) { MakeSource(s); }
        return s.source;
    }

    private Sound GetSound(string str, SoundType type = SoundType.Both)
    {
        Sound sound = null;
        if (type == SoundType.FX || type == SoundType.Both)
        {
            sound = Array.Find(SFXSounds, s => s.name == str);
        }
        if (sound == null && (type == SoundType.Music || type == SoundType.Both))
        {
            sound = Array.Find(MusicSounds, s => s.name == str);
        }
        if (sound == null)
        {
            Debug.LogWarning("Sound name not found in " + type.ToString() + " array: " + str);
            return null;
        }
        return sound;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            if (FadeTime == 0f) { audioSource.volume = 0; }
            else{ audioSource.volume -= startVolume * Time.deltaTime / FadeTime; }
            yield return null;
        }
        audioSource.Stop();
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float maxVol = 1f)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        audioSource.volume = 0f;
        while (audioSource.volume < maxVol)
        {
            if (FadeTime == 0f) { audioSource.volume = maxVol; }
            else{ audioSource.volume += Time.deltaTime / FadeTime; }
            yield return null;
        }
    }

    private void UnloadAudio(Sound s)
    {
        if (s.source.isPlaying)
        {
            s.source.Stop();
        }
        s.source.Stop();
        s.source.clip.UnloadAudioData();
        s.clip.UnloadAudioData();
        Destroy(s.source);
    }

    public IEnumerator FadeOutAndUnload(Sound sound, float FadeTime)
    {
        float startVolume = sound.source.volume;
        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * (Time.deltaTime / FadeTime);
            yield return null;
        }
        sound.source.Stop();
        sound.source.clip.UnloadAudioData();
        sound.clip.UnloadAudioData();
        Destroy(sound.source);
    }

    public IEnumerator FadeOutBlip(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
