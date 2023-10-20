using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform mainCam, mainCamTarget;    

    [SerializeField] float speed = 10f, updateTimer;
    
    [SerializeField] Image minimapImage;
    [SerializeField] Image blipPrefab;
    
    [SerializeField] GameObject minimap, shootButton, timerHud;
    [SerializeField] Timer timer;
    [SerializeField] EndMatchMenu endMatchMenu;
    
    bool matchEnded = false;
    
    void Start()
    {
        StartCoroutine(RadarUpdate());
    }
    
    void Update()
    {
        //For testing on PC
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     Shoot();
        // }
    }

    IEnumerator DestroyCurrentBlips()
    {
        for(int i = 0; i < minimapImage.transform.childCount; i++)
        {
            Destroy(minimapImage.transform.GetChild(i).gameObject);
        }
        
        yield return new WaitForSeconds(updateTimer);
        
        StartCoroutine(RadarUpdate());
    }
    
    IEnumerator RadarUpdate()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            //Big brain math to convert real world relative position of balloons into minimap position
            Vector3 balloonPos = gameObject.transform.GetChild(i).position;
            balloonPos.y = mainCam.position.y;
            
            Vector3 relativePos = (balloonPos - gameObject.transform.position).normalized;
            
            float dotProduct = Vector3.Dot(mainCam.forward, relativePos);
            float angleInRadian = Mathf.Acos(dotProduct);
            float angleInDegrees = angleInRadian * Mathf.Rad2Deg;   
            
            if(balloonPos.x < gameObject.transform.position.x) angleInDegrees*=-1; 
            
            float newRad = (90f - angleInDegrees) * Mathf.Deg2Rad;
            
            float x = 50 * Mathf.Cos(newRad);
            float y = 50 * Mathf.Sin(newRad);        
            
            //Create the minimap indication for where the balloon is
            Image blip = Instantiate(blipPrefab, minimapImage.rectTransform);
            blip.rectTransform.anchoredPosition = new Vector2(x,y);
        }
        
        yield return new WaitForSeconds(1f);
        //"Radar" effect of showing the position just for a short while
        StartCoroutine(DestroyCurrentBlips());
    }

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.forward = mainCam.transform.forward;
        newProjectile.transform.position = mainCamTarget.position;
        newProjectile.transform.GetComponent<Rigidbody>().velocity = (mainCam.transform.forward) * speed;
    }
    public void EndMatch()
    {
        if(matchEnded)
            return;
            
        Time.timeScale = 0;
        DisableHUD();
        UpdateEndMatchMenu();
        endMatchMenu.gameObject.SetActive(true);
        matchEnded = true;
    }

    private void DisableHUD()
    {
        timerHud.SetActive(false);
        minimap.SetActive(false);
        shootButton.SetActive(false);
    }

    private void UpdateEndMatchMenu()
    {
        endMatchMenu.UpdateMenu((int)timer.count);
    } 
    
    public void ResetMatch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
