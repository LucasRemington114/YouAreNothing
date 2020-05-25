using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //UI elements
    public Text[] testEnemyText;
    public Image whiteScreen;
    public Image[] playerTargetSprite;
    public Animator[] playerTargetAnim;
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

    //Tarot variables
    public int selectedTarotCard; //The tarot card currently selected.
    public bool selectedTarotCardIsMoveable; //True if the selected tarot card is in the moveable state.
    public bool doneButtonSelected; //True if the done button is selected.

    //Move Select UI elements
    public GameObject[] moveSelect1 = new GameObject[3];
    public GameObject[] moveSelect2 = new GameObject[4];
    public GameObject moveDescription;
    Text[] moveSelectText1 = new Text[3];
    Text[] moveSelectText2 = new Text[4];
    Animator[] moveSelectAnim1 = new Animator[3];
    Animator[] moveSelectAnim2 = new Animator[4];
    Image[] moveSelectIMG2 = new Image[4];

    //Move Select variables
    public int selectedMoveButtonRow; //The row of the move button currently selected.
    public int selectedMoveButtonColumn; //The column of the move button currently selected.
    public int moveTypeSelected; // 0 for basic, 1 for special, 2 for run.

    //Temporary stoage variables for moving cards
    string oldTarotCardString;
    Sprite oldTarotCardSprite;

    //Bools
    public bool initativePhaseOver;

    //Ints
    public int currentTurn; //The current turn number. 
    public int playerTakingTurn; //Which player is currently taking a turn.
    public int activeUI; //The type of UI currently active. 
    public int roundNumber; //equal to the number of rounds.

    //Player UI
    public Image[] playerBackgroundImage;
    public Animator[] playerBackgroundImageAnim;

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
        for (int i = 0; i < player.Length; i++)
        {
            playerBackgroundImage[i] = player[i].GetComponentInChildren<Image>();
            playerBackgroundImageAnim[i] = playerBackgroundImage[i].GetComponent<Animator>();
        }
        playerDoneButtonIMG = playerDoneButton.GetComponentInChildren<Image>();
        playerDoneButtonAnim = playerDoneButton.GetComponentInChildren<Animator>();
        playerDoneButtonText = playerDoneButton.GetComponentInChildren<Text>();
        TogglePlayerBackgroundImages();
        RollInitiative();
        StartCoroutine(EasyTestingInputs());
    }

    //Pressing space on a selected tarot card makes it moveable. 
    public IEnumerator EasyTestingInputs()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Y));
        if (Input.GetKeyDown(KeyCode.Y) == true)
        {
            playerDoneButtonAnim.SetTrigger("Vanish");
            TogglePlayerBackgroundImages();
            for (int i = 0; i < player.Length; i++)
            {
                playerTarotAnim[i].SetTrigger("Vanish");
                player[i].playerTarotCard = playerTarotText[i].text;
                player[i].initiativeIMG.sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(player[i].playerTarotCard)];
                player[i].initiativeIMG.enabled = true;
                player[i].initiativeText.text = dt.ReturnTextStringOfTarotCardByString(player[i].playerTarotCard);
                player[i].initiativeNumber = dt.ReturnIdentityIntOfTarotCardByString(player[i].playerTarotCard);
            }
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[0].initiativeNumber = 78;
            }
            initativePhaseOver = true;
            currentTurn = 79;
            DetermineTurn();
        }
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
        for (int i = 0; i < moveSelect1.Length; i++)
        {
            moveSelect1[i].SetActive(false);
            moveSelectText1[i] = moveSelect1[i].GetComponentInChildren<Text>();
            moveSelectAnim1[i] = moveSelect1[i].GetComponentInChildren<Animator>();
        }
        for (int i = 0; i < moveSelect2.Length; i++)
        {
            moveSelect2[i].SetActive(false);
            moveSelectText2[i] = moveSelect2[i].GetComponentInChildren<Text>();
            moveSelectAnim2[i] = moveSelect2[i].GetComponentInChildren<Animator>();
            moveSelectIMG2[i] = moveSelect2[i].GetComponentInChildren<Image>();
        }
        moveDescription.SetActive(false);
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

        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].enemyTarotCard = dt.DrawTarotCardAndReturnString();
            enemy[i].initiativeIMG.sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(enemy[i].enemyTarotCard)];
            enemy[i].initiativeIMG.enabled = true;
            enemy[i].initiativeText.text = dt.ReturnTextStringOfTarotCardByString(enemy[i].enemyTarotCard);
            enemy[i].initiativeText.enabled = true;
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
        TogglePlayerBackgroundImages();
        for (int i = 0; i < player.Length; i++)
        {
            playerTarotAnim[i].SetTrigger("Vanish");
            player[i].playerTarotCard = playerTarotText[i].text;
            player[i].initiativeIMG.sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(player[i].playerTarotCard)];
            player[i].initiativeIMG.enabled = true;
            player[i].initiativeText.text = dt.ReturnTextStringOfTarotCardByString(player[i].playerTarotCard);
            player[i].initiativeNumber = dt.ReturnIdentityIntOfTarotCardByString(player[i].playerTarotCard);
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].initiativeNumber = dt.ReturnIdentityIntOfTarotCardByString(enemy[i].enemyTarotCard);
        }
        initativePhaseOver = true;
        currentTurn = 79;
        DetermineTurn();
    }

    //Toggles all Player Background Images on or off.
    public void TogglePlayerBackgroundImages ()
    {
        if (playerBackgroundImageAnim[0].GetBool("Active") == true)
        {
            for (int i = 0; i < player.Length; i++)
            {
                playerBackgroundImageAnim[i].SetBool("Active", false);
            }
        } else
        {
            for (int i = 0; i < player.Length; i++)
            {
                playerBackgroundImageAnim[i].SetBool("Active", true);
                playerBackgroundImage[i].enabled = true;
            }
        }
    }

    //This probably isn't a great way to do this BUT basically, every living enemy and player has a turn number under 78. 
    //This function cycles through the turn numbers, counting down from 78. If that number matches the iniative number of the player or enemy, it calls the appropriate turn-taking function.
    //Right now this function (and everything else) only cares about players. Enemies 
    public void DetermineTurn ()
    {
        currentTurn--;
        for (int i = 0; i < player.Length; i++)
        {
            if (currentTurn == player[i].initiativeNumber)
            {
                player[i].isTurn = true;
            }
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            if (currentTurn == enemy[i].initiativeNumber)
            {
                enemy[i].isTurn = true;
            }
        }
        if (player[0].isTurn == true)
        {
            StartCoroutine(TakePlayerTurn(0));
        }
        else if (player[1].isTurn == true)
        {
            StartCoroutine(TakePlayerTurn(1));
        }
        else if (player[2].isTurn == true)
        {
            StartCoroutine(TakePlayerTurn(2));
        }
        else if (player[3].isTurn == true)
        {
            StartCoroutine(TakePlayerTurn(3));
        }
        else if (enemy[0].isTurn == true)
        {
            StartCoroutine(enemy[0].TakeEnemyTurn());
        }
        else if (enemy.Length == 2)
        {
            if (enemy[1].isTurn == true)
            {
                StartCoroutine(enemy[1].TakeEnemyTurn());
            }
        }
        else if (enemy.Length == 3)
        {
            if (enemy[2].isTurn == true)
            {
                StartCoroutine(enemy[2].TakeEnemyTurn());
            }
        }
        else if (enemy.Length == 4)
        {
            if (enemy[3].isTurn == true)
            {
                StartCoroutine(enemy[3].TakeEnemyTurn());
            }
        }
        else if (enemy.Length == 5)
        {
            if (enemy[4].isTurn == true)
            {
                StartCoroutine(enemy[4].TakeEnemyTurn());
            }
        }
        else if (enemy.Length == 6)
        {
            if (enemy[5].isTurn == true)
            {
                StartCoroutine(enemy[5].TakeEnemyTurn());
            }
        }
        else
        {
            DetermineTurn();
        }
    }

    //When this function is called, take the turn of the indicated player. 
    public IEnumerator TakePlayerTurn(int playerNumber)
    {
        Debug.Log(currentTurn + " Player turn " + playerNumber);
        playerTakingTurn = playerNumber;
        yield return new WaitUntil(() => playerBackgroundImage[playerTakingTurn].color.a == 0);
        player[playerTakingTurn].BeginTurn();
        playerBackgroundImageAnim[playerTakingTurn].SetBool("Active", true);
        playerBackgroundImage[playerTakingTurn].enabled = true;
        player[playerTakingTurn].initiativeIMG.sprite = dt.tarotCardImages[dt.ReturnImageIntOfTarotCardByString(player[playerTakingTurn].playerTarotCard)];
        player[playerTakingTurn].initiativeIMG.enabled = true;
        player[playerTakingTurn].initiativeText.text = dt.ReturnTextStringOfTarotCardByString(player[playerTakingTurn].playerTarotCard);
        for (int i = 0; i < moveSelect1.Length; i++)
        {
            moveSelect1[i].SetActive(true);
            moveSelectText1[i].enabled = true;
        }
        for (int i = 0; i < moveSelect2.Length; i++)
        {
            moveSelect2[i].SetActive(true);
            moveSelectText2[i].enabled = false;
            moveSelectIMG2[i].enabled = false;
        }
        moveDescription.SetActive(true);
        moveSelectText1[0].text = "Basic Move";
        moveSelectText1[1].text = "Special Move";
        moveSelectText1[2].text = "Run";
        moveSelectAnim1[0].SetBool("Selected", true);
        StartCoroutine(SelectMoveWithArrowKeysVertical());
        StartCoroutine(SelectMoveWithArrowKeysHorizontal());
    }

    //This coroutine is active when players can use their arrow keys to select a move... vertically.
    public IEnumerator SelectMoveWithArrowKeysVertical()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow));
        if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectUpMove();
            yield return new WaitForEndOfFrame();
        }
        if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectDownMove();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(SelectMoveWithArrowKeysVertical());
    }

    //This coroutine is active when players can use their arrow keys and spacebar to assign tarot cards to each player. 
    public IEnumerator SelectMoveWithArrowKeysHorizontal()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow));
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectRightMove();
            yield return new WaitForEndOfFrame();
        }
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectLeftMove();
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(SelectMoveWithArrowKeysHorizontal());
    }

    //Selects the move button above the currently selected one, if possible
    public void SelectUpMove()
    {
        if (selectedMoveButtonRow > 0)
        {
            selectedMoveButtonRow--;
            SelectMoveButtonDependentOnColumn();
        }
    }

    //Selects the move button below the currently selected one, if possible
    public void SelectDownMove()
    {
        if (selectedMoveButtonColumn == 0 & selectedMoveButtonRow < 2)
        {
            selectedMoveButtonRow++;
            SelectMoveButtonDependentOnColumn();
        }
        else if (selectedMoveButtonColumn == 1 & moveTypeSelected == 0 & selectedMoveButtonRow < 2)
        {
            selectedMoveButtonRow++;
            SelectMoveButtonDependentOnColumn();
        }
        else if (selectedMoveButtonColumn == 1 & moveTypeSelected == 1 & selectedMoveButtonRow < 3)
        {
            selectedMoveButtonRow++;
            SelectMoveButtonDependentOnColumn();
        }
    }

    //Selects the move button to the left of the currently selected one, if possible
    public void SelectLeftMove()
    {
        if (selectedMoveButtonColumn == 1)
        {
            selectedMoveButtonColumn--;
            DeactivateMoveButton2();
            ClearAllMoveAnimations();
            selectedMoveButtonRow = 0;
            moveSelectAnim1[0].SetBool("Selected", true);
        }
    }

    //Selects the move button to the right of the currently selected one, if possible
    public void SelectRightMove()
    {
        if (selectedMoveButtonColumn == 0)
        {
            selectedMoveButtonColumn++;
            ActivateMoveButton2();
            ClearAllMoveAnimations();
            moveSelectAnim2[0].SetBool("Selected", true);
            if (selectedMoveButtonRow == 0)
            {
                moveTypeSelected = 0;
                moveSelectText2[0].text = "Basic Attack";
                moveSelectText2[1].text = "Block";
                moveSelectText2[2].text = "Use Item";
                moveSelectText2[3].text = "";
                moveSelectIMG2[3].enabled = false;
            }
            else if (selectedMoveButtonRow == 1 & player[playerTakingTurn].tarotCardType == 0)
            {
                moveTypeSelected = 1;
                moveSelectText2[0].text = player[playerTakingTurn].playerData.Wand1;
                moveSelectText2[1].text = player[playerTakingTurn].playerData.Wand2;
                moveSelectText2[2].text = player[playerTakingTurn].playerData.Wand3;
                moveSelectText2[3].text = player[playerTakingTurn].playerData.Wand4;
                EnableMoveButton2ImagesAndText(4);
            }
            else if (selectedMoveButtonRow == 1 & player[playerTakingTurn].tarotCardType == 1)
            {
                moveTypeSelected = 1;
                moveSelectText2[0].text = player[playerTakingTurn].playerData.Coin1;
                moveSelectText2[1].text = player[playerTakingTurn].playerData.Coin2;
                moveSelectText2[2].text = player[playerTakingTurn].playerData.Coin3;
                moveSelectText2[3].text = player[playerTakingTurn].playerData.Coin4;
                EnableMoveButton2ImagesAndText(4);
            }
            else if (selectedMoveButtonRow == 1 & player[playerTakingTurn].tarotCardType == 2)
            {
                moveTypeSelected = 1;
                moveSelectText2[0].text = player[playerTakingTurn].playerData.Cup1;
                moveSelectText2[1].text = player[playerTakingTurn].playerData.Cup2;
                moveSelectText2[2].text = player[playerTakingTurn].playerData.Cup3;
                moveSelectText2[3].text = player[playerTakingTurn].playerData.Cup4;
                EnableMoveButton2ImagesAndText(4);
            }
            else if (selectedMoveButtonRow == 1 & player[playerTakingTurn].tarotCardType == 3)
            {
                moveTypeSelected = 1;
                moveSelectText2[0].text = player[playerTakingTurn].playerData.Sword1;
                moveSelectText2[1].text = player[playerTakingTurn].playerData.Sword2;
                moveSelectText2[2].text = player[playerTakingTurn].playerData.Sword3;
                moveSelectText2[3].text = player[playerTakingTurn].playerData.Sword4;
                EnableMoveButton2ImagesAndText(4);
            }
            else if (selectedMoveButtonRow == 1 & player[playerTakingTurn].tarotCardType == 4)
            {
                moveTypeSelected = 1;
                moveSelectText2[0].text = "Tarot Move"; //needs revision
                moveSelectText2[1].text = player[playerTakingTurn].playerData.Ultimate1;
                moveSelectText2[2].text = player[playerTakingTurn].playerData.Ultimate1;
                moveSelectText2[3].text = player[playerTakingTurn].playerData.Ultimate1;
                EnableMoveButton2ImagesAndText(4);
            }
            else if (selectedMoveButtonRow == 2)
            {
                moveTypeSelected = 2;
                moveSelectText2[0].text = "Run";
                for (int i = 1; i < 4; i++)
                {
                    moveSelectIMG2[i].enabled = false;
                    moveSelectText2[i].enabled = false;
                }
            }
            selectedMoveButtonRow = 0;
        }
    }

    public void EnableMoveButton2ImagesAndText (int buttonsToEnable)
    {
        for (int i = 0; i < buttonsToEnable; i++)
        {
            moveSelectIMG2[i].enabled = true;
            moveSelectText2[i].enabled = true;
        }
    }

    //Selects the move button appropriate to the currently selected column
    public void SelectMoveButtonDependentOnColumn ()
    {
        ClearAllMoveAnimations();
        if (selectedMoveButtonColumn == 0)
        {
            moveSelectAnim1[selectedMoveButtonRow].SetBool("Selected", true);
        }
        else if (selectedMoveButtonColumn == 1)
        {
            moveSelectAnim2[selectedMoveButtonRow].SetBool("Selected", true);
        }
    }

    //Clears all move button animations. 
    public void ClearAllMoveAnimations()
    {
        foreach (Animator moveSelect in moveSelectAnim1)
        {
            if(moveSelect != null)
            moveSelect.SetBool("Selected", false);
        }
        foreach (Animator moveSelect in moveSelectAnim2)
        {
            if (moveSelect != null)
            moveSelect.SetBool("Selected", false);
        }
    }

    //activates the text and images for the second column of move buttons
    public void ActivateMoveButton2 ()
    {
        for (int i = 0; i < moveSelect2.Length; i++)
        {
            moveSelectText2[i].enabled = true;
            moveSelectIMG2[i].enabled = true;
        }
    }

    //Deactivates the text and images for the second column of move buttons
    public void DeactivateMoveButton2()
    {
        for (int i = 0; i < moveSelect2.Length; i++)
        {
            moveSelectText2[i].enabled = false;
            moveSelectIMG2[i].enabled = false;
        }
    }



}
