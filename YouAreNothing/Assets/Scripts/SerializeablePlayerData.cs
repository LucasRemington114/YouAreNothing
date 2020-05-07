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

}
