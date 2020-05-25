using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationFunctions : MonoBehaviour
{

    public BattleManager bm;
    public Enemy en;

    public void Awake()
    {
        bm = GameObject.Find("/ManagerHolder/BattleManager").GetComponent<BattleManager>();
        en = GetComponentInParent<Enemy>();
    }

    //These functions are intended to be called through animation, and change the opacity of the white screen overlay.
    public void whiteScreenOn()
    {
        bm.whiteScreen.color = new Color(255, 255, 255, 100);
    }

    public void whiteScreenOff()
    {
        bm.whiteScreen.color = new Color(255, 255, 255, 0);
    }

    //These functions are used to control the 'block window' of each attack. Controlled through animation.
    public void blockOn()
    {

    }

    public void blockOff()
    {
        Debug.Log("Block over");
        en.DamageTargetPlayers();
    }

    //This function ends the turn. Controlled through animation.
    public void StopAttacking ()
    {
        en.StopAttack();
    }
}
