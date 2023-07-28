using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    private CanvasGroup messageBodyGroup;
    private CanvasGroup dialogueUI;

    [Tooltip("Currently played dialogue; assign value to start playing a dialogue immediately")]
    [SerializeField] private Dialogue dialogue;
    private int currentMessageIdx = 0;

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
        actorImage.rectTransform.LeanAlpha(Convert.ToSingle(actor.avatar != null), fadeTime / 2).setIgnoreTimeScale(true);

        // Display text
        actorName.text = actor.name;
        messageBody.text = message.text;
        messageBox.gameObject.SetActive(message.text.Length != 0);
        nameBox.gameObject.SetActive(actor.name.Length != 0 && message.text.Length != 0);
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
                    background.rectTransform.LeanAlpha(1f, fadeTime).setIgnoreTimeScale(true);
                }
                else // Null background: Hide previous background
                {
                    background.rectTransform.LeanAlpha(0, fadeTime).setIgnoreTimeScale(true);
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
        messageBodyGroup.alpha = 0;
        messageBodyGroup.LeanAlpha(1f, fadeTime).setIgnoreTimeScale(true);
    }

    public void OpenDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        currentMessageIdx = 0;

        // Make sure the dialogue box is invisible at the start of the dialogue
        background.rectTransform.LeanAlpha(0, 0).setIgnoreTimeScale(true);
        dialogueUI.alpha = 0;
        dialogueUI.LeanAlpha(1f, 0.5f).setEaseInCubic().setIgnoreTimeScale(true);

        // Display the first message to overwrite previous message display
        DisplayNextMessage();
        isPlayingDialogue = true;
        Time.timeScale = 0f;
    }

    public void CloseDialogue()
    {
        dialogue = null;
        dialogueUI.LeanAlpha(0f, fadeTime).setIgnoreTimeScale(true);
        background.rectTransform.LeanAlpha(0, fadeTime).setIgnoreTimeScale(true);
        isPlayingDialogue = false;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        actorName = nameBox.GetComponentInChildren<TMP_Text>();
        messageBody = messageBox.GetComponentInChildren<TMP_Text>();
        messageBodyGroup = messageBox.GetComponentInChildren<CanvasGroup>();
        dialogueUI = messageBox.parent.GetComponent<CanvasGroup>();

        if (!actorName || !messageBody || !dialogueUI || !background)
        {
            Debug.LogError("Missing or incorrect dialogue UI structure!");
        }

        // Make sure the dialogue box is active and invisible at the start
        dialogueUI.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        dialogueUI.alpha = 0f;

        // Check if there's a dialogue to play at the start of the level
        if (dialogue)
        {
            // Start with the background image already showing and the dialogue box fading in
            OpenDialogue(dialogue);
            background.rectTransform.LeanAlpha(1f, 0).setIgnoreTimeScale(true);
        }
        else
        {
            background.rectTransform.LeanAlpha(0, 0).setIgnoreTimeScale(true);
        }
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
