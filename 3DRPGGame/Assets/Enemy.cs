using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //use AI API

public class Enemy : MonoBehaviour
{


    #region Value
    [Header("MoveSpeed"), Range(0,100)]
    public float speed = 1.5f;
    [Header("Attack"), Range(0,100)]
    public float attack = 20f;
    [Header("HP"), Range(0,1000)]
    public float hp = 350f;
    [Header("EXP"), Range(0,1000)]
    public float exp = 30f;
    [Header("DropProperty"), Range(0f,1f)]
    public float prop = 0.3f;
    [Header("DropObject")]
    public Transform skull;
    [Header("Attck Range"), Range(0, 10)]
    public float rangeAtk =1.5f;
    [Header("Attck CD"), Range (0,10)]
    public float cd =3.5f;

    private NavMeshAgent nav;
    private Animator anim;
    private Player player;

    private float timer;// Timer
    #endregion

    #region fangfa
    private void Move()
    {
        nav.SetDestination(player.transform.position);
        anim.SetFloat("移動",nav.velocity.magnitude);

        if (nav.remainingDistance <= rangeAtk)
        {
            Attack();
        }

    }

    private void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= cd)
        {
            timer = 0;
            anim.SetTrigger("攻擊觸發");
        }
        
    }
    #endregion

    #region shijian
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        player = FindObjectOfType<Player>();
            
        nav.speed = speed;
        nav.stoppingDistance = rangeAtk;
    }

    void Update()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, rangeAtk);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.name == "玩家")
        {
            other.GetComponent<Player>().Hit(attack, transform);
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

}
