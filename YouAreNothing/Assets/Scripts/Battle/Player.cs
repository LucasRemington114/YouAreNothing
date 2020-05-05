using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 originalPosition;

    public int initiativeNumber;
    public Image playerSprite;
    public Text nameText;
    public Image initiativeIMG;
    public Text initiativeText;
    public Image[] conditionIMG = new Image[6];
    public Image[] buffIMG = new Image[6];
    public int playerHealth;
    public int resourceType; //0 for mana, 1 for stamina, 2 for rage
    public int playerResource;
    public string playerTarotCard;
    public bool isTurn; //true when it is this players turn, and they can act
    public bool turnOver; //true when the player has already acting, but it is not currently their turn.

    public void SetPositionToZeroLocation()
    {
        originalPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(-230, -72, 0);
    }
}
