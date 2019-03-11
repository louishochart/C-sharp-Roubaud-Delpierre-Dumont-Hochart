using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    public GameObject main_weapon;
    public GameObject secondary_weapon;

    private int weaponSelected;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelected = 0;

        main_weapon.GetComponent<WeaponScript>().printUI();
        secondary_weapon.GetComponent<WeaponScript>().printUI();

        secondary_weapon.SetActive(false);
        main_weapon.SetActive(true);
        main_weapon.GetComponent<Animator>().Rebind();

        
    }

    public WeaponScript getActiveWeapon()
    {
        if(weaponSelected == 0)
        {
            return main_weapon.GetComponent<WeaponScript>();
        }
        else
        {
            return secondary_weapon.GetComponent<WeaponScript>();
        }
    }

    public void addAmmo(int main, int second)
    {
        main_weapon.GetComponent<WeaponScript>().AddAmmunition(main);
        secondary_weapon.GetComponent<WeaponScript>().AddAmmunition(main);
    }

    // Update is called once per frame
    void Update()
    {
        if (main_weapon.GetComponent<WeaponScript>().isReloadingWeapon() || secondary_weapon.GetComponent<WeaponScript>().isReloadingWeapon()) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(weaponSelected == 0)
            {
                weaponSelected = 1;
                main_weapon.SetActive(false);
                secondary_weapon.SetActive(true);
                secondary_weapon.GetComponent<Animator>().Rebind();
            }
            else if(weaponSelected == 1)
            {
                weaponSelected = 0;
                secondary_weapon.SetActive(false);
                main_weapon.SetActive(true);
                main_weapon.GetComponent<Animator>().Rebind();
            }
        }
    }
}
