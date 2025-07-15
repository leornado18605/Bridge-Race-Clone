using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    [SerializeField] private GameObject stackPoint;
    [SerializeField] private float stackYIncreaseRate = 0.32f;
    [SerializeField] private float stackXposition = -0.18f;

    [SerializeField] List<GameObject> bricks = new List<GameObject>();

    //Ad brick into the stack
    public void Push(GameObject collectedBrick)
    {
        bricks.Add(collectedBrick);
        MoveToStackAnim(collectedBrick);
    }

    public void Pop()
    {
        var objref = bricks[bricks.Count - 1].gameObject;
        bricks[bricks.Count - 1].gameObject.SetActive(false);
        bricks.RemoveAt(bricks.Count - 1);
        Destroy(objref);

        var playerScript = GetComponent<PlayerScript>();
        //spawn 1 more different brick
        StartCoroutine(playerScript.currentlyStandingFloor.GetComponent<BrickSpawner>().SpawnItemsAtWill(1, playerScript.playerColorIndex));
    }

    //check brick 
    public bool isPopable()
    {
        if (bricks.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void MoveToStackAnim(GameObject brick)
    {
        Vector3 targetPosition;

        if (bricks.Count == 1)
        {
            targetPosition = new Vector3(stackXposition, stackPoint.transform.localPosition.y, 0);
        }
        else
        {
            targetPosition = new Vector3(stackXposition, (bricks.Count - 1) * stackYIncreaseRate, 0);
        }

        brick.transform.parent = stackPoint.transform;
        brick.transform.DOLocalMove(targetPosition, 0.2f);
        brick.transform.rotation = stackPoint.transform.rotation;
    }

}
