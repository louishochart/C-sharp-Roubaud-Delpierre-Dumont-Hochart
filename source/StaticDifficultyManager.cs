using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDifficultyManager
{
    public static float PLAYERHP;
    public static float ZOMBIEHP;
    public static float SKELETONHP;
                        
    public static int PLAYERAMMO;
    public static float ZOMBIEDMG;
    public static float SKELETONDMG;

    public static void setDifficultyToHard()
    {
        PLAYERHP = 50;
        ZOMBIEHP = 40;
        SKELETONHP = 20;
        
        PLAYERAMMO = 30;
        ZOMBIEDMG = 15;
        SKELETONDMG = 10;
    }
    public static void setDifficultyToNormal()
    {
        PLAYERHP = 100;
        ZOMBIEHP = 20;
        SKELETONHP = 10;

        PLAYERAMMO = 90;
        ZOMBIEDMG = 8;
        SKELETONDMG = 6;
    }
}
