using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTarot : MonoBehaviour
{
    public string[] cardsInTarotDeck = new string[78];
    public Sprite[] tarotCardImages = new Sprite[26];
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

    //Returns the identity int (ie card in tarotdeck) by tarot string
    public int ReturnIdentityIntOfTarotCardByString(string TarotCard)
    {
        if (TarotCard.Contains("The Fool"))
        {
            return 0;
        }
        else if (TarotCard.Contains("The Magician"))
        {
            return 1;
        }
        else if (TarotCard.Contains("The High Priestess"))
        {
            return 2;
        }
        else if (TarotCard.Contains("The Empress"))
        {
            return 3;
        }
        else if (TarotCard.Contains("The Emperor"))
        {
            return 4;
        }
        else if (TarotCard.Contains("The Hierophant"))
        {
            return 5;
        }
        else if (TarotCard.Contains("The Lovers"))
        {
            return 6;
        }
        else if (TarotCard.Contains("The Chariot"))
        {
            return 7;
        }
        else if (TarotCard.Contains("Strength"))
        {
            return 8;
        }
        else if (TarotCard.Contains("The Hermit"))
        {
            return 9;
        }
        else if (TarotCard.Contains("Wheel of Fortune"))
        {
            return 10;
        }
        else if (TarotCard.Contains("Justice"))
        {
            return 11;
        }
        else if (TarotCard.Contains("The Hanged Man"))
        {
            return 12;
        }
        else if (TarotCard.Contains("Death"))
        {
            return 13;
        }
        else if (TarotCard.Contains("Temperance"))
        {
            return 14;
        }
        else if (TarotCard.Contains("The Devil"))
        {
            return 15;
        }
        else if (TarotCard.Contains("The Tower"))
        {
            return 16;
        }
        else if (TarotCard.Contains("The Star"))
        {
            return 17;
        }
        else if (TarotCard.Contains("The Moon"))
        {
            return 18;
        }
        else if (TarotCard.Contains("The Sun"))
        {
            return 19;
        }
        else if (TarotCard.Contains("Judgement"))
        {
            return 20;
        }
        else if (TarotCard.Contains("The World"))
        {
            return 21;
        }
        else if (TarotCard.Contains("Ace of Wands"))
        {
            return 22;
        }
        else if (TarotCard.Contains("Ace of Coins"))
        {
            return 23;
        }
        else if (TarotCard.Contains("Ace of Cups"))
        {
            return 24;
        }
        else if (TarotCard.Contains("Ace of Swords"))
        {
            return 25;
        }
        else if (TarotCard.Contains("Two of Wands"))
        {
            return 26;
        }
        else if (TarotCard.Contains("Two of Coins"))
        {
            return 27;
        }
        else if (TarotCard.Contains("Two of Cups"))
        {
            return 28;
        }
        else if (TarotCard.Contains("Two of Swords"))
        {
            return 29;
        }
        else if (TarotCard.Contains("Three of Wands"))
        {
            return 30;
        }
        else if (TarotCard.Contains("Three of Coins"))
        {
            return 31;
        }
        else if (TarotCard.Contains("Three of Cups"))
        {
            return 32;
        }
        else if (TarotCard.Contains("Three of Swords"))
        {
            return 33;
        }
        else if (TarotCard.Contains("Four of Wands"))
        {
            return 34;
        }
        else if (TarotCard.Contains("Four of Coins"))
        {
            return 35;
        }
        else if (TarotCard.Contains("Four of Cups"))
        {
            return 36;
        }
        else if (TarotCard.Contains("Four of Swords"))
        {
            return 37;
        }
        else if (TarotCard.Contains("Five of Wands"))
        {
            return 38;
        }
        else if (TarotCard.Contains("Five of Coins"))
        {
            return 39;
        }
        else if (TarotCard.Contains("Five of Cups"))
        {
            return 40;
        }
        else if (TarotCard.Contains("Five of Swords"))
        {
            return 41;
        }
        else if (TarotCard.Contains("Six of Wands"))
        {
            return 42;
        }
        else if (TarotCard.Contains("Six of Coins"))
        {
            return 43;
        }
        else if (TarotCard.Contains("Six of Cups"))
        {
            return 44;
        }
        else if (TarotCard.Contains("Six of Swords"))
        {
            return 45;
        }
        else if (TarotCard.Contains("Seven of Wands"))
        {
            return 46;
        }
        else if (TarotCard.Contains("Seven of Coins"))
        {
            return 47;
        }
        else if (TarotCard.Contains("Seven of Cups"))
        {
            return 48;
        }
        else if (TarotCard.Contains("Seven of Swords"))
        {
            return 49;
        }
        else if (TarotCard.Contains("Eight of Wands"))
        {
            return 50;
        }
        else if (TarotCard.Contains("Eight of Coins"))
        {
            return 51;
        }
        else if (TarotCard.Contains("Eight of Cups"))
        {
            return 52;
        }
        else if (TarotCard.Contains("Eight of Swords"))
        {
            return 53;
        }
        else if (TarotCard.Contains("Nine of Wands"))
        {
            return 54;
        }
        else if (TarotCard.Contains("Nine of Coins"))
        {
            return 55;
        }
        else if (TarotCard.Contains("Nine of Cups"))
        {
            return 56;
        }
        else if (TarotCard.Contains("Nine of Swords"))
        {
            return 57;
        }
        else if (TarotCard.Contains("Ten of Wands"))
        {
            return 58;
        }
        else if (TarotCard.Contains("Ten of Coins"))
        {
            return 59;
        }
        else if (TarotCard.Contains("Ten of Cups"))
        {
            return 60;
        }
        else if (TarotCard.Contains("Ten of Swords"))
        {
            return 61;
        }
        else if (TarotCard.Contains("Page of Wands"))
        {
            return 62;
        }
        else if (TarotCard.Contains("Page of Coins"))
        {
            return 63;
        }
        else if (TarotCard.Contains("Page of Cups"))
        {
            return 64;
        }
        else if (TarotCard.Contains("Page of Swords"))
        {
            return 65;
        }
        else if (TarotCard.Contains("Knight of Wands"))
        {
            return 66;
        }
        else if (TarotCard.Contains("Knight of Coins"))
        {
            return 67;
        }
        else if (TarotCard.Contains("Knight of Cups"))
        {
            return 68;
        }
        else if (TarotCard.Contains("Knight of Swords"))
        {
            return 69;
        }
        else if (TarotCard.Contains("Queen of Wands"))
        {
            return 70;
        }
        else if (TarotCard.Contains("Queen of Coins"))
        {
            return 71;
        }
        else if (TarotCard.Contains("Queen of Cups"))
        {
            return 72;
        }
        else if (TarotCard.Contains("Queen of Swords"))
        {
            return 73;
        }
        else if (TarotCard.Contains("King of Wands"))
        {
            return 74;
        }
        else if (TarotCard.Contains("King of Coins"))
        {
            return 75;
        }
        else if (TarotCard.Contains("King of Cups"))
        {
            return 76;
        }
        else if (TarotCard.Contains("King of Swords"))
        {
            return 77;
        }
        else
        {
            Debug.Log("This should never happen! ReturnIdentityIntOfTarotCardByString give a non-tarot string");
            return 0;
        }
    }

    //Returns int that can be used to get appropriate image in the tarotCardImages array with input of a string. 
    public int ReturnImageIntOfTarotCardByString (string TarotCard)
    {
        if (TarotCard.Contains("Wands"))
        {
            return 0;
        }
        else if (TarotCard.Contains("Coins"))
        {
            return 1;
        }
        else if (TarotCard.Contains("Cups"))
        {
            return 2;
        }
        else if (TarotCard.Contains("Swords"))
        {
            return 3;
        }
        else if (TarotCard.Contains("Fool"))
        {
            return 4;
        }
        else if (TarotCard.Contains("Magician"))
        {
            return 5;
        }
        else if (TarotCard.Contains("High Priestess"))
        {
            return 6;
        }
        else if (TarotCard.Contains("Empress"))
        {
            return 7;
        }
        else if (TarotCard.Contains("Emperor"))
        {
            return 8;
        }
        else if (TarotCard.Contains("Hierophant"))
        {
            return 9;
        }
        else if (TarotCard.Contains("Lover"))
        {
            return 10;
        }
        else if (TarotCard.Contains("Chariot"))
        {
            return 11;
        }
        else if (TarotCard.Contains("Strength"))
        {
            return 12;
        }
        else if (TarotCard.Contains("Hermit"))
        {
            return 13;
        }
        else if (TarotCard.Contains("Wheel of Fortune"))
        {
            return 14;
        }
        else if (TarotCard.Contains("Justice"))
        {
            return 15;
        }
        else if (TarotCard.Contains("Hanged Man"))
        {
            return 16;
        }
        else if (TarotCard.Contains("Death"))
        {
            return 17;
        }
        else if (TarotCard.Contains("Temperance"))
        {
            return 18;
        }
        else if (TarotCard.Contains("Devil"))
        {
            return 19;
        }
        else if (TarotCard.Contains("Tower"))
        {
            return 20;
        }
        else if (TarotCard.Contains("Star"))
        {
            return 21;
        }
        else if (TarotCard.Contains("Moon"))
        {
            return 22;
        }
        else if (TarotCard.Contains("Sun"))
        {
            return 23;
        }
        else if (TarotCard.Contains("Judgement"))
        {
            return 24;
        }
        else if (TarotCard.Contains("World"))
        {
            return 25;
        }
        else
        {
            Debug.Log("String was not a Tarot!");
            return 0;
        }
    }

    //Returns text to be used in initiative by inputting a tarot string.
    public string ReturnTextStringOfTarotCardByString (string TarotCard)
    {
        if (TarotCard.Contains("Ace of "))
        {
            return "Ac";
        }
        else if (TarotCard.Contains("Two of "))
        {
            return "2";
        }
        else if (TarotCard.Contains("Three of "))
        {
            return "3";
        }
        else if (TarotCard.Contains("Four of "))
        {
            return "4";
        }
        else if (TarotCard.Contains("Five of "))
        {
            return "5";
        }
        else if (TarotCard.Contains("Six of "))
        {
            return "6";
        }
        else if (TarotCard.Contains("Seven of "))
        {
            return "7";
        }
        else if (TarotCard.Contains("Eight of "))
        {
            return "8";
        }
        else if (TarotCard.Contains("Nine of "))
        {
            return "9";
        }
        else if (TarotCard.Contains("Ten of "))
        {
            return "10";
        }
        else if (TarotCard.Contains("Page of "))
        {
            return "Pg";
        }
        else if (TarotCard.Contains("Knight of "))
        {
            return "Kt";
        }
        else if (TarotCard.Contains("Queen of "))
        {
            return "Qn";
        }
        else if (TarotCard.Contains("King of "))
        {
            return "Kg";
        }
        else
        {
            return "";
        }

    }
}
