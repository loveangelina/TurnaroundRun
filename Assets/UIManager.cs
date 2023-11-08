using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 싱글턴 인스턴스
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<UIManager>();
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
    }
    #endregion
    #region 변수들


    //인원 선택 변수
    public int value = 2;

    //패널 on off 확인 변수
    public bool isOpenPnl = false;



    #endregion
    #region 싱글턴 활용 함수들

    public void ActivePnl(GameObject pnl)
    {
        isOpenPnl = !pnl.activeSelf;
        pnl.SetActive(isOpenPnl);

    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    #endregion
}
