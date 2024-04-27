using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float NormalSpeed = 5f;
    public float DashSpeed = 6.5f;

    private float CurrentSpeed = 5f;
    private bool canDash = true;
    private Vector2 CurrentInputedDirection;
    private Rigidbody2D m_rigidbody2D;
    public int dashCooldown = 1000; //+700

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.identity; //Avoid player rotation

        CurrentInputedDirection = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical"); //Take player's input
        CurrentInputedDirection.Normalize();

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

    private void FixedUpdate()
    {
        m_rigidbody2D.MovePosition(m_rigidbody2D.position + CurrentInputedDirection * CurrentSpeed * Time.fixedDeltaTime);
    }
    IEnumerator Dash()
    {
        CurrentSpeed = DashSpeed;
        gameObject.GetComponent<Animator>().SetBool("dash", true);

        yield return new WaitForSeconds(0.7f);
        CurrentSpeed = NormalSpeed;
        gameObject.GetComponent<Animator>().SetBool("dash", false);
    }

    async void DashCooldown()
    {
        canDash = false;
        await Task.Delay(dashCooldown);
        canDash = true;
    }
}