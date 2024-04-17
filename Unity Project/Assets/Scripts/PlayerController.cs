using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float movement = 1f;
    bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            gameObject.transform.Translate(-movement * Time.deltaTime, 0, 0);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey("right"))
        {
            gameObject.transform.Translate(movement * Time.deltaTime, 0, 0);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("up"))
        {
            gameObject.transform.Translate(0, movement * Time.deltaTime, 0);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
        }
        if (Input.GetKey("down"))
        {
            gameObject.transform.Translate(0, -movement * Time.deltaTime, 0);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
        }

        if (!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down"))
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKey("space") && canDash == true)
        {
            StartCoroutine(Dash());
            Dash();
        }
    }
    IEnumerator Dash()
    {
        movement = 2f;
        gameObject.GetComponent<Animator>().SetBool("dash", true);

        yield return new WaitForSeconds(0.7f);
        movement = 1f;
        gameObject.GetComponent<Animator>().SetBool("dash", false);

        canDash = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
    }
}