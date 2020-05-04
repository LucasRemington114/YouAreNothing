using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //UI elements
    public Text[] testEnemyText;
    //public GameObject[] playerUIHolder = new GameObject[4];

    //Tarot UI elements
    public GameObject[] playerTarotCard = new GameObject[4];
    public Image[] playerTarotIcon = new Image[4];
    Animator[] playerTarotAnim = new Animator[4];
    Text[] playerTarotText = new Text[4];
    public GameObject playerDoneButton;
    Image playerDoneButtonIMG;
    Animator playerDoneButtonAnim;
    Text playerDoneButtonText;
    public int selectedTarotCard; //The tarot card currently selected.
    public bool selectedTarotCardIsMoveable; //True if the selected tarot card is in the moveable state.
    public bool doneButtonSelected; //True if the done button is selected.

    //Temporary stoage variables for moving cards
    string oldTarotCardString;
    Sprite oldTarotCardSprite;

    //Bools
    public bool initativePhaseOver;

    public int activeUI; //The type of UI currently active. 
    public int roundNumber; //equal to the number of rounds.

    //Other scripts
    public DrawTarot dt;
    public Player[] player;
    public Enemy[] enemy;

    //Called whenever battlemanager becomes active.
    void Awake()
    {
        for (int i = 0; i < playerTarotCard.Length; i++)
        {
            playerTarotIcon[i] = playerTarotCard[i].GetComponentsInChildren<Image>()[1];
            playerTarotAnim[i] = playerTarotCard[i].GetComponent<Animator>();
            playerTarotText[i] = playerTarotCard[i].GetComponentInChildren<Text>();
        }
        playerDoneButtonIMG = playerDoneButton.GetComponentInChildren<Image>();
        playerDoneButtonAnim = playerDoneButton.GetComponentInChildren<Animator>();
        playerDoneButtonText = playerDoneButton.GetComponentInChildren<Text>();
        RollInitiative();
    }

    //Called at the beginning of each turn.
    public void RollInitiative()
    {
        if (roundNumber > 0)
        {
            playerDoneButtonAnim.SetTrigger("Vanish");
            for (int i = 0; i < player.Length; i++)
            {
                Image playerTarotCardParentImage;
                playerTarotCardParentImage = playerTarotCard[i].GetComponent<Image>();
                playerTarotCardParentImage.enabled = true;
                playerTarotAnim[i].SetTrigger("Vanish");
            }
        }
        roundNumber++;
        initativePhaseOver = false;
        if (testEnemyText.Length + 4 > dt.cardsRemainingInDeck)
        {
            dt.RefreshDeck();
        }

        for (int i = 0; i < 4; i++)
        {
            playerTarotText[i].text = dt.DrawTarotCardAndReturnString();
            playerTarotIcon[i].sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(playerTarotText[i].text)];
        }

        for (int i = 0; i < testEnemyText.Length; i++)
        {
            testEnemyText[i].text = dt.DrawTarotCardAndReturnString();
        }
        PlayerSelectInitiative();
    }

    //Called after cards have been drawn, allows player to assign tarot cards to each character. 
    public void PlayerSelectInitiative()
    {
        selectedTarotCard = 0;
        ClearAllTarotAnimations();
        playerTarotAnim[0].SetBool("Selected", true);
        StartCoroutine(SelectTarotWithArrowKeys());
        StartCoroutine(MakeTarotCardMoveable());
        StartCoroutine(SelectDoneButtonWithArrowKeys());
        StartCoroutine(DoneWithTarot());
    }

    //Pressing space on a selected tarot card makes it moveable. 
    public IEnumerator SelectDoneButtonWithArrowKeys()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow));
        if (selectedTarotCardIsMoveable == false)
        {
            ClearAllTarotAnimations();
            playerDoneButtonAnim.SetBool("Selected", true);
            doneButtonSelected = true;
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow) | initativePhaseOver == true);
            if (initativePhaseOver == false)
            {
                playerDoneButtonAnim.SetBool("Selected", false);
                doneButtonSelected = false;
                playerTarotAnim[selectedTarotCard].SetBool("Selected", true);
            }
        }
        yield return new WaitForEndOfFrame();
        if (initativePhaseOver == false)
        {
            StartCoroutine(SelectDoneButtonWithArrowKeys());
        }
    }

    //This coroutine is active when players can use their arrow keys and spacebar to assign tarot cards to each player. 
    public IEnumerator SelectTarotWithArrowKeys()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow) | initativePhaseOver == true);
        if (initativePhaseOver == false)
        {
            if (selectedTarotCardIsMoveable == false & doneButtonSelected == false)
            {
                if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SelectLeftTarotCard();
                }
                else if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SelectRightTarotCard();
                }
            }
            else if (doneButtonSelected == false)
            {
                if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveSelectedTarotCardLeft();
                }
                else if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveSelectedTarotCardRight();
                }
            }
            StartCoroutine(SelectTarotWithArrowKeys());
        }
    }

    //Pressing space on a selected tarot card makes it moveable. 
    public IEnumerator MakeTarotCardMoveable()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) & doneButtonSelected == false | initativePhaseOver == true);
        if (initativePhaseOver == false)
        {
            selectedTarotCardIsMoveable = true;
            playerTarotAnim[selectedTarotCard].SetBool("Moveable", true);
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            selectedTarotCardIsMoveable = false;
            playerTarotAnim[selectedTarotCard].SetBool("Moveable", false);
            yield return new WaitForEndOfFrame();
            StartCoroutine(MakeTarotCardMoveable());
        }
    }

    //Selects the tarot card to the left of the currently selected one. 
    public void SelectLeftTarotCard()
    {
        if (selectedTarotCard > 0)
        {
            selectedTarotCard--;
            ClearAllTarotAnimations();
            playerTarotAnim[selectedTarotCard].SetBool("Selected", true);
        }
    }

    //Selects the tarot card to the right of the currently selected one. 
    public void SelectRightTarotCard()
    {
        if (selectedTarotCard < 3)
        {
            selectedTarotCard++;
            ClearAllTarotAnimations();
            playerTarotAnim[selectedTarotCard].SetBool("Selected", true);
        }
    }

    //Moves the currently selected card to the left.
    public void MoveSelectedTarotCardLeft()
    {
        if (selectedTarotCard > 0)
        {
            oldTarotCardString = playerTarotText[selectedTarotCard - 1].text;
            playerTarotText[selectedTarotCard - 1].text = playerTarotText[selectedTarotCard].text;
            playerTarotText[selectedTarotCard].text = oldTarotCardString;
            oldTarotCardSprite = playerTarotIcon[selectedTarotCard - 1].sprite;
            playerTarotIcon[selectedTarotCard - 1].sprite = playerTarotIcon[selectedTarotCard].sprite;
            playerTarotIcon[selectedTarotCard].sprite = oldTarotCardSprite;
            selectedTarotCard--;
            ClearAllTarotAnimations();
            playerTarotAnim[selectedTarotCard].SetBool("Selected", true);
            playerTarotAnim[selectedTarotCard].SetBool("Moveable", true);
        }
    }

    //Moves the currently selected card to the right.
    public void MoveSelectedTarotCardRight()
    {
        if (selectedTarotCard < 3)
        {
            oldTarotCardString = playerTarotText[selectedTarotCard + 1].text;
            playerTarotText[selectedTarotCard + 1].text = playerTarotText[selectedTarotCard].text;
            playerTarotText[selectedTarotCard].text = oldTarotCardString;
            oldTarotCardSprite = playerTarotIcon[selectedTarotCard + 1].sprite;
            playerTarotIcon[selectedTarotCard + 1].sprite = playerTarotIcon[selectedTarotCard].sprite;
            playerTarotIcon[selectedTarotCard].sprite = oldTarotCardSprite;
            selectedTarotCard++;
            ClearAllTarotAnimations();
            playerTarotAnim[selectedTarotCard].SetBool("Selected", true);
            playerTarotAnim[selectedTarotCard].SetBool("Moveable", true);
        }
    }

    //Clears all tarot card animations. 
    public void ClearAllTarotAnimations()
    {
        for (int i = 0; i < playerTarotAnim.Length; i++)
        {
            playerTarotAnim[i].SetBool("Selected", false);
            playerTarotAnim[i].SetBool("Moveable", false);
        }
    }

    //Pressing space when the Done button is selected sets initiative for each player, and moves onto turns. 
    public IEnumerator DoneWithTarot()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) & doneButtonSelected == true);

        playerDoneButtonAnim.SetTrigger("Vanish");
        for (int i = 0; i < player.Length; i++)
        {
            playerTarotAnim[i].SetTrigger("Vanish");
            player[i].playerTarotCard = playerTarotText[i].text;
            player[i].initiativeIMG.sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(player[i].playerTarotCard)];
            player[i].initiativeIMG.enabled = true;
            player[i].initiativeText.text = dt.ReturnTextStringOfTarotCardByString(player[i].playerTarotCard);
            player[i].initiativeNumber = dt.ReturnIdentityIntOfTarotCardByString(player[i].playerTarotCard);
            initativePhaseOver = true;
        }

    }

}
