using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTarot : MonoBehaviour
{
    public string[] cardsInTarotDeck = new string[78];
    public List<int> drawnCards;
    public int cardDrawn; //The most recently drawn card by the DrawTarotCard() function. 
    public int cardsRemainingInDeck; //Equal to the number of cards remaining in deck, updated when cards are drawn.

    //Refreshes the deck automatically upon awake.
    private void Awake()
    {
        RefreshDeck();
    }

    //Initializes the string array, and clears the list of drawn cards. 
    public void RefreshDeck()
    {
        Debug.Log("Deck Refreshed");
        drawnCards.Clear();
        cardsRemainingInDeck = 78;
        cardsInTarotDeck[0] = "The Fool";
        cardsInTarotDeck[1] = "The Magician";
        cardsInTarotDeck[2] = "The High Priestess";
        cardsInTarotDeck[3] = "The Empress";
        cardsInTarotDeck[4] = "The Emperor";
        cardsInTarotDeck[5] = "The Hierophant";
        cardsInTarotDeck[6] = "The Lovers";
        cardsInTarotDeck[7] = "The Chariot";
        cardsInTarotDeck[8] = "Strength";
        cardsInTarotDeck[9] = "The Hermit";
        cardsInTarotDeck[10] = "Wheel of Fortune";
        cardsInTarotDeck[11] = "Justice";
        cardsInTarotDeck[12] = "The Hanged Man";
        cardsInTarotDeck[13] = "Death";
        cardsInTarotDeck[14] = "Temperance";
        cardsInTarotDeck[15] = "The Devil";
        cardsInTarotDeck[16] = "The Tower";
        cardsInTarotDeck[17] = "The Star";
        cardsInTarotDeck[18] = "The Moon";
        cardsInTarotDeck[19] = "The Sun";
        cardsInTarotDeck[20] = "Judgement";
        cardsInTarotDeck[21] = "The World";
        cardsInTarotDeck[22] = "Ace of Wands";
        cardsInTarotDeck[23] = "Ace of Coins";
        cardsInTarotDeck[24] = "Ace of Cups";
        cardsInTarotDeck[25] = "Ace of Swords";
        cardsInTarotDeck[26] = "Two of Wands";
        cardsInTarotDeck[27] = "Two of Coins";
        cardsInTarotDeck[28] = "Two of Cups";
        cardsInTarotDeck[29] = "Two of Swords";
        cardsInTarotDeck[30] = "Three of Wands";
        cardsInTarotDeck[31] = "Three of Coins";
        cardsInTarotDeck[32] = "Three of Cups";
        cardsInTarotDeck[33] = "Three of Swords";
        cardsInTarotDeck[34] = "Four of Wands";
        cardsInTarotDeck[35] = "Four of Coins";
        cardsInTarotDeck[36] = "Four of Cups";
        cardsInTarotDeck[37] = "Four of Swords";
        cardsInTarotDeck[38] = "Five of Wands";
        cardsInTarotDeck[39] = "Five of Coins";
        cardsInTarotDeck[40] = "Five of Cups";
        cardsInTarotDeck[41] = "Five of Swords";
        cardsInTarotDeck[42] = "Six of Wands";
        cardsInTarotDeck[43] = "Six of Coins";
        cardsInTarotDeck[44] = "Six of Cups";
        cardsInTarotDeck[45] = "Six of Swords";
        cardsInTarotDeck[46] = "Seven of Wands";
        cardsInTarotDeck[47] = "Seven of Coins";
        cardsInTarotDeck[48] = "Seven of Cups";
        cardsInTarotDeck[49] = "Seven of Swords";
        cardsInTarotDeck[50] = "Eight of Wands";
        cardsInTarotDeck[51] = "Eight of Coins";
        cardsInTarotDeck[52] = "Eight of Cups";
        cardsInTarotDeck[53] = "Eight of Swords";
        cardsInTarotDeck[54] = "Nine of Wands";
        cardsInTarotDeck[55] = "Nine of Coins";
        cardsInTarotDeck[56] = "Nine of Cups";
        cardsInTarotDeck[57] = "Nine of Swords";
        cardsInTarotDeck[58] = "Ten of Wands";
        cardsInTarotDeck[59] = "Ten of Coins";
        cardsInTarotDeck[60] = "Ten of Cups";
        cardsInTarotDeck[61] = "Ten of Swords";
        cardsInTarotDeck[62] = "Page of Wands";
        cardsInTarotDeck[63] = "Page of Coins";
        cardsInTarotDeck[64] = "Page of Cups";
        cardsInTarotDeck[65] = "Page of Swords";
        cardsInTarotDeck[66] = "Knight of Wands";
        cardsInTarotDeck[67] = "Knight of Coins";
        cardsInTarotDeck[68] = "Knight of Cups";
        cardsInTarotDeck[69] = "Knight of Swords";
        cardsInTarotDeck[70] = "Queen of Wands";
        cardsInTarotDeck[71] = "Queen of Coins";
        cardsInTarotDeck[72] = "Queen of Cups";
        cardsInTarotDeck[73] = "Queen of Swords";
        cardsInTarotDeck[74] = "King of Wands";
        cardsInTarotDeck[75] = "King of Coins";
        cardsInTarotDeck[76] = "King of Cups";
        cardsInTarotDeck[77] = "King of Swords";
    }

    //Draws a card by picking a number from the appropriate range and checking it against the list of drawn cards. 
    //If the card is on the list,  it repeats the random number generation: otherwise, it adds the number to the list of drawn cards.
    //Then, it sets cardDrawn to the generated number. 
    public void DrawTarotCard()
    {
        cardDrawn = Random.Range(0, 77);
        bool drawAgain = false;

        if (drawnCards.Count == cardsInTarotDeck.Length - 1)
        {
            RefreshDeck();
            Debug.Log("Deck was auto-refreshed as part of DrawTarotCard(). This should never happen!");
        }

        for (int i = 0; i < drawnCards.Count; i++)
        {
            if (cardDrawn == drawnCards[i])
            {
                drawAgain = true;
            }
        }

        if (drawAgain == true)
        {
            DrawTarotCard();
        }
        else
        {
            drawnCards.Add(cardDrawn);
            cardsRemainingInDeck--;
        }

    }

    //Returns the string of the tarot card drawn.
    public string DrawTarotCardAndReturnString ()
    {
        DrawTarotCard();
        return cardsInTarotDeck[cardDrawn];
    }
}
