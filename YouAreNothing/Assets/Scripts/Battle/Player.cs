using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
