using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float timer;
    
    [SerializeField] GameObject[] balloonPrefabs;
    
    [SerializeField] GameObject player;
    
    [SerializeField] int minDistance, maxDistance;
    
    float count;
    
    [SerializeField] int enemiesToKill;
    
    int enemiesKilled;
    
    [SerializeField] GameManager gameManager;
    
    [SerializeField] TMP_Text remainingBalloonsText;

    void Start()
    {
        count = enemiesKilled = 0;
        UpdateBalloonsRemaining();
    }
    
    void Update()
    {
        count += Time.deltaTime;
        
        if (count >= timer)
        {
            int concurrentEnemies = Random.Range(1, 3);
            for(int i = 0; i < concurrentEnemies; i++)
                SpawnEnemy();
            count = 0;
        }
        
        if (enemiesKilled >= enemiesToKill)
        {
            gameManager.EndMatch();
        }
    }

    private void SpawnEnemy()
    {
        // Generate a random distance within the specified range.
        float randomDistance = Random.Range(minDistance, maxDistance);

        // Generate a random angle in radians (0 to 2*pi).
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        // Calculate the random position
        //Position has a varied height, with the area being a circle around the player with min and max distance
        Vector3 randomOffset = new Vector3(
            Mathf.Cos(randomAngle) * randomDistance,
            Random.Range(-7f, 7f),
            Mathf.Sin(randomAngle) * randomDistance
        );

        // Calculate the final position by adding the offset to the player's position.
        Vector3 randomPosition = player.transform.position + randomOffset;
        
        //Choose a random balloon type to spawn at the randon position that was created above
        int rand = Random.Range(0,3);
        GameObject balloon = Instantiate(balloonPrefabs[rand], randomPosition, Quaternion.identity);
        balloon.transform.parent = gameObject.transform;
        
        //Rotate ballon to face the player
        Vector3 lookAtPosition = new Vector3(player.transform.position.x, balloon.transform.position.y, player.transform.position.z);
        balloon.transform.LookAt(lookAtPosition);
        
        //Subscribe to the balloon death even so you can count when they get destroyed
        balloon.GetComponent<Balloon>().DeathEvent += EnemyDeathEvent;       
    }

    private void EnemyDeathEvent()
    {
        enemiesKilled++;
        UpdateBalloonsRemaining();
    }
    
    private void UpdateBalloonsRemaining()
    {
        remainingBalloonsText.text = $"REMAINING: {enemiesToKill - enemiesKilled}";
    }
}
