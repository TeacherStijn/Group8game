using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class DialoguePlayer : MonoBehaviour
{
    #region Singleton

    public static DialoguePlayer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Dialogue Player instance already exists!");
        }

        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #endregion

    public UnityEngine.UI.Image actorImage;
    public RectTransform messageBox;
    public RectTransform nameBox;
    public bool isPlayingDialogue = false;

    private TMP_Text actorName;
    private TMP_Text messageBody;
    private CanvasGroup dialogueUI;

    [SerializeField] private Dialogue dialogue;
    private int currentMessageIdx;

    public void DisplayNextMessage()
    {
        if (currentMessageIdx > dialogue.messages.Length - 1)
        {
            CloseDialogue();
            return;
        }

        Dialogue.Message message = dialogue.messages[currentMessageIdx++];
        Dialogue.Actor actor = dialogue.actors[message.actorId];

        actorImage.sprite = actor.avatar;
        actorImage.gameObject.SetActive(actor.avatar != null);
        actorName.text = actor.name;
        messageBody.text = message.text;
        AnimateTextAlpha();

    }

    private void AnimateTextAlpha()
    {
        // TODO: Tween TMP alpha
    }

    public void OpenDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        currentMessageIdx = 0;
        dialogueUI.LeanAlpha(1f, 0.4f).setEaseInCubic();


        // Display the first message to overwrite previous message display
        DisplayNextMessage();
        isPlayingDialogue = true;
    }

    public void CloseDialogue()
    {
        dialogue = null;
        dialogueUI.LeanAlpha(0f, 0.3f);
        isPlayingDialogue = false;
    }

    private void Start()
    {
        actorName = nameBox.GetComponentInChildren<TMP_Text>();
        messageBody = messageBox.GetComponentInChildren<TMP_Text>();
        dialogueUI = messageBox.parent.GetComponent<CanvasGroup>();

        if (!actorName || !messageBody || !dialogueUI)
        {
            Debug.LogError("Missing or incorrect dialogue UI structure!");
        }

        // Make sure the dialogue box is closed at the start
        dialogueUI.alpha = 0f;
        CloseDialogue();
    }

    private void Update()
    {
        if (isPlayingDialogue && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Dash")))
        {
            DisplayNextMessage();
        }
    }
}
