using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [HideInInspector] public float count;
    
    [SerializeField] TMP_Text timerObject;

    void Update()
    {
        count += Time.deltaTime;
        
        int minutes = (int)(count / 60);
        int seconds = (int)(count % 60);
        
        timerObject.text = $"{minutes.ToString("D2")}:{seconds.ToString("D2")}";
    }
}
