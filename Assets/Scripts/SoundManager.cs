using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup bgmgroup;
    [SerializeField] AudioMixerGroup sfxgroup;
    public static SoundManager instance;
    public SoundAudioClip[] gameSounds;
    public SoundAudioClip[] randomPete;
    public AudioMixer audioMixer;

    string bgm = "BGM";
    string sfx = "SFX";

    private void Awake()
    {
        instance = this;
    }
    //private void Start()
    //{
    //    SetMixerVolume(bgm);
    //    SetMixerVolume(sfx);
    //}
    //private void SetMixerVolume(string mixerGroup)
    //{
    //    audioMixer.SetFloat(mixerGroup, Mathf.Log10(PlayerPrefs.GetFloat(mixerGroup, 0.25f)) * 20);
    //}

    private void Start()
    {
        if(0 == LevelManager.CurrentLevel())
            PlaySoundLoop(sound: Sounds.BGM);
    }
    public AudioSource PlaySound(Sounds sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.outputAudioMixerGroup = sfxgroup;
        audioSource.Play();
        Destroy(gameObject, audioSource.clip.length);
        return audioSource;
    }
    public void PlaySoundLoop(Sounds sound)
    {
        GameObject gameObject = new GameObject("LoopSound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
        audioSource.outputAudioMixerGroup = bgmgroup;
    }
    private AudioClip GetAudioClip(Sounds sound)
    {
        foreach (SoundAudioClip soundAudioClip in gameSounds)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
    public void PlaySoundRandom()
    {
        SoundAudioClip soundAudioClip = randomPete[UnityEngine.Random.Range(0, randomPete.Length)];

        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = (soundAudioClip.audioClip);
        audioSource.Play();
        Destroy(gameObject, audioSource.clip.length);
    }

    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void Unmute()
    {
        AudioListener.volume = 1;
    }
    public enum Sounds
    {
        CarDrive,
        CarReverse,
        CarCrash,
        PartPop,
        ButtonClick,
        BGM,
    }

    [Serializable]
    public class SoundAudioClip
    {
        public Sounds sound;
        public AudioClip audioClip;
    }
}