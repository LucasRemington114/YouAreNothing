using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Player", menuName = "Player", order = 1)]
public class SerializeablePlayerData : ScriptableObject
{
    public Sprite playerSprite;
    public AnimatorOverrideController playerAnimator;
    public string playerName;
    public int startingHealth;
    public int resourceType; //0 for mana, 1 for stamina, 2 for rage
    public int startingResource;
    public string Wand1;
    public string Wand2;
    public string Wand3;
    public string Wand4;
    public string Coin1;
    public string Coin2;
    public string Coin3;
    public string Coin4;
    public string Cup1;
    public string Cup2;
    public string Cup3;
    public string Cup4;
    public string Sword1;
    public string Sword2;
    public string Sword3;
    public string Sword4;
    public string Ultimate1;
    public string Ultimate2;
    public string Ultimate3;
}
