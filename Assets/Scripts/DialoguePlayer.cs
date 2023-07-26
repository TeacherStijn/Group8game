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
    public UnityEngine.UI.Image background;
    public RectTransform messageBox;
    public RectTransform nameBox;
    public bool isPlayingDialogue = false;
    public float fadeTime = 0.3f;

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

        displayBackground(message.backgroundId);

        // Display actor image
        actorImage.sprite = actor.avatar;
        actorImage.gameObject.SetActive(actor.avatar != null);

        // Display text
        actorName.text = actor.name;
        messageBody.text = message.text;
        messageBox.gameObject.SetActive(message.text.Length != 0);
        nameBox.gameObject.SetActive(actor.name.Length != 0);
        AnimateTextAlpha();

    }

    private void displayBackground(int backgroundId)
    {
        if (backgroundId >= 0)
        {
            if (backgroundId < dialogue.backgroundImages.Length)
            {
                if (dialogue.backgroundImages[backgroundId])
                {
                    background.sprite = dialogue.backgroundImages[backgroundId];
                    background.rectTransform.LeanAlpha(1f, fadeTime);
                }
                else // Null background: Hide previous background
                {
                    background.rectTransform.LeanAlpha(0, fadeTime);
                }
            }
            else
            {
                Debug.LogErrorFormat("Invalid background id `{0}` for dialogue `{1}`!", backgroundId, dialogue.name);
            }
        }
    }

    private void AnimateTextAlpha()
    {
        // TODO: Tween TMP alpha
    }

    public void OpenDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        currentMessageIdx = 0;

        // Make sure the dialogue box is invisible at the start of the dialogue
        background.rectTransform.LeanAlpha(0, 0);
        dialogueUI.alpha = 0;
        dialogueUI.LeanAlpha(1f, 0.4f).setEaseInCubic();

        // Display the first message to overwrite previous message display
        DisplayNextMessage();
        isPlayingDialogue = true;
    }

    public void CloseDialogue()
    {
        dialogue = null;
        dialogueUI.LeanAlpha(0f, fadeTime);
        background.rectTransform.LeanAlpha(0, fadeTime);
        isPlayingDialogue = false;
    }

    private void Start()
    {
        actorName = nameBox.GetComponentInChildren<TMP_Text>();
        messageBody = messageBox.GetComponentInChildren<TMP_Text>();
        dialogueUI = messageBox.parent.GetComponent<CanvasGroup>();

        if (!actorName || !messageBody || !dialogueUI || !background)
        {
            Debug.LogError("Missing or incorrect dialogue UI structure!");
        }

        // Make sure the dialogue box is active and invisible at the start
        dialogueUI.alpha = 0f;
        dialogueUI.gameObject.SetActive(true);
        background.rectTransform.LeanAlpha(0, 0);
        background.gameObject.SetActive(true);
        CloseDialogue();
    }

    private void Update()
    {
        if (isPlayingDialogue && Input.GetKeyDown(KeyCode.C))
        {
            CloseDialogue();
        }
        else if (isPlayingDialogue && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Dash")))
        {
            DisplayNextMessage();
        }
    }
}
