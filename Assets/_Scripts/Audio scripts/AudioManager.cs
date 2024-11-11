using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
        public static AudioManager audioManager;


        public AudioSource musicSource, sfxSource;

        public float MusicVolume, SfxVolume;

        [SerializeField] private float _TimeBetweenSameSounds;



    private List<AudioClip> _lastSounds=new List<AudioClip>();
    private float initVolumeMusic, initVolumeSfx;

    private AudioClip currentTrack;

    private bool coroutineEnd=true;


    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;

            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        initVolumeMusic = musicSource.volume;
        initVolumeSfx = sfxSource.volume;
        ChangeVolumeMusic(PlayerPrefs.GetFloat("music volume", 0.5f));

        ChangeVolumeSfx(PlayerPrefs.GetFloat("sfx volume", 0.5f));

      

    }

  
    public void changeVolumeMusic(float volumeChange)
    {
        musicSource.volume += volumeChange;
    }
        public void ChangeMusic(AudioClip clip)
        {
        StartCoroutine(ChangingMusicIe(clip));
        }
    private IEnumerator ChangingMusicIe(AudioClip music)
    {
        yield return new WaitUntil(()=>coroutineEnd == true);
        if (currentTrack == music) yield break;
        coroutineEnd = false;
        float volume = musicSource.volume;
        currentTrack = music;
        while (musicSource.volume > 0)
        {
           changeVolumeMusic(-0.05f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;
        while (musicSource.volume < volume)
        {
            changeVolumeMusic(0.05f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        musicSource.volume = volume;

        coroutineEnd = true;
    }
    public void PlaySound(AudioClip clip)
        {
            if (!_lastSounds.Contains(clip))
            {
                sfxSource.PlayOneShot(clip);
            _lastSounds.Add(clip);
            StartCoroutine(SoundPlayingCoolDown(clip));
            }
        }
    public void PlaySoundCoolDown(AudioClip clip, float timeBetweenSoundAdd)
    {
        if (!_lastSounds.Contains(clip))
        {
            sfxSource.PlayOneShot(clip);
            _lastSounds.Add(clip);
          
            StartCoroutine(SoundPlayingCoolDown(clip, timeBetweenSoundAdd));
        }
    }
    private IEnumerator SoundPlayingCoolDown(AudioClip clip)
        {
       
            yield return new WaitForSecondsRealtime(_TimeBetweenSameSounds);
        if (_lastSounds.Contains(clip))
        _lastSounds.Remove(clip);
    }
    private IEnumerator SoundPlayingCoolDown(AudioClip clip,float addedTime)
    {
        yield return new WaitForSecondsRealtime(_TimeBetweenSameSounds+addedTime);
        if (_lastSounds.Contains(clip))
            _lastSounds.Remove(clip);
    }
    public void PlaySound(AudioClip clip, float VolumeMulti)
        {

            if (!_lastSounds.Contains(clip))
            {
                sfxSource.PlayOneShot(clip, VolumeMulti * SfxVolume);
            _lastSounds.Add(clip);
            StartCoroutine(SoundPlayingCoolDown(clip));
            }
        }
        public void PlaySoundDelay(AudioClip clip, float delay)
        {

            StartCoroutine(DelaySound(clip, delay));
        }
        private IEnumerator DelaySound(AudioClip clip, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (_lastSounds.Contains(clip))
            {
                sfxSource.PlayOneShot(clip);
            _lastSounds.Add(clip);
            StartCoroutine(SoundPlayingCoolDown(clip));
            }
        }
        public void ChangeVolumeSfx(float volume)
        {
            SfxVolume = volume;

            sfxSource.volume = SfxVolume*initVolumeSfx;
        }

        public void ChangeVolumeMusic(float volume)
        {
            MusicVolume = volume;

            musicSource.volume = MusicVolume*initVolumeMusic;
        }

        public void SaveVolume()
        {
            PlayerPrefs.SetFloat("music volume", MusicVolume);

            PlayerPrefs.SetFloat("sfx volume", SfxVolume);

            PlayerPrefs.Save();
        }
    }


