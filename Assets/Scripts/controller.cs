using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;

    public LayerMask ground;
    public Transform groundcheck;
    public float speed;
    public float jump;

    private bool canJump = false;
    private float minX;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = -bounds.x + 2f;
    }

    void Update()
    {
        canJump = Physics2D.OverlapCircle(groundcheck.position, 0.15f, ground);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            sprite.flipX = true;
            animator.SetInteger("direction", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            sprite.flipX = false;
            animator.SetInteger("direction", 1);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetInteger("direction", 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            canJump = false;
            transform.Translate(new Vector2(0, jump * Time.deltaTime));
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");
        }
        if (transform.position.x < minX)
        {
            Vector3 temp = transform.position;
            temp.x = minX;
            transform.position = temp;
        }
    }
}
