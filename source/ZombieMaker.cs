using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Script that manage the strength of the zombie depending on the wave level
public class ZombieMaker
{
    //Different statistics of the zombie
    public static float _speed = 2.2f;
    public static float _accel = 8;

    public static float _stopDist = 1.2f;
    public static float _scale = 1.2f;
    public static float _hp = 10;

    public static float _attackdelay = 1f;
    public static float _dmgPerHit = 6;

    // ------------------------------------
    //Depending on the wave level, it will add to the main statistic these values multiplied by the wave number

    public static float _speedAdder = .40f;
    public static float _accelAdder = .5f;

    public static float _stopDistAdder = .2f;
    public static float _scaleAdder = .06f;
    public static float _hpAdder = 5;

    public static float _attackdelayAdder = .1f;
    public static float _dmgPerHitAdder = 2;

    //Depending on the size of the zombie (defined randomly), add differents stats
    public static void SetRandomZombieFromPoints(GameObject instZombie, float points)
    {
        if (instZombie == null) return;

        int[] characteristicProps;

        switch (Mathf.Abs(Random.Range(0, 2)))
        {
            case 0:
                characteristicProps = new int[3] { 2, -1, 1};
                break;
            case 1:
                characteristicProps = new int[3] { -1, 2, 1};
                break;
            case 2:
                characteristicProps = new int[3] { 1, -1, 2};
                break;
            case 3:
                characteristicProps = new int[3] { 1, 2, -1};
                break;
            case 4:
                characteristicProps = new int[3] { -1, 1, 2};
                break;
            case 5:
                characteristicProps = new int[3] { -1, 1, 2};
                break;
            default:
                characteristicProps = new int[3] { 1, 1, 1};
                break;
        }
        calculateCharacteristics(instZombie, points, characteristicProps);
    }
    //calculate how to upgrade stats
    private static void calculateCharacteristics(GameObject instZombie, float points, int[] props)
    {
        Ennemy zombieScript = instZombie.GetComponent<Ennemy>();
        NavMeshAgent agent = instZombie.GetComponent<NavMeshAgent>();

        agent.speed = _speed + _speedAdder * props[0] * points;
        agent.acceleration = _accel + _accelAdder * props[0] * points;

        agent.stoppingDistance = _stopDist + _stopDistAdder * props[1] * points;
        zombieScript.transform.localScale = Vector3.one * (_scale + _scaleAdder * props[1] * points);
        zombieScript.setMaxHP(_hp + _hpAdder * props[1] * points);

        zombieScript.damagePerHit = _dmgPerHit + _dmgPerHitAdder * props[2] * points;
        zombieScript.attackDelay = _attackdelay + _attackdelayAdder * props[2] * points;
    }
}
