using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 originalPosition;

    public SerializeablePlayerData playerData; //The scriptable object containing the necessary player data. 

    //Data related to turns and initiative
    public int initiativeNumber; //From 0 to 77: the higher the number, the sooner the player takes their turn. 
    public bool isTurn; //true when it is this players turn, and they can act
    public bool turnOver; //true when the player has already acting, but it is not currently their turn.

    //Images, animators, and other visual objects.
    public Animator playerAnim;
    public Image playerSprite;
    public Text nameText;
    public Image initiativeIMG;
    public Text initiativeText;
    public Image[] conditionIMG = new Image[6];
    public Image[] buffIMG = new Image[6];

    public int playerHealth;
    public int currentHealth;
    public int maxHealth;
    public int resourceType; //0 for mana, 1 for stamina, 2 for rage
    public int playerResource; //The amount of resources the player has. 
    public int currentResource;
    public int maxResource;

    public string playerTarotCard; //Which tarot card the player has: determines what abilities they have available. 
    public int tarotCardType; //0 = Wand, 1 = Coin, 2 = Cup, 3 = Sword, 4 = Major Arcana

    void Awake()
    {
        playerSprite.sprite = playerData.playerSprite;
        nameText.text = playerData.playerName;
        playerHealth = playerData.startingHealth;
        currentHealth = playerHealth; //in the future this will need to be pulled from the appropriate manager
        maxHealth = playerHealth;
        resourceType = playerData.resourceType;
        currentResource = playerResource;
        maxResource = playerResource;
        playerResource = playerData.startingResource;
        playerAnim = GetComponentsInChildren<Animator>()[1];
        if (playerData.playerAnimator != null)
        {
            playerAnim.runtimeAnimatorController = playerData.playerAnimator;
        }
        else
        {
            Debug.Log("Fool animator");
            playerAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerAnimations/Fool/TheFool");
        }
    }

    //Called by BattleManager when the player begins their turn. 
    public void BeginTurn ()
    {
        SetPositionToZeroLocation();
        tarotCardType = DetermineTarotCardType();
    }

    //Sets position to Zero Location, and records the original position.
    public void SetPositionToZeroLocation()
    {
        originalPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(-230, -72, 0);
    }


    //Determines the type of tarot card. 
    public int DetermineTarotCardType ()
    {
        if (playerTarotCard.Contains("Wand"))
        {
            return 0;
        }
        else if (playerTarotCard.Contains("Coin"))
        {
            return 1;
        }
        else if (playerTarotCard.Contains("Cup"))
        {
            return 2;
        }
        else if (playerTarotCard.Contains("Sword"))
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
