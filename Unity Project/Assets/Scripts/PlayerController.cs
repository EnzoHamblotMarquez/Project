using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementMultiplier = 5f;
    private bool canDash = true;
    public int dashCooldown = 1000; //+700

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity; //Avoid player rotation

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized; //Take player's input

        gameObject.transform.Translate(movement * movementMultiplier * Time.deltaTime);

        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


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
        movementMultiplier = 1.5f * movementMultiplier;
        gameObject.GetComponent<Animator>().SetBool("dash", true);

        yield return new WaitForSeconds(0.7f);
        movementMultiplier = movementMultiplier / 1.5f;
        gameObject.GetComponent<Animator>().SetBool("dash", false);
    }

    async void DashCooldown()
    {
        canDash = false;
        await Task.Delay(dashCooldown);
        canDash = true;
    }
}