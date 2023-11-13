using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //버튼 연결 변수

    public Button btnSetting;
    public Button btnExit;
    public Button btnGameStart;


    public GameObject pnlSetting;

    //셋팅 창 관련 변수
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;

    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;



    #region AddListner 연결
    private void Start()
    {
        //버튼 AddListner
        btnExit.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayButtonClickSound();
            UIManager.Instance.OnClickExit();
        });
        btnSetting.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayButtonClickSound();
            UIManager.Instance.ActivePnl(pnlSetting);
        });
       
        btnGameStart.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayGameStartSound();
            OnClickGameStart();
        });


        //사운드
        sldBGMVolume.onValueChanged.AddListener((value) =>
        {
            SoundMgr.Instance.SetBGMVolume(value);

        });

        sldSoundFxVolume.onValueChanged.AddListener((value) =>
        {
            SoundMgr.Instance.SetSoundFxVolume(value);

        });

        toggleBGMMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundMgr.Instance.MuteBGM(isMuted);

        });

        toggleSoundFxMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundMgr.Instance.MuteSoundFx(isMuted);

        });
    }

    #endregion

    #region TItle 씬 버튼 기능 함수
    

    public void OnClickGameStart()
    {
        SoundMgr.Instance.ChangeBGMForScene();//사운드 변경
        SceneManager.LoadScene("Game");
    }
    #endregion

    #region SETTING 창 기능 함수

    //볼륨 조절 함수
    public void SetSoundFxVolume(float volume)
    {

        SoundMgr.Instance.SetSoundFxVolume(volume);

        if (volume <= 0)
        {
            isSoundFxMuted = true;
            toggleSoundFxMute.isOn = true;
        }
        else
        {
            isSoundFxMuted = false;
            toggleSoundFxMute.isOn = false;
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("Setting BGM volume to: " + volume);
        SoundMgr.Instance.SetBGMVolume(volume);

        if (volume <= 0)
        {
            isBGMMuted = true;
            toggleBGMMute.isOn = true;
        }
        else
        {
            isBGMMuted = false;
            toggleBGMMute.isOn = false;
        }
    }

    //음소거 함수
    public void ToggleBGMMute(bool isMuted)
    {
        isBGMMuted = isMuted;

        // SoundManager의 음소거 기능 호출
        SoundMgr.Instance.MuteBGM(isBGMMuted);
    }

    public void ToggleSoundFxMute(bool isMuted)
    {
        isSoundFxMuted = isMuted;

        // SoundManager의 음소거 기능 호출
        SoundMgr.Instance.MuteSoundFx(isSoundFxMuted);

    }
    #endregion


    


}
