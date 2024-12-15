using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject CoinPrefab;
    public GameObject John;
    public Animator animator;

    private float LastShoot;
    private int Health = 3;
    private bool isDead = false;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead || John == null) return;

        Vector3 direction = John.transform.position - transform.position;

        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.75f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);

        animator.SetTrigger("shoot");
    }

    public void Hit()
    {
        if (isDead) return;

        Health -= 1;

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hurt");
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("die");

        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        if (CoinPrefab != null)
        {
            StartCoroutine(DropCoinWithDelay(0.7f));
        }

        Destroy(gameObject, 1.0f);
    }

    private IEnumerator DropCoinWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(CoinPrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (John.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Die() ;
            }
        }
    }
}
