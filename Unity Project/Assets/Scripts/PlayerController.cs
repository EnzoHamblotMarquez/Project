using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float movementMultiplier = 1f;
    bool canDash = true;
    int dashCooldown = 1000; //+700

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        Debug.Log(Input.GetAxis("Vertical"));

        gameObject.transform.Translate(Input.GetAxis("Horizontal") * movementMultiplier * Time.deltaTime, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        gameObject.transform.Translate(0, Input.GetAxis("Vertical") * movementMultiplier * Time.deltaTime, 0);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            gameObject.GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKey("space") && canDash == true)
        {
            StartCoroutine(Dash());
            DashCooldown();
        }

    }
    IEnumerator Dash()
    {
        movementMultiplier = 2f;
        gameObject.GetComponent<Animator>().SetBool("dash", true);

        yield return new WaitForSeconds(0.7f);
        movementMultiplier = 1f;
        gameObject.GetComponent<Animator>().SetBool("dash", false);
    }

    async void DashCooldown()
    {
        canDash = false;
        await Task.Delay(dashCooldown);
        canDash = true;
    }
}