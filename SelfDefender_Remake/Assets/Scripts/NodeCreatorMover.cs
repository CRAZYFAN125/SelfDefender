using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreatorMover : MonoBehaviour {

    public float odleglosc = 2f;
    public float maxMove = 8;
    public Transform startObj;
    private Vector3 lastVector;

    public void MoveNode()
    {
        lastVector = startObj.position;
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject item in nodes)
        {
            item.transform.position =lastVector;
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 v = new Vector3(lastVector.x + odleglosc, lastVector.y, lastVector.z);
            nodes[i].transform.position = v;
            if (nodes[i].transform.position.x == maxMove)
            {
                lastVector = new Vector3(startObj.position.x, lastVector.y + odleglosc, startObj.position.z);
            }
            else
            {
                lastVector.x += odleglosc;
            }
            Debug.Log(lastVector);
        }
    }
}
