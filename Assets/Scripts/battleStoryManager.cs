using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

//I need to go to bed T-T
//I choose you Pikachu!!!!!!

public class battleStoryManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText; 
    [SerializeField] private TextMeshProUGUI playerPokemonName; 
    [SerializeField] private Image playerPokemonSprite; 
    [SerializeField] private GameObject TextPanel; 
    [SerializeField] private Image opponentPokemonSprite;

    [SerializeField] private Button choiceButtonPrefab; 
    [SerializeField] private Transform disappearsOnEnd; 

    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private Slider opponentHealthSlider;
    [SerializeField] private int opponentHealth = 100;

    [SerializeField] private Slider opponentEnergySlider;
    [SerializeField] private int opponentEnergy = 30;

    [SerializeField] private TextMeshProUGUI playerHPNumbers;
    [SerializeField] private TextMeshProUGUI opponentHPNumbers;

    [SerializeField] private TextMeshProUGUI opponentName;

    private string opponentPokemonName;
    private string PokemonName;

    private float startX = 0f;
    private float startY = 100f;
    private float spacingY = -50f;
    private int started = 1;

    private bool antibioticResistence = false;
    public int opponentType = 1;

    private Dictionary<string, Sprite> characterImages; 

    void Start()
    {
        
        characterImages = new Dictionary<string, Sprite>
        {
            { "redbloodcell", Resources.Load<Sprite>("redbloodcell") },
            { "whiteBloodCell", Resources.Load<Sprite>("whiteBloodCell")},
            { "bacteria", Resources.Load<Sprite>("bacteria")},
            { "strep", Resources.Load<Sprite>("strep")},
            { "staph", Resources.Load<Sprite>("staph")},
            { "ecoli", Resources.Load<Sprite>("ecoli")},
 
        };

        playerHPNumbers.text = playerHealth + "/100";
        opponentHPNumbers.text = opponentHealth + "/100";

        playerHealthSlider.value = playerHealth;
        opponentHealthSlider.value = opponentHealth;
        PokemonName = "White Blood Cell";
        playerPokemonName.text = PokemonName;
        playerPokemonSprite.sprite = characterImages["whiteBloodCell"];

        //Opponent type 1: strep
        //Opponent type 2: staph
        //Opponent type 3: e.coli
        if (opponentType == 1 ){
            opponentPokemonName = "Strep";
            opponentName.text = opponentPokemonName;
            opponentPokemonSprite.sprite = characterImages["strep"];
            antibioticResistence = false; 
        }
        if (opponentType == 2 ){
            opponentPokemonName = "Staph";
            opponentName.text = opponentPokemonName;
            opponentPokemonSprite.sprite = characterImages["staph"];
            antibioticResistence = false; 
        }
         if (opponentType == 3 ){
            opponentPokemonName = "E.coli";
            opponentName.text = opponentPokemonName;
            opponentPokemonSprite.sprite = characterImages["ecoli"];
            antibioticResistence = false; 
        }


        dialogueText.text = "A stray " + opponentPokemonName + " has approached!";
    }

    void Update()
    {
        if (started == 1){
            if (Input.GetMouseButtonDown(0)){
            yourTurn();
            started = 0;
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
            buttonTransform.anchoredPosition = new Vector2(startX, startY + (1*spacingY));
            abilitiesButton.transform.SetAsLastSibling();

            abilitiesButton.onClick.AddListener(showAbilities);

            //Items button
            Button itemsButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
            itemsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Items";

            RectTransform itemsTransform = itemsButton.GetComponent<RectTransform>();
            itemsTransform.anchoredPosition = new Vector2(startX, startY + (2 * spacingY));
            itemsButton.transform.SetAsLastSibling();

            itemsButton.onClick.AddListener(showItems);
        
    }

    void showAbilities(){
        DestroyAllButtons();

        //Ability One Button
        Button abilityOneButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        abilityOneButton.GetComponentInChildren<TextMeshProUGUI>().text = "Attack";

        RectTransform abilityOneTransform = abilityOneButton.GetComponent<RectTransform>();
        abilityOneTransform.anchoredPosition = new Vector2(startX, startY + (spacingY));
        abilityOneButton.transform.SetAsLastSibling();

        abilityOneButton.onClick.AddListener(abilityOne);

        //Ability Two Button
        Button abilityTwoButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        abilityTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = "Swallow";

        RectTransform abilityTwoTransform = abilityTwoButton.GetComponent<RectTransform>();
        abilityTwoTransform.anchoredPosition = new Vector2(startX, startY + (2 * spacingY));
        abilityTwoButton.transform.SetAsLastSibling();

        abilityTwoButton.onClick.AddListener(abilityTwo);

    }

    void abilityOne(){
        DestroyAllButtons();
        dialogueText.text = "You attack!";
        opponentHealth = opponentHealth - 10;
        opponentHealthSlider.value = opponentHealth;
        opponentHPNumbers.text = opponentHealth + "/100";
        StartCoroutine(onAttack());

        if (opponentHealth<=0){
            dialogueText.text = "YOU WIN!";
            SceneManager.LoadScene("Traveling");
        }
        opponentTurn();
    }
       IEnumerator onAttack(){
            dialogueText.text = "You use attack!";

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueText.text = opponentPokemonName + " takes 10 damage!";

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

    void abilityTwo(){
        DestroyAllButtons();

        if (opponentHealth<=30){
            StartCoroutine(executed());
        }else{
            StartCoroutine(ineffective());
        }

        if (opponentHealth<=0){
            StartCoroutine(winn());
            SceneManager.LoadScene("Traveling");
        }
        if (opponentHealth>0){
            opponentTurn();
        }
    }
        IEnumerator winn(){
            dialogueText.text = "YOU WIN!";
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));


        }
        IEnumerator executed(){
            dialogueText.text = "You use swallow!";

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueText.text = "Swallow was effective!";
            opponentHealth = 0;
            opponentHPNumbers.text = opponentHealth + "/100";
            opponentHealthSlider.value = opponentHealth;

            playerHealth = playerHealth + 20;
            playerHealthSlider.value = playerHealth;
            playerHPNumbers.text = playerHealth + "/100";
        
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
//THE ISSUE OCCURS HERE.
            dialogueText.text = opponentPokemonName + " takes fatal damage!";

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        IEnumerator ineffective(){
            dialogueText.text = "You use swallow!";
        
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueText.text = "Swallow was ineffective!";
            opponentHealth = opponentHealth - 5;
            opponentHPNumbers.text = opponentHealth + "/100";
            opponentHealthSlider.value = opponentHealth;

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueText.text = opponentPokemonName + " takes 5 damage!";

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
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
        itemOneTransform.anchoredPosition = new Vector2(startX, startY + (1 * spacingY));
        itemOneButton.transform.SetAsLastSibling();

        itemOneButton.onClick.AddListener(itemOne);

        //Item Two Button
        Button itemTwoButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        itemTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = "Item Two";

        RectTransform itemTwoTransform = itemTwoButton.GetComponent<RectTransform>();
        itemTwoTransform.anchoredPosition = new Vector2(startX, startY + (2 * spacingY));
        itemTwoButton.transform.SetAsLastSibling();

        itemTwoButton.onClick.AddListener(itemTwo);

    }

    void itemOne(){
        DestroyAllButtons();
        dialogueText.text = "You use item one!";

        if (opponentHealth<=0){
            dialogueText.text = "YOU WIN!";
            SceneManager.LoadScene("Traveling");
        }
        opponentTurn();   
    }

    void itemTwo(){
        DestroyAllButtons();
        dialogueText.text = "You use item two!";

        if (opponentHealth<=0){
            dialogueText.text = "YOU WIN!";
            SceneManager.LoadScene("Traveling");
        }
        opponentTurn();   
    }

    void opponentTurn(){
        StartCoroutine(opponentTurnSequence());

    }
    IEnumerator opponentTurnSequence(){
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        dialogueText.text = "It is the opponent's turn";
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        if (opponentEnergy>=100){
            dialogueText.text = "The opponent uses ultimate!";
            opponentEnergy = opponentEnergy -100;
            opponentEnergySlider.value = opponentEnergy;
            playerHealth = playerHealth - 25;
            playerHealthSlider.value = playerHealth;
            playerHPNumbers.text = playerHealth + "/100";
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = "White Blood Cell takes 25 damage!";

        }else{
            dialogueText.text = "The opponent uses attack!";
            playerHealth = playerHealth - 10;
            playerHealthSlider.value = playerHealth;
            playerHPNumbers.text = playerHealth + "/100";
            opponentEnergy = opponentEnergy + 10;
            opponentEnergySlider.value = opponentEnergy;

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueText.text = "White blood cell takes 10 damage!";
        }
        if(playerHealth<=0){
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = "GAME OVER!";
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            SceneManager.LoadScene("Main Menu");
            }

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yourTurn();
    }
}
//Help T-T