using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 1)]
public class ScriptableEnemy : ScriptableObject
{
    public Sprite enemySprite;
    public AnimatorOverrideController enemyAnimator;
    public string enemyName;
    public int startingHealth;
    public string Wand1;
    public string Wand2;
    public string Coin1;
    public string Coin2;
    public string Cup1;
    public string Cup2;
    public string Sword1;
    public string Sword2;
    public string Ultimate1;
    public string Ultimate2;
}