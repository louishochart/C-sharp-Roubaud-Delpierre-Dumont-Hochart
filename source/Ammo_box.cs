using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_box : MonoBehaviour
{
    public int main_ammo;
    public int second_ammo;

    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            player.GetComponent<Player>().LootAmmoBox(main_ammo, second_ammo);
            

            Destroy(gameObject);
        }
    }

    
}
