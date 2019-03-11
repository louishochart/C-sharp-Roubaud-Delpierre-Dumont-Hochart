using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : HittableEntity
{
    public AudioClip gruntSample;
    public AudioClip ammo_loot;

    private AudioSource _source;

    private Image bloodSplash;
    private Color bloodSplashColor;

    public float InvincibilityTime;

    public Text hpUIPrint;

    WeaponHandler _weaphandl;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _source = GetComponent<AudioSource>();
        bloodSplash = GameObject.FindGameObjectWithTag("BloodSplash").GetComponent<Image>();
        bloodSplashColor = bloodSplash.color;
        bloodSplashColor.a = 0;
        bloodSplash.color = bloodSplashColor;
        _weaphandl = transform.GetChild(0).transform.GetChild(0).GetComponent<WeaponHandler>();
        

        printHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodSplashColor.a <= 0) return;
        bloodSplashColor.a -= Time.deltaTime;
        bloodSplash.color = bloodSplashColor;
    }

    public void LootAmmoBox(int main, int second)
    {
        _source.PlayOneShot(ammo_loot);
        _weaphandl.addAmmo(main, second);
    }

    protected override void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    protected override void Hit()
    {
        bloodSplashColor.a = 1;
        bloodSplash.color = bloodSplashColor;
        _source.PlayOneShot(gruntSample);
        StartCoroutine(invicible());
        printHP();
    }
    protected override void Hit(RaycastHit hit)
    {
        bloodSplashColor.a = 1;
        bloodSplash.color = bloodSplashColor;
        _source.PlayOneShot(gruntSample);
        printHP();
    }

    private IEnumerator invicible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibilityTime);
        isInvincible = false;
    }

    private void printHP()
    {
        hpUIPrint.text = "HP : " + currentHp;
    }
}
