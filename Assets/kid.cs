using UnityEngine;
using TMPro;

public class GrabbableKid3D : MonoBehaviour
{
    public Transform holdPoint;
    public TMP_Text interactionText;
    public GameObject interactionPanel;

    private bool playerInRange = false;
    private bool isCarried = false;
    private Transform currentPlayer;

    private Rigidbody rb;
    private Collider kidCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        kidCollider = GetComponent<Collider>();

        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarried)
                PickUpKid();
            else
                DropKid();
        }

        if (isCarried && holdPoint != null)
        {
            transform.position = holdPoint.position;
            transform.rotation = holdPoint.rotation;
        }
    }

    private void PickUpKid()
    {
        isCarried = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (kidCollider != null)
            kidCollider.enabled = false;

        transform.SetParent(holdPoint);

        if (interactionText != null)
            interactionText.text = "Press E to drop the kid.";
    }

    public void DropKid()
    {
        isCarried = false;
        transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;

        if (kidCollider != null)
            kidCollider.enabled = true;

        if (interactionText != null)
            interactionText.text = "Press E to pick up the kid.";
    }

    public bool IsCarriedByPlayer(Transform player)
    {
        return isCarried && currentPlayer == player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            currentPlayer = other.transform;

            if (interactionText != null)
            {
                interactionText.text = isCarried ? "Press E to drop the kid." : "Press E to pick up the kid.";
            }

            if (interactionPanel != null)
                interactionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (!isCarried)
                currentPlayer = null;

            if (interactionPanel != null && !isCarried)
                interactionPanel.SetActive(false);
        }
    }
}