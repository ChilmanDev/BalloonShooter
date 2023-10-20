using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DeathEventHandler();

public class Balloon : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] Slider hpSlider;
    
    public event DeathEventHandler DeathEvent;
    
    private void Start() {
        hpSlider.maxValue = hp;
    }

    void Update()
    {
        hpSlider.value = hp;
        
        if(hp <= 0)
            End();
    }

    private void End()
    {
        DeathEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        HandleHit();
    }

    private void HandleHit()
    {
        hp--;
    }
}
