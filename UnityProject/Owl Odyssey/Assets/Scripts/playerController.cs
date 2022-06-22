using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Player")]
    public float speed = 10f;
    public float playerRotation = 100f;
    public float smooth = 5.0f;
    public bool curAttk; // = false;
    public bool canMove = true;

    [Header("Gravity")]
    public float gravity; //= 9.81f;
    public float curGravity;
    public float constGravity;
    public float maxGravity;

    private Vector3 gravityDirection;
    private Vector3 gravityMove;

    [Header("Components")]
    public Camera camera;
    public Vector3 display;
    public CharacterController controller;
    public Animator animator;
    public AudioSource audioSource;

    public float audDelay = 0.25f;
    private bool walkAud;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        gravityDirection = Vector3.down;
    }

    void Update()
    {
        calcGravity();
        move();
        rotate();
        attack();
    }

    void move()
    {
        if (canMove && !curAttk)
        {
            float horizontal = -Input.GetAxisRaw("Vertical");
            float vertical = Input.GetAxisRaw("Horizontal");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                controller.Move((direction * speed * Time.deltaTime) + gravityMove);
                animator.SetBool("walk", true);

                if (!walkAud)
                {
                    //StartCoroutine(walk());
                }
            }
            else
            {
                animator.SetBool("walk", false);
                controller.Move(gravityMove);
            }
        }
    }

    void rotate()
    {
        float lookAngle = Mathf.Atan2(Input.mousePosition.y - ((Screen.height / 2) - ((Screen.height/2) - camera.WorldToScreenPoint(controller.transform.position).y)), Input.mousePosition.x - (Screen.width/2)) * Mathf.Rad2Deg;
        //float lookAngle = Mathf.Atan2(Input.mousePosition.y - (Screen.height / 2), Input.mousePosition.x - (Screen.width / 2)) * Mathf.Rad2Deg;
        //Debug.Log(Input.mousePosition);

        Quaternion target = Quaternion.Euler(0, -lookAngle, 0);
        //Debug.Log(target);

        controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, target, Time.deltaTime * smooth);
    }

    bool isGrounded()
    {
        return controller.isGrounded;
    }

    void calcGravity()
    {
        if (isGrounded())
        {
            curGravity = constGravity;
        }
        else
        {
            if (curGravity > maxGravity)
            {
                curGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMove = gravityDirection * -curGravity * Time.deltaTime;
    }

    //IEnumerator walk()
    //{
    //    //walkAud = true;
    //    //audioSource.Play();
    //    //yield return new WaitForSeconds(audDelay);
    //    //walkAud = false;
    //}

    void attack()
    {
        if (Input.GetMouseButtonDown(0) && !curAttk)
        {
            canMove = false;
            animator.SetBool("attack", true);
            Debug.Log("Attacked");

            //if (collisionBool)
            //{
            //    collisionObj.GetComponent<enemyAI>().TakeDamage(10);
            //    hit.Play();
            //}
            //else
            //    miss.Play();
        }
        else if (!curAttk)
        {
            canMove = true;
            animator.SetBool("attack", false);
            //Debug.Log("Attack: " + curAttk);
        }

    }
}
