using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMatchMenu : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    
    public void UpdateMenu(int time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        
        timeText.text = $"{minutes.ToString("D2")}:{seconds.ToString("D2")}";
    }
}
