using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boost : MonoBehaviour
{
    public class BoostEvent : UnityEvent { } 
    public BoostEvent onBoost;
    // Start is called before the first frame update
    public void BoostSpeed()
    {       
         onBoost.Invoke();       
    }
}
