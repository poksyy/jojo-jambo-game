using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private int jumpCount;
    private float LastShoot;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        jumpCount = 0;
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (Animator != null)
        {
            Animator.SetBool("running", Horizontal != 0.0f);
        }

        Grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        if (Grounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        if (jumpCount < 1)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            jumpCount++;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
