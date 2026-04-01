using UnityEngine;

public class DeadlyPlatform : MonoBehaviour
{
    public Transform playerStartPoint;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.transform.root.gameObject;

        if (player.CompareTag("Player"))
        {
            RespawnCharacter(player);
        }
    }

    private void RespawnCharacter(GameObject player)
    {
        Debug.Log("Respawn");

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;

        player.transform.position = playerStartPoint.position;
        player.transform.rotation = playerStartPoint.rotation;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (cc != null)
            cc.enabled = true;
    }
}