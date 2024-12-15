using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform portalDestino;
    private bool isTeleporting = false;
    private Collider2D portalCollider;
    private Coroutine teleportCoroutine;

    private void Start()
    {
        portalCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;
            teleportCoroutine = StartCoroutine(TeleportPlayer(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportCoroutine != null)
            {
                StopCoroutine(teleportCoroutine);
            }
            isTeleporting = false;
        }
    }

    private IEnumerator TeleportPlayer(Collider2D player)
    {
        Debug.Log("Waiting 3 seconds...");

        yield return new WaitForSeconds(3f);

        player.transform.position = portalDestino.position;


        isTeleporting = false;
    }
}
