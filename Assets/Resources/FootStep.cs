using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFoot;
    private void Start()
    {
        isFoot = true;
    }
    public void footstep1()
    {
        if (isFoot)
        {
            SoundMgr.Instance.footstep();
        }
        else
            return;
    }
    public void footstep2()
    {
        if (isFoot)
        {
            SoundMgr.Instance.footstep();
        }
        else
            return;
    }
    public void SetIsFoot(bool value)
    {
        isFoot = value;
    }

}
