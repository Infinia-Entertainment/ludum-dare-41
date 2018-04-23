using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour {
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float speed = 3.25f;
    public GameObject lights;
    private float xValue;

    public float minX, maxX;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame


    void Update () {
        Look();
        Move();
    }

    void Look()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 2);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Look_Down"))
            {
                animator.SetInteger("State", 0);
            }
        }
    }

    void Move()
    {
        int isEntered = PlayerPrefs.GetInt("EnterButtonPressed");
        if (isEntered == 1)
        {
            xValue = Input.GetAxis("Horizontal");
            Vector3 newPos = new Vector3(xValue + transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);

            var pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = pos;
            if (xValue > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (xValue < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
    //void Turn()
    //{
    //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //    {
            
    //     //   lights.transform.position = new Vector3(lightsXValue,lights.transform.position.y, lights.transform.position.z);
    //    }
    //    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //    {
           
    //       // lights.transform.position = new Vector3(normalXValue, lights.transform.position.y, lights.transform.position.z);
    //    }
    //}
}
