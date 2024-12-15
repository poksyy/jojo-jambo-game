using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<JohnMovement>().CollectCoin();

            Destroy(gameObject);
        }
    }
}
