using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearAttack : MonoBehaviour
{
    private GameObject spear;
    private Animator animator;
    public bool curAttk; // = false;
    bool collisionBool;
    GameObject collisionObj;

    [Space(10)]
    [SerializeField] private float moveSpeed = 0.5f;
    
    //Audio
    private AudioSource miss;
    private AudioSource hit;

    private void Start()
    {
        spear = this.gameObject;
        animator = spear.GetComponent<Animator>();
        miss = this.gameObject.transform.Find("missSound").GetComponent<AudioSource>();
        hit = this.gameObject.transform.Find("hitSound").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !curAttk)
        {
            animator.SetBool("attacking", true);
            
            if (collisionBool)
            {
                collisionObj.GetComponent<enemyAI>().TakeDamage(10);
                hit.Play();
            }
            else
                miss.Play();
        }
        else if (!curAttk)
            animator.SetBool("attacking", false);

    }

    private void FixedUpdate()
    {
        collisionBool = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collisionBool = true;
            collisionObj = collision.gameObject;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collisionBool = false;
            collisionObj = null;
        }
    }

}
