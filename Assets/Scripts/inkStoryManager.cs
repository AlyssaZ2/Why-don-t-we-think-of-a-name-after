using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for the Button component
using Ink.Runtime;
using TMPro;

public class inkStoryManager : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON; // Ink story file
    private Story story;

    [SerializeField] private TextMeshProUGUI dialogueText; // Dialogue field
    [SerializeField] private TextMeshProUGUI characterNameText; // Character name field
    [SerializeField] private Image characterPortrait; // Character portrait image
    [SerializeField] private GameObject TextPanel; 

    [SerializeField] private Button choiceButtonPrefab; // Prefab for choice buttons (Make sure this is a Button prefab)
    [SerializeField] private Transform choicesContainer; // UI container for choice buttons

    private Dictionary<string, Sprite> characterImages; // Dictionary for image lookup
    private bool isWaitingForChoice = false;
    private bool waitingForClickToShowChoices = false;

    void Start()
    {
        // Initialize Ink story
        story = new Story(inkJSON.text);

        // Load character portraits from Resources
        characterImages = new Dictionary<string, Sprite>
        {
            { "redbloodcell", Resources.Load<Sprite>("redbloodcell") },
 
        };

        DisplayNextLine();
    }

    void Update()
    {
        // Click anywhere to continue, unless waiting for a choice
        if (Input.GetMouseButtonDown(0))
        {
            if (waitingForClickToShowChoices)
            {
                waitingForClickToShowChoices = false;
                DisplayChoices();
            }
            else if (!isWaitingForChoice)
            {
                DisplayNextLine();
            }
        }
    }

    void DisplayNextLine()
    {
        if (isWaitingForChoice) return; // Don't continue if we're waiting for a choice

        if (story.canContinue)
        {
            string currentText = story.Continue();
            dialogueText.text = currentText;

            // Process and apply character name & image
            ProcessTags();
        }
        else
        {
            dialogueText.text = "The End.";
            HideUI();
        }

        if (story.currentChoices.Count > 0)
        {
            isWaitingForChoice = true; // We are waiting for a choice to be made
            waitingForClickToShowChoices = true; // Wait for the next click to show choices
        }
    }

    void HideUI()
{
    dialogueText.gameObject.SetActive(false);
    characterNameText.gameObject.SetActive(false);
    characterPortrait.gameObject.SetActive(false);
    choicesContainer.gameObject.SetActive(false);
    TextPanel.gameObject.SetActive(false);

}

    void ProcessTags()
    {
        List<string> currentTags = story.currentTags;

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length < 2) continue;

            string key = splitTag[0].Trim();
            string value = splitTag[1].Trim();

            if (key == "char")
            {
                characterNameText.text = value; // Set character name
            }
            else if (key == "image" && characterImages.ContainsKey(value))
            {
                characterPortrait.sprite = characterImages[value]; // Set character portrait
            }
        }
    }

    void DisplayChoices()
    {
        float startX = 200f;  // X position
        float startY = 100f;   // Initial Y position
        float spacingY = -50f; // Distance between buttons

        foreach (Ink.Runtime.Choice choice in story.currentChoices)
        {
            // Instantiate the button
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            // Positioning the button
            RectTransform buttonTransform = choiceButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(startX, startY + (choicesContainer.childCount * spacingY));

            int choiceIndex = choice.index;
            choiceButton.onClick.AddListener(() => ChooseChoice(choiceIndex));
        }
    }

    void ChooseChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        DestroyAllButtons();
        isWaitingForChoice = false;
        DisplayNextLine();
    }

    void DestroyAllButtons()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
