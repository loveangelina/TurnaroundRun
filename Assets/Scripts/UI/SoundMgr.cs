using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public AudioSource bgmSource;
    
    public AudioSource sfxSource;

    public AudioClip gameStartClip;
    public AudioClip buttonClickClip;

    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private AudioSource countDownSorce;
    public AudioClip[] CountDown;
    public AudioClip[] sceneBGMs;
    public AudioClip boostSoundClip; // 부스트 효과음 클립
    private static SoundMgr _instance;

    public static SoundMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundMgr>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<SoundMgr>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        LoadAudioSettings();
    }

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(value =>
        {
            SetBGMVolume(value);
            SaveAudioSettings();
        });

        sfxSlider.onValueChanged.AddListener(value =>
        {
            SetSoundFxVolume(value);
            SaveAudioSettings();
        });

        bgmToggle.onValueChanged.AddListener(isMuted =>
        {
            MuteBGM(isMuted);
            SaveAudioSettings();
        });

        sfxToggle.onValueChanged.AddListener(isMuted =>
        {
            MuteSoundFx(isMuted);
            SaveAudioSettings();
        });
    }


    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSoundFxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteBGM(bool mute)
    {
        bgmSource.mute = mute;
    }

    public void MuteSoundFx(bool mute)
    {
        sfxSource.mute = mute;
    }

    public void PlayGameStartSound()
    {
        sfxSource.PlayOneShot(gameStartClip);
    }

    public void PlayButtonClickSound()
    {
        sfxSource.PlayOneShot(buttonClickClip);
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("BGM_VOLUME", bgmSlider.value);
        PlayerPrefs.SetFloat("SFX_VOLUME", sfxSlider.value);
        PlayerPrefs.SetInt("BGM_MUTE", bgmToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SFX_MUTE", sfxToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {


        if (PlayerPrefs.HasKey("BGM_VOLUME"))
        {
            float loadedBGMVolume = PlayerPrefs.GetFloat("BGM_VOLUME");
            bgmSlider.value = loadedBGMVolume;
            bgmSource.volume = loadedBGMVolume;

        }

        if (PlayerPrefs.HasKey("SFX_VOLUME"))
        {
            float loadedSFXVolume = PlayerPrefs.GetFloat("SFX_VOLUME");
            sfxSlider.value = loadedSFXVolume;
            sfxSource.volume = loadedSFXVolume;

        }

        if (PlayerPrefs.HasKey("BGM_MUTE"))
        {
            bool loadedBGMMute = PlayerPrefs.GetInt("BGM_MUTE") == 1;
            bgmToggle.isOn = loadedBGMMute;
            bgmSource.mute = loadedBGMMute;

        }

        if (PlayerPrefs.HasKey("SFX_MUTE"))
        {
            bool loadedSFXMute = PlayerPrefs.GetInt("SFX_MUTE") == 1;
            sfxToggle.isOn = loadedSFXMute;
            sfxSource.mute = loadedSFXMute;

        }
        if (!PlayerPrefs.HasKey("ISSAVE"))
        {
            bgmSlider.value = 1.0f;
            sfxSlider.value = 1.0f;
            bgmToggle.isOn = false;
            sfxToggle.isOn = false;

            SaveAudioSettings();
            PlayerPrefs.SetInt("ISSAVE", 1);
        }
    }
    public void PlayBoostSound()
    {
        sfxSource.PlayOneShot(boostSoundClip);
    }
    public void StopBoostSound()
    {
        // 부스트 효과음 중지
        sfxSource.Stop();
    }
    public void ChangeBGMForScene()
    {
        int randomSceneIndex = Random.Range(0, sceneBGMs.Length);

        AudioClip newBgmClip = sceneBGMs[randomSceneIndex];

        bgmSource.clip = newBgmClip;
        bgmSource.loop = true;
        bgmSource.Play();        
        StartCoroutine(volumeUP());
    }
    public void countDown()
    {
        countDownSorce = gameObject.AddComponent<AudioSource>();
        countDownSorce.clip = CountDown[0];
        countDownSorce.volume = 0.5f;
        countDownSorce.Play();
        StartCoroutine(DestroyCountDown());
    }
    IEnumerator volumeUP()
    {
        bgmSource.volume = 0.6f;
        yield return new WaitForSeconds(3f);
        bgmSource.volume = 1f;
    }
    IEnumerator DestroyCountDown()
    {       
        yield return new WaitForSeconds(3.5f);
        countDownSorce.Stop();
    }
    public void ComeIn()
    {
        countDownSorce.clip = CountDown[1];
        countDownSorce.Play();
    }
    public IEnumerator Nagative()
    {
        yield return new WaitForSeconds(2f);
        countDownSorce.clip = CountDown[2];
        countDownSorce.Play();
    }
}
