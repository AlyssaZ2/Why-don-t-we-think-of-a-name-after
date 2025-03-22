using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class battleSystem : MonoBehaviour

{
    //UI:
    [SerializeField] private GameObject TextPanel; //panel behind the text
    [SerializeField] private TextMeshProUGUI dialogueText; //the text
    [SerializeField] private TextMeshProUGUI playerName; 
    [SerializeField] private Image playerSprite; 
    [SerializeField] private TextMeshProUGUI opponentName;  
    [SerializeField] private Image opponentSprite;

    //Print Health Numbers
    [SerializeField] private TextMeshProUGUI playerHPNumbers;
    [SerializeField] private TextMeshProUGUI opponentHPNumbers;

    //Health and energy sliders
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private int playerHealth = 100;

    [SerializeField] private Slider opponentHealthSlider;
    [SerializeField] private int opponentHealth = 100;
    [SerializeField] private Slider opponentEnergySlider;
    [SerializeField] private int opponentEnergy = 30;

    //Button stuff
    [SerializeField] private Button choiceButtonPrefab; 
    [SerializeField] private Transform disappearsOnEnd; 
    private float startX = 0f;
    private float startY = 100f;
    private float spacingY = -50f;
    private int started = 1;

    //Variables
    [SerializeField] private string PokemonName;
    [SerializeField] private string opponentPokemonName;
    [SerializeField] private string nextScene;
    [SerializeField] private string tryAgainScene;
    public int opponentType = 1;

    private Dictionary<string, Sprite> characterImages; 
    
    // Start is called before the first frame update
    void Start()
    {
        //LOAD IMAGES
        characterImages = new Dictionary<string, Sprite>
        {
            { "redbloodcell", Resources.Load<Sprite>("redbloodcell") },
            { "whiteBloodCell", Resources.Load<Sprite>("whiteBloodCell")},
            { "bacteria", Resources.Load<Sprite>("bacteria")},
            { "strep", Resources.Load<Sprite>("strep")},
            { "staph", Resources.Load<Sprite>("staph")},
            { "ecoli", Resources.Load<Sprite>("ecoli")},
 
        };
        PokemonName = "Neutrophil 2342352346 (Nelly)";
        playerName.text = PokemonName;
        opponentName.text = opponentPokemonName;
        opponentHealthSlider.value = opponentHealth;
        playerHealthSlider.value = playerHealth;
        opponentEnergySlider.value = opponentEnergy;
        playerHPNumbers.text = playerHealth +"/100";
        opponentHPNumbers.text = opponentHealth + "/100";
        playerSprite.sprite = characterImages["whiteBloodCell"];
        opponentSprite.sprite = characterImages["bacteria"];
        StartCoroutine(beginBattle());
    }

        IEnumerator beginBattle(){
            dialogueText.text = "A stray "+ opponentPokemonName + " has approached!!!";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = "Make your move.";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yourTurn();
        }

    void yourTurn(){
        menu();
    }

    //SHOW ABILTIES-ITEMS SELECTION
    void menu(){
        destroyButtons();
        //Abilities button
        Button abilitiesButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        abilitiesButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use Ability";

        RectTransform buttonTransform = abilitiesButton.GetComponent<RectTransform>();
        buttonTransform.anchoredPosition = new Vector2(startX, startY + (1*spacingY));
        abilitiesButton.transform.SetAsLastSibling();

        abilitiesButton.onClick.AddListener(abilities);

        //Items button
        Button itemsButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        itemsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use Item";

        RectTransform itemsTransform = itemsButton.GetComponent<RectTransform>();
        itemsTransform.anchoredPosition = new Vector2(startX, startY + (2 * spacingY));
        itemsButton.transform.SetAsLastSibling();

        itemsButton.onClick.AddListener(items);
    }

    //SHOW ABILITY BUTTONS
    void abilities(){
        destroyButtons();
        //attack One Button
        Button attackButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        attackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Attack";

        RectTransform attackTransform = attackButton.GetComponent<RectTransform>();
        attackTransform.anchoredPosition = new Vector2(startX, startY + (spacingY));
        attackButton.transform.SetAsLastSibling();

        attackButton.onClick.AddListener(attack);

        //Swallow Button
        Button swallowButton = Instantiate(choiceButtonPrefab, disappearsOnEnd);
        swallowButton.GetComponentInChildren<TextMeshProUGUI>().text = "Swallow";

        RectTransform swallowTransform = swallowButton.GetComponent<RectTransform>();
        swallowTransform.anchoredPosition = new Vector2(startX, startY + (2 * spacingY));
        swallowButton.transform.SetAsLastSibling();

        swallowButton.onClick.AddListener(swallow);

    }

    void attack(){
        destroyButtons();
        StartCoroutine(attackCoroutine());

    }

    IEnumerator attackCoroutine(){
        dialogueText.text = PokemonName + " used attack!";
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        dialogueText.text = opponentPokemonName + " takes 10 damage!";
        opponentHealth = opponentHealth -10;
        opponentHealthSlider.value = opponentHealth;
        opponentHPNumbers.text = opponentHealth + "/100";
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        checkOpponentHP();
    }


    void swallow(){
        destroyButtons();
        StartCoroutine(swallowCoroutine());

    }

    IEnumerator swallowCoroutine(){
        dialogueText.text = PokemonName + " used swallow!";
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        if (opponentHealth<=20){
            dialogueText.text = "Swallow was effective!";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = opponentPokemonName + " is executed!";
            opponentHealth = 0;
            opponentHealthSlider.value = opponentHealth;
            opponentHPNumbers.text = opponentHealth + "/100";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            checkOpponentHP();
        }else{
            dialogueText.text = "Swallow was ineffective!";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = opponentPokemonName +" takes 5 damage!";
            opponentHealth = opponentHealth - 5;
            opponentHPNumbers.text = opponentHealth + "/100";
            opponentHealthSlider.value = opponentHealth;
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            checkOpponentHP();
        }
    }

    void items(){
        destroyButtons();
    }

    void checkOpponentHP(){
        if (opponentHealth<=0){
            win();
        }else{
            opponentTurn();
        }

    }

    void opponentTurn(){
        StartCoroutine(opponentTurnCoroutine());
    }

    IEnumerator opponentTurnCoroutine(){
        dialogueText.text = "It's the opponent's turn."; 
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        if (opponentEnergy >= 100){
            dialogueText.text = opponentPokemonName + " uses ULTIMATE!";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = PokemonName + " takes 25 damage!!!";
            opponentEnergy = opponentEnergy -100;
            opponentEnergySlider.value = opponentEnergy;
            playerHealth = playerHealth - 25;
            playerHPNumbers.text = playerHealth + "/100";
            playerHealthSlider.value = playerHealth;
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            checkPlayerHP();
        }else{
            dialogueText.text = opponentPokemonName + " uses attack!";
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogueText.text = PokemonName + " takes 10 damage!";
            playerHealth = playerHealth - 10;
            playerHPNumbers.text = playerHealth + "/100";
            playerHealthSlider.value = playerHealth;
            opponentEnergy = opponentEnergy + 20;
            opponentEnergySlider.value = opponentEnergy;
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            checkPlayerHP();
        }
    }

    void checkPlayerHP(){
        if (playerHealth<=0){
            lose();
        }else{
            yourTurn();
        }
    }

    void win(){
        StartCoroutine(winCoroutine());
    }

    IEnumerator winCoroutine(){
        dialogueText.text = opponentPokemonName +" has taken fatal damage!";
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        SceneManager.LoadScene(nextScene);
    }
    void lose(){
        StartCoroutine(loseCoroutine());
    }

    IEnumerator loseCoroutine(){
        dialogueText.text = PokemonName + " has taken fatal damage...";
        yield return null;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        SceneManager.LoadScene(tryAgainScene);
    }
    void destroyButtons()
    {
        foreach (Transform child in disappearsOnEnd)
        {
            Destroy(child.gameObject);
        }
    }
    // UPDATE IS FOR THE WEAK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
}
