using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to put on a GameObject to make it spawn ennemies
public class MobSpawnerScript : MonoBehaviour
{
    //GameObject to spawn
    public GameObject objectToSpawn;

    public float detectionDistance;

    public float timeBetweenSpawn;
    private float spawnCounter;

    private GameObject player;

    public string enemyType;




    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeBetweenSpawn;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        //This condition is here to time the spawn and make it spawn when the player is close enough
        if(spawnCounter <= 0 && Vector3.Distance(transform.position,player.transform.position) <= detectionDistance)
        {
            spawnCounter = timeBetweenSpawn;
            //Instantiate the enemy
            GameObject enemy = Instantiate(objectToSpawn, transform.position, transform.rotation);

            //Set HP and DMG for different types of enemies
            if (enemyType == "zombie")
                enemy.GetComponent<Ennemy>().setHPandDMG(StaticDifficultyManager.ZOMBIEHP, StaticDifficultyManager.ZOMBIEDMG);
            else if(enemyType == "skel")
                enemy.GetComponent<Ennemy>().setHPandDMG(StaticDifficultyManager.SKELETONHP, StaticDifficultyManager.SKELETONDMG);
            else
            {
                Debug.Log("error : wrong enemy type on mob spawner");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
