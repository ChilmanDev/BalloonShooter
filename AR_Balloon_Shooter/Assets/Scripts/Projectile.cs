using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float timeToDie = 7f;
    float count = 0f;

    void Update()
    {
        count += Time.deltaTime;

        if (count >= timeToDie)
            Destroy(transform.gameObject);
    }
    
    private void OnCollisionEnter(Collision other) {
        Destroy(transform.gameObject);
    }
}
