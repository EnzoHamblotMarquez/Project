using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float NormalSpeed = 5f;
    public float DashSpeed = 6.5f;

    private float CurrentSpeed = 5f;
    private bool canDash = true;
    private Vector2 CurrentInputedDirection;
    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator animator;
    public int dashCooldown = 1000; //+700
    private float CurrentDashCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.identity; //Avoid player rotation

        CurrentInputedDirection = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical"); //Take player's input
        CurrentInputedDirection.Normalize();

        if (Input.GetAxis("Horizontal") < 0)
        {
            m_spriteRenderer.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
<<<<<<< Updated upstream
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            gameObject.GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
=======
            m_spriteRenderer.flipX = false;
>>>>>>> Stashed changes
        }

        animator.SetBool("moving", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
       

        if (Input.GetKey("space") && canDash)
        {
            StartCoroutine(Dash());
            DashCooldown();
        }
        CurrentDashCooldown -= Time.deltaTime;
        if(CurrentDashCooldown <= 0f)
        {
            canDash = true;
            CurrentDashCooldown = 0;
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.MovePosition(m_rigidbody2D.position + CurrentInputedDirection * CurrentSpeed * Time.fixedDeltaTime);
    }
    IEnumerator Dash()
    {
        CurrentSpeed = DashSpeed;
        animator.SetBool("dash", true);

        yield return new WaitForSeconds(0.7f);
        CurrentSpeed = NormalSpeed;
        animator.SetBool("dash", false);
        CurrentDashCooldown = dashCooldown;
    }

    async void DashCooldown()
    {
        canDash = false;
        await Task.Delay(dashCooldown);
        canDash = true;
    }
}