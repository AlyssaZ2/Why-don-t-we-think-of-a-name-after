using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class inkStoryManager : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON; // ink file
    private Story story;

    [SerializeField] private TextMeshProUGUI dialogueText; // dialogue
    [SerializeField] private TextMeshProUGUI speakerName; // name
    [SerializeField] private Image speakerSprite; // pic
    [SerializeField] private GameObject TextPanel; 

    [SerializeField] private Button choiceButtonPrefab; //button prefav
    [SerializeField] private Transform disappearsOnEnd; //container for choice buttons
    [SerializeField] private bool scenceChange;

    private Dictionary<string, Sprite> characterImages; // Where da pics go~
    private bool isWaitingForChoice = false;
    private bool waitingForClickToShowChoices = false;

    [SerializeField] public string nextSceneName;

    void Start()
    {
        story = new Story(inkJSON.text);

        // Load da pics or they wont show :(
        characterImages = new Dictionary<string, Sprite>
        {
            { "redBloodCell", Resources.Load<Sprite>("redbloodcell") },
            { "DistributorRbc", Resources.Load<Sprite>("distributerRbc")},
            { "elderRbc", Resources.Load<Sprite>("elderRbc")},
            { "bacteria", Resources.Load<Sprite>("bacteria")},
            { "whiteBloodCell", Resources.Load<Sprite>("whiteBloodCell")},
            { "Blank", Resources.Load<Sprite>("blank")},
        };

        DisplayNextLine();
    }

    void Update()
    {
        // Click to continue, unless waiting for choice 
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
        if (isWaitingForChoice) return; // Don't continue if waiting for a choice

        if (story.canContinue)
        {
            string currentText = story.Continue();
            dialogueText.text = currentText;

            ProcessTags();
        }
        else
        {
            if (scenceChange == true){
                SceneManager.LoadScene(nextSceneName);
            }else{
            HideUI();
            }
        }

        if (story.currentChoices.Count > 0)
        {
            isWaitingForChoice = true; 
            waitingForClickToShowChoices = true; 
        }
    }

    void HideUI()
{
    dialogueText.gameObject.SetActive(false);
    speakerName.gameObject.SetActive(false);
    speakerSprite.gameObject.SetActive(false);
    disappearsOnEnd.gameObject.SetActive(false);
    TextPanel.gameObject.SetActive(false);

}

    void ProcessTags()//take da hashtags and make them real
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
                speakerName.text = value; 
            }
            else if (key == "image" && characterImages.ContainsKey(value))
            {
                speakerSprite.sprite = characterImages[value]; 
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
            
            Button choiceButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            RectTransform buttonTransform = choiceButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));

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
        foreach (Transform child in disappearsOnEnd)
        {
            Destroy(child.gameObject);
        }
    }
}
