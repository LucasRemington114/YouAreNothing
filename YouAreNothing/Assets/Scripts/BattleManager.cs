using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //UI elements
    public Text[] testPlayerText;
    public Text[] testEnemyText;

    //Other scripts
    public DrawTarot dt;

    void Awake()
    {
        RollInitiative();
    }

    public void RollInitiative()
    {
        if(testEnemyText.Length+4 > dt.cardsRemainingInDeck)
        {
            dt.RefreshDeck();
        }

        for (int i = 0; i < 4; i++)
        {
            testPlayerText[i].text = dt.DrawTarotCardAndReturnString();
        }

        for (int i = 0; i < testEnemyText.Length; i++)
        {
            testEnemyText[i].text = dt.DrawTarotCardAndReturnString();
        }
    }
}
