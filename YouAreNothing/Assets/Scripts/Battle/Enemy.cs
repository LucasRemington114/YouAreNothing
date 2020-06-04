using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Vector3 originalPosition;
    public Vector3[] battlePosition;
    private Animator targetAnimation;

    public ScriptableEnemy enemyData; //The scriptable object containing the necessary player data. 

    //Other scripts
    public BattleManager bm;
    
    //Data related to turns and initiative
    public int initiativeNumber; //From 0 to 77: the higher the number, the sooner the player takes their turn. 
    public bool isTurn; //true when it is this players turn, and they can act
    public bool turnOver; //true when the player has already acting, but it is not currently their turn.

    //Images, animators, and other visual objects.
    public Animator enemyAnim;
    public Image enemySprite;
    public Text tarotText;
    public Image initiativeIMG;
    public Text initiativeText;
    public Image[] conditionIMG = new Image[6];
    public Image[] buffIMG = new Image[6];

    public int enemyHealth;
    public int currentHealth;
    public int maxHealth;

    public int currentAttackDamage;

    public string enemyTarotCard; //Which tarot card the player has: determines what abilities they have available. 
    public int tarotCardType; //0 = Wand, 1 = Coin, 2 = Cup, 3 = Sword, 4 = Major Arcana

    public List<int> targetPlayer; //which player is currently the target of the attack

    void Awake()
    {
        bm = GameObject.Find("/ManagerHolder/BattleManager").GetComponent<BattleManager>();
        originalPosition = gameObject.transform.localPosition;
        enemySprite.sprite = enemyData.enemySprite;
        tarotText.text = enemyData.enemyName;
        enemyHealth = enemyData.startingHealth;
        currentHealth = enemyHealth; //in the future this will need to be pulled from the appropriate manager
        maxHealth = enemyHealth;
        enemyAnim = GetComponentsInChildren<Animator>()[0];
        if (enemyData.enemyAnimator != null)
        {
            enemyAnim.runtimeAnimatorController = enemyData.enemyAnimator;
        }
    }

    public IEnumerator TakeEnemyTurn ()
    {
        yield return new WaitUntil(() => bm.playerBackgroundImage[0].color.a == 0);
        Debug.Log("Enemy turn");
        bm.TogglePlayerBackgroundImages();
        tarotCardType = DetermineTarotCardType();
        yield return new WaitForSeconds(0.5f);
        switch (tarotCardType)
        {
            case 0:
                if (Random.Range(0, 1) == 0)
                {
                    Invoke(enemyData.Wand1, 0f);
                } else
                {
                    Invoke(enemyData.Wand2, 0f);
                }
                break;
            case 1:
                if (Random.Range(0, 1) == 0)
                {
                    Invoke(enemyData.Coin1, 0f);
                }
                else
                {
                    Invoke(enemyData.Coin2, 0f);
                }
                break;
            case 2:
                if (Random.Range(0, 1) == 0)
                {
                    Invoke(enemyData.Cup1, 0f);
                }
                else
                {
                    Invoke(enemyData.Cup2, 0f);
                }
                break;
            case 3:
                if (Random.Range(0, 1) == 0)
                {
                    Invoke(enemyData.Sword1, 0f);
                }
                else
                {
                    Invoke(enemyData.Sword2, 0f);
                }
                break;
            case 4:
                if (Random.Range(0, 1) == 0)
                {
                    Invoke(enemyData.Ultimate1, 0f);
                }
                else
                {
                    Invoke(enemyData.Ultimate2, 0f);
                }
                break;
        }
    }

    //Determines the type of tarot card. 
    public int DetermineTarotCardType()
    {
        if (enemyTarotCard.Contains("Wand"))
        {
            return 0;
        }
        else if (enemyTarotCard.Contains("Coin"))
        {
            return 1;
        }
        else if (enemyTarotCard.Contains("Cup"))
        {
            return 2;
        }
        else if (enemyTarotCard.Contains("Sword"))
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    //Sets targetPlayet to a number of randomly determined players.
    void TargetRandomPlayers (int numberOfTargets)
    {
        targetPlayer.Clear();
        List<int> values = new List<int>();

        for (int i = 0; i < numberOfTargets; i++)
        {
            targetPlayer.Add(Random.Range(0, bm.player.Length - 1));
            if (values.Contains(targetPlayer[i]))
            {
                TargetRandomPlayers(numberOfTargets);
                break;
            }
            else
            {
                values.Add(targetPlayer[i]);
            }
        }
        Debug.Log(targetPlayer[0]);
    }

    //Sets the animation triggers for attacks.
    void SetAttackTrigger(int number, int type)
    {
        enemyAnim.SetTrigger("SpecialAttack");
        enemyAnim.SetInteger("SpecialAttackNumber", number);
        enemyAnim.SetInteger("SpecialAttackType", type);
    }

    //Called through animation on EnemyAnimationFunction. Stops the attacks. 
    public void StopAttack()
    {
        enemyAnim.SetInteger("SpecialAttackNumber", -1);
        enemyAnim.SetInteger("SpecialAttackType", -1);
    }

    //Sets the enemy sprite to the predetermined battle position, and sets the existing spaces for players to have the correct image and animations for the targeted players. 
    public void SetEnemyAttackBattlePositions (int numberOfTargets)
    {
        for (int i = 0; i < bm.enemy.Length; i++)
        {
            bm.enemy[i].enemySprite.enabled = false;
        }
        enemySprite.enabled = true;
        enemySprite.transform.localPosition = battlePosition[0];
        for (int i = 0; i < numberOfTargets; i++)
        {
            bm.playerTargetSprite[i].enabled = true;
            bm.playerTargetSprite[i].sprite = bm.player[targetPlayer[i]].playerSprite.sprite;
            targetAnimation = bm.playerTargetSprite[i].GetComponent<Animator>();
            targetAnimation.runtimeAnimatorController = bm.player[targetPlayer[i]].playerAnim.runtimeAnimatorController;
        }
    }

    public void DamageTargetPlayers ()
    {
        for (int i = 0; i < targetPlayer.Count; i++)
        {
            bm.player[targetPlayer[i]].DamagePlayer(currentAttackDamage);
        }
    }

    void UseMajorArcana()
    {
        Debug.Log("major arcana");
    }

    void XtremeUppercut ()
    {
        Debug.Log("wand");
        TargetRandomPlayers(1);
        SetEnemyAttackBattlePositions(1);
        currentAttackDamage = 20;
        SetAttackTrigger(0, 0);
    }

    void XStomp()
    {
        Debug.Log("coin");
        TargetRandomPlayers(1);
        SetEnemyAttackBattlePositions(1);
        currentAttackDamage = 20;
        SetAttackTrigger(0, 1);
    }

    void InspiringFireworx()
    {
        Debug.Log("cup");
        TargetRandomPlayers(1);
        SetEnemyAttackBattlePositions(1);
        currentAttackDamage = 20;
        SetAttackTrigger(0, 2);
    }

    void XSlicer()
    {
        Debug.Log("sword");
        TargetRandomPlayers(1);
        SetEnemyAttackBattlePositions(1);
        currentAttackDamage = 20;
        SetAttackTrigger(0, 3);
    }
}
