using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArrow : MonoBehaviour
{
    //Other scripts
    public BattleManager bm;
    public GameObject arrowHolder;

    private int currentSelected;

    void Awake()
    {
        bm = GameObject.Find("/ManagerHolder/BattleManager").GetComponent<BattleManager>();
        arrowHolder = GameObject.Find("/Canvas/ArrowHolder");
        this.transform.SetParent(arrowHolder.transform);
        this.transform.position = new Vector3(bm.enemy[0].transform.position.x, bm.enemy[0].transform.position.y + 175f, bm.enemy[0].transform.position.z);
        this.transform.localScale = new Vector3(1, 1, 1);
        currentSelected = 0;
        StartCoroutine(ArrowKeySelect());
        StartCoroutine(SpacebarSelect());
        StartCoroutine(ZCancel());
    }

    IEnumerator ArrowKeySelect()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow));
        if (Input.GetKeyDown(KeyCode.LeftArrow) & currentSelected > 0)
        {
            currentSelected++;
            this.transform.position = bm.enemy[currentSelected].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) & currentSelected < bm.enemy.Length -1)
        {
            currentSelected--;
            this.transform.position = bm.enemy[currentSelected].transform.position;
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(ArrowKeySelect());
    }

    IEnumerator SpacebarSelect()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Debug.Log("Selected");
        bm.playerSelectingTarget = false;
    }

    IEnumerator ZCancel()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        Debug.Log("Cancelled");
        bm.playerSelectingTarget = false;
        bm.currentTurn++;
        bm.playerBackgroundImage[bm.playerTakingTurn].color = new Color (1, 1, 1, 0);
        bm.DetermineTurn();
        Destroy(this.gameObject);
    }

}
