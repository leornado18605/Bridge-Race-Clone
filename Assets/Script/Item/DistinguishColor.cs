using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistinguishColor : MonoBehaviour
{
    public Color color;
    public bool isOnStack = false;

    private void OnTriggerEnter(Collider other)
    {
        CollectBricks(other);
    }

    void CollectBricks(Collider other)
    {
        if (other.transform.GetComponent<PlayerScript>().playerColor == color)
        {
            other.transform.GetComponent<StackManager>().Push(gameObject);
            gameObject.GetComponent<Collider>().enabled = false;

            if (other.transform.GetComponent<AIControl>() != null)
            {
                other.transform.GetComponent<AIControl>().RemoveTargetFromList(gameObject);
            }

            isOnStack = true;
        }
    }
}
