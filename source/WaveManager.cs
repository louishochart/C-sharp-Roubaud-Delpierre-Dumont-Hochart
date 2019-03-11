using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Script that manage the waves (amount of zombies, spawn speed, wave number)
public class WaveManager : MonoBehaviour
{
    //Gives the current wave
    private int waveNumber = 0;

    public Transform[] spawnerArray;
    public GameObject zombiePrefab;

    public float timeBetweenSpawns = 2f;

    public float nofZombieMultiplier = 1.6f;
    public float nofPointsMultiplier = 1.3f;

    public float points = 1;
    public int numberOfZombies = 5;

    private bool isWaveRunning;
    private int numberOfZombiesCounter;

    private AudioSource _audiosource;

    public AudioClip waveStartSound;

    public GameObject UIText;

    private void Start()
    {
        Cursor.visible = false;      
        UIText.SetActive(false);
        _audiosource = GetComponent<AudioSource>();
        nextWave();
    }

    private void Update()
    {
        if (isWaveRunning)
        {
            if(GameObject.FindGameObjectWithTag("Zombie") == null)
            {
                isWaveRunning = false;
                nextWave();
            }
        }
    }
    //Start the next wave
    private void nextWave()
    {
        waveNumber++;
        points = points * nofPointsMultiplier;
        Debug.Log(points);
        numberOfZombies =(int)(numberOfZombies * nofPointsMultiplier);
        Debug.Log(numberOfZombies);
        spawningRoutine();
    }
    //Routine to display on the screen the wave number and number of zombies
    private void spawningRoutine()
    {
        string sentence = "Wave n : " + waveNumber + "\n Number of zombies : " + numberOfZombies * spawnerArray.Length;
        StartCoroutine(printUI(sentence));
        _audiosource.PlayOneShot(waveStartSound);
        numberOfZombiesCounter = numberOfZombies;
        StartCoroutine(delaySpawnRoutine());
    }

    IEnumerator printUI(string print)
    {
        UIText.GetComponent<Text>().text = print;
        UIText.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIText.SetActive(false);

    }

    IEnumerator delaySpawnRoutine()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        foreach(Transform trans in spawnerArray)
        {
            GameObject zombie = Instantiate(zombiePrefab, trans.position, trans.rotation);
            ZombieMaker.SetRandomZombieFromPoints(zombie, points);
        }
        numberOfZombiesCounter--;
        isWaveRunning = true;
        if (numberOfZombiesCounter > 0) StartCoroutine(delaySpawnRoutine());
    }
    
}
