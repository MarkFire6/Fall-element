using UnityEngine;
using TMPro;

public class NPCQuest3D : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    public GrabbableKid3D kid;
    public Transform player;

    [TextArea] public string startDialogue = "Hi, can you help me find my kid? As a reward I will give you a clue on where you are.";
    [TextArea] public string completeDialogue = "Thank you for finding my kid. Here is your clue: you are near the old ruins behind the forest.";

    private bool playerInRange = false;
    private bool questStarted = false;
    private bool questCompleted = false;

    private void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!questStarted)
            {
                questStarted = true;
                ShowDialogue(startDialogue);
            }
            else if (!questCompleted && kid != null && kid.IsCarriedByPlayer(player))
            {
                kid.DropKid();
                kid.transform.position = transform.position + transform.forward * 1.5f;
                questCompleted = true;
                ShowDialogue(completeDialogue);
            }
            else if (questCompleted)
            {
                ShowDialogue(completeDialogue);
            }
            else
            {
                ShowDialogue("Please find my kid.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!questStarted)
                ShowDialogue("Press E to talk.");
            else if (!questCompleted)
                ShowDialogue("Did you find my kid?");
            else
                ShowDialogue(completeDialogue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideDialogue();
        }
    }

    private void ShowDialogue(string message)
    {
        if (dialogueText != null)
            dialogueText.text = message;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
    }

    private void HideDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}