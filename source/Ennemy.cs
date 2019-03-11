using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : HittableEntity
{
    //Target
    private GameObject player;
    
    //Detection and movement
    private float detectionDiam = Mathf.Infinity;

    //Attack
    private float attackDiam = 1f;
    public float attackDelay = 1;
    public float damagePerHit = 1;

    private float attackCounter = 0;

    //HP
    private bool alive = true;

    //Component
    private Animator _anim;
    private CapsuleCollider _col;
    private NavMeshAgent _navAgent;
    private Rigidbody _rb;

    //FX and audio
    public GameObject hitImpactFX;
    private AudioSource audiosrc;
    public AudioClip impactSound;
    public AudioClip deathSound;
    public AudioClip attackSound;

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionDiam);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDiam);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        audiosrc = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _col = GetComponent<CapsuleCollider>();
        _navAgent = GetComponent<NavMeshAgent>();
        attackDiam = _navAgent.stoppingDistance + 1f;
        Debug.Log(attackDiam);
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;
        

        if ((Vector3.Distance(player.transform.position, transform.position) <= detectionDiam) && (isAlive == true))
        {

            _navAgent.SetDestination(player.transform.position);

            _anim.SetFloat("speed", 1f);

            if (Vector3.Distance(player.transform.position, transform.position) <= attackDiam && attackCounter <= 0)
            {
                audiosrc.PlayOneShot(attackSound);
                _anim.SetTrigger("attack");
                attackCounter = attackDelay;
                Invoke("attackRoutine",attackDelay-.5f);
            }
        }
        else
        {
            _anim.SetFloat("speed", 0);
        }
    }

    private void attackRoutine()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDiam && alive==true)
        {
            Player pl = player.GetComponent<Player>();
            pl.TakeDamage(damagePerHit);
        }
    }

    protected override void Hit()
    {
        Debug.Log("hit");
    }

    protected override void Hit(RaycastHit hit)
    {
        Debug.Log("hit raycast");
        Destroy(Instantiate(hitImpactFX, hit.point, Quaternion.LookRotation(hit.normal)),1f);
        audiosrc.PlayOneShot(impactSound);
    }

    public void setHPandDMG(float hp, float dmg)
    {
        setMaxHP(hp);
        damagePerHit = dmg;
    }

    protected override void Die()
    {
        isAlive = false;
        audiosrc.PlayOneShot(deathSound);
        _rb.detectCollisions = false;
        _navAgent.isStopped = true;
        _anim.SetTrigger("fall");
        Destroy(gameObject, 3f);
    }
}
