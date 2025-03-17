using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class battleStoryManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText; // Dialogue field
    [SerializeField] private TextMeshProUGUI playerPokemonName; // Character name field
    [SerializeField] private Image playerPokemonSprite; // Character portrait image
    [SerializeField] private GameObject TextPanel; 
    [SerializeField] private Image opponentPokemonSprite;

    [SerializeField] private Button choiceButtonPrefab; // Prefab for choice buttons (Make sure this is a Button prefab)
    [SerializeField] private Transform disappearsOnEnd; // UI container for choice buttons

    [SerializeField] private int playerHealth = 100;
    [SerializeField] private TextMeshProUGUI opponentName;
    private float startX = 0f;
    private float startY = 100f;
    private float spacingY = -50f;
    private int started = 1;
    private int isOpponentTurn = 0;

    private Dictionary<string, Sprite> characterImages; 

    void Start()
    {
        dialogueText.text = "An opponent has aproached.";
    }

    void Update()
    {
        if (started == 1){
            if (Input.GetMouseButtonDown(0)){
            yourTurn();
            started = 0;
            }

        if (isOpponentTurn == 1){
            if (Input.GetMouseButtonDown(0)){
            yourTurn();
            isOpponentTurn = 0;
            }
        }

        if (playerHealth<=0){
            dialogueText.text = "GAME OVER!";
            SceneManager.LoadScene("Main Menu");
            }
        }
    }
    
        
    void yourTurn(){
        dialogueText.text = "Make your move.";
        showChoices();
    }

    void win(){
        
        for (int i = 0; i <= 1; i++) {
            if (Input.GetMouseButtonDown(0)){
                if (i == 0) {
                    dialogueText.text = opponentName + " has taken lethal damage!";
            }
                if (i == 1) {
                    dialogueText.text = "You win!";
                    endScene();
                }
            }
        }
    }
    
    void endScene(){
        SceneManager.LoadScene("Traveling");
    }


    void HideUI()
{
    dialogueText.gameObject.SetActive(false);
    playerPokemonName.gameObject.SetActive(false);
    playerPokemonSprite.gameObject.SetActive(false);
    TextPanel.gameObject.SetActive(false);
    TextPanel.gameObject.SetActive(false);
    opponentPokemonSprite.gameObject.SetActive(false);

}

    void ShowUI(){
    dialogueText.gameObject.SetActive(true);
    playerPokemonName.gameObject.SetActive(true);
    playerPokemonSprite.gameObject.SetActive(true);
    TextPanel.gameObject.SetActive(true);
    TextPanel.gameObject.SetActive(true);
    opponentPokemonSprite.gameObject.SetActive(true);       
    }


    void showChoices()
    {
            DestroyAllButtons();
            // Abilities button
            Button abilitiesButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
            abilitiesButton.GetComponentInChildren<TextMeshProUGUI>().text = "Abilities";

            RectTransform buttonTransform = abilitiesButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
            abilitiesButton.transform.SetAsLastSibling();

            abilitiesButton.onClick.AddListener(showAbilities);

            //Items button
            Button itemsButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
            itemsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Items";

            RectTransform itemsTransform = itemsButton.GetComponent<RectTransform>();
            itemsTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
            itemsButton.transform.SetAsLastSibling();

            itemsButton.onClick.AddListener(showItems);
        
    }

    void showAbilities(){
        DestroyAllButtons();

        //Ability One Button
        Button abilityOneButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        abilityOneButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ability One";

        RectTransform abilityOneTransform = abilityOneButton.GetComponent<RectTransform>();
        abilityOneTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
        abilityOneButton.transform.SetAsLastSibling();

        abilityOneButton.onClick.AddListener(abilityOne);

        //Ability Two Button
        Button abilityTwoButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        abilityTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ability Two";

        RectTransform abilityTwoTransform = abilityTwoButton.GetComponent<RectTransform>();
        abilityTwoTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
        abilityTwoButton.transform.SetAsLastSibling();

        abilityTwoButton.onClick.AddListener(abilityTwo);

    }

    void abilityOne(){
        DestroyAllButtons();
        dialogueText.text = "You use ability one!";
        opponentTurn();
    }


    void abilityTwo(){
        DestroyAllButtons();
        dialogueText.text = "You use ability two!";
        opponentTurn();
    }

    void DestroyAllButtons()
    {
        foreach (Transform child in disappearsOnEnd)
        {
            Destroy(child.gameObject);
        }
    }


    void showItems(){
        DestroyAllButtons();

        //Item One Button
        Button itemOneButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        itemOneButton.GetComponentInChildren<TextMeshProUGUI>().text = "Item One";

        RectTransform itemOneTransform = itemOneButton.GetComponent<RectTransform>();
        itemOneTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
        itemOneButton.transform.SetAsLastSibling();

        itemOneButton.onClick.AddListener(itemOne);

        //Item Two Button
        Button itemTwoButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        itemTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = "Item Two";

        RectTransform itemTwoTransform = itemTwoButton.GetComponent<RectTransform>();
        itemTwoTransform.anchoredPosition = new Vector2(startX, startY + (disappearsOnEnd.childCount * spacingY));
        itemTwoButton.transform.SetAsLastSibling();

        itemTwoButton.onClick.AddListener(itemTwo);

    }

    void itemOne(){
        DestroyAllButtons();
        dialogueText.text = "You use item one!";
        opponentTurn();   
    }

    void itemTwo(){
        DestroyAllButtons();
        dialogueText.text = "You use item two!";
        opponentTurn();   
    }

    void opponentTurn(){
        DestroyAllButtons();
        dialogueText.text = "It is the opponent's turn";
        isOpponentTurn = 1;

    }
}