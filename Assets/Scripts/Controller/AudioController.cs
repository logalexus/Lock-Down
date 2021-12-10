using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private Sounds _sounds;

    public Sounds Sounds => _sounds;

    private AudioSource _musicSource1;
    private AudioSource _sfxSourceLoop;
    private AudioSource _sfxSource;
    private AudioSource _ambientSource;
    private float _pitchRatio = 0.2f;
    private float _durationFade = 1f;
    private int _click;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

    private void Start()
    {
        GameController.Instance.GameOver += () =>
        {
            StopAmbient();
            StopSFXLoop();
        };
        _musicSource1 = gameObject.AddComponent<AudioSource>();
        _sfxSourceLoop = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();
        _ambientSource = gameObject.AddComponent<AudioSource>();

        _musicSource1.volume = 0;
        _musicSource1.loop = true;
        _sfxSourceLoop.loop = true;
        _ambientSource.loop = true;
        //_ambientSource.clip = Sounds.AmbientFactory;
        _ambientSource.volume = 0;
        //PlayMusic(_sounds.MainTheme);
        StartAmbient();
    }

    public void PlayMusic(AudioClip music)
    {
        _musicSource1.clip = music;
        _musicSource1.Play();
        _musicSource1.DOFade(1, _durationFade).SetEase(Ease.InCubic);

    }

    public void StopMusic()
    {
        _musicSource1.DOFade(0, _durationFade).SetEase(Ease.InCubic).OnComplete(() => { _musicSource1.Stop(); });
    }

    public void PlaySFX(AudioClip sound)
    {
        _sfxSource.PlayOneShot(sound);
    }

    public void PlaySFXLoop(AudioClip sound)
    {
        _sfxSourceLoop.clip = sound;
        _sfxSourceLoop.Play();
    }

    public void StopSFXLoop()
    {
        _sfxSourceLoop.Stop();
    }

    public void SetPitchEngine(float verticalInput)
    {
        _ambientSource.pitch = 1 + verticalInput * _pitchRatio;
    }

    public void StartAmbient()
    {
        _ambientSource.Play();
        _ambientSource.DOFade(1, _durationFade).SetEase(Ease.InCubic);
    }

    public void StopAmbient()
    {
        _ambientSource.Stop();
        _ambientSource.volume = 0;
    }

    public void SetMusicActive(bool value)
    {
        _musicSource1.enabled = value;
    }

    public void SetSoundActive(bool value)
    {
        _sfxSource.enabled = value;
        _ambientSource.enabled = value;
    }
    
}
