using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    //Constraints
    private bool isReloading = false;
    private bool isRunning = false;

    //UI handler
    public Text _UI;

    //Audio handler
    private AudioSource _audioSource;

    //Bullet count handler
    public int _oneChargerCapacity;
    public int _totalBulletCount;
    public float _reloadTime;
    private int _actualBulletCount;

    public bool infiniteAmmo;
    public bool headShot;

    //Fire rate handler
    public bool _isAutomatic;
    public float _delayBetweenTwoShots;
    private float _shotCounter;

    //Damage
    public float _damagePerHit;

    //Components
    private Animator _anim;
    private Transform _bulletSpawn;

    //FX
    public ParticleSystem flash;
    public AudioClip reloadSample;
    public AudioClip noMoreAmmoSample;
    public GameObject headShotBloodFX;
    

    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();

        _anim = GetComponent<Animator>();

        _bulletSpawn = transform.parent;
        

        _actualBulletCount = _oneChargerCapacity;
        _totalBulletCount -= _oneChargerCapacity;

        _shotCounter = _delayBetweenTwoShots;

        printUI();
    }

    public void printUI()
    {
        if (infiniteAmmo)
        {
            _UI.text = _actualBulletCount + " / inf.";
        }
        else
        {
            _UI.text = _actualBulletCount + " / " + _totalBulletCount;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            isRunning = true;
            _anim.SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            _anim.SetBool("isRunning", false);
        }

        _shotCounter -= Time.deltaTime;
        

        if (_isAutomatic){if (Input.GetMouseButton(0)){Fire();}}
        else{if (Input.GetMouseButtonDown(0)){Fire();}}

        
        
        if (Input.GetKeyDown("r") && !isReloading)
        {
            Reload();
        }
    }

    public void AddAmmunition(int ammo)
    {
        _totalBulletCount += ammo;
        printUI();
    }

    void Reload()
    {
        if(!isRunning && !(_totalBulletCount <= 0) && (_actualBulletCount!=_oneChargerCapacity) ) StartCoroutine(reloadRoutine());
    }

    IEnumerator reloadRoutine()
    {
        isReloading = true;
        _anim.SetTrigger("Reload");
        _audioSource.PlayOneShot(reloadSample);
        yield return new WaitForSeconds(_reloadTime);
        int bulletsToFill = _oneChargerCapacity - _actualBulletCount;

        if (bulletsToFill > _totalBulletCount)
        {
            _actualBulletCount += _totalBulletCount;
            _totalBulletCount = 0;
        }
        else
        {
            _actualBulletCount = _oneChargerCapacity;
            _totalBulletCount -= bulletsToFill;
        }
        if (infiniteAmmo) _totalBulletCount = 999;
        printUI();
        isReloading = false;
        
    }

    void Fire()
    {
        if (_actualBulletCount > 0 && _shotCounter < 0 && !isRunning && !isReloading)
        {
            flash.Play();
            _audioSource.Play();
            _anim.SetTrigger("fire");
            _actualBulletCount--;
            _shotCounter = _delayBetweenTwoShots;

            RaycastHit hit;
            Debug.DrawRay(_bulletSpawn.position, _bulletSpawn.TransformDirection(Vector3.forward) * 50f, Color.yellow);
            if (Physics.Raycast(_bulletSpawn.position, _bulletSpawn.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.Log("Did Hit");
                HandleHit(hit);
            }
        }
        else if (_actualBulletCount <= 0 && _shotCounter <= 0)
        {
            _audioSource.PlayOneShot(noMoreAmmoSample);
            _shotCounter = _delayBetweenTwoShots;
        }
        printUI();
    }

    void HandleHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Head" && headShot)
        {
            Debug.Log("HEADSHOT");
            transform.parent.parent.parent.GetComponent<Player>().LootAmmoBox(1, 0);
            hit.transform.GetComponentInParent<HittableEntity>().TakeDamage(_damagePerHit * 3, hit);
            GameObject bloodfx = Instantiate(headShotBloodFX, hit.point, hit.transform.rotation);
            bloodfx.transform.SetParent(hit.transform.parent);
            Destroy(bloodfx, 3f);
            Destroy(hit.transform.gameObject);

        }

        HittableEntity hittedObject =  hit.transform.GetComponent<HittableEntity>();
        
        if (hittedObject != null)
        {
            hittedObject.TakeDamage(_damagePerHit, hit);
        }
    }

    public bool isReloadingWeapon()
    {
        return isReloading;
    }
}
