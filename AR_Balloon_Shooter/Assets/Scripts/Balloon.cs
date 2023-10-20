using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeathEventHandler();

public class Balloon : MonoBehaviour
{
    [SerializeField] int hp;
    
    public event DeathEventHandler DeathEvent;

    void Update()
    {
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
