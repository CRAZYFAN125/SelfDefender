using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Crazy;

public class Node : MonoBehaviour
{
    #region ColorProp
    [ColorUsage(true)]public Color hoverColor;
    private Color startColor;
    private Renderer rendererek;
    #endregion

    private GameObject currentTurret;
    private BuildManager bManager;

    private void Start()
    {
        rendererek = GetComponent<Renderer>();
        startColor=rendererek.material.color;
        bManager = BuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if (GameManager.instance.isShopOpen == true)
        {
            return;
        }
        if (currentTurret==null&& bManager.GetTurretToBuild() != null)
        {
            rendererek.material.color = hoverColor;
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.instance.isShopOpen == true)
        {
            return;
        }

        if (bManager.GetTurretToBuild() == null)
        {
            return;
        }

        if (currentTurret != null)
        {
            Debug.Log("Can't do it!");
            return;
        }
        if (GameManager.instance.MoneyCount-bManager.turretToBuild.cost<0)
        {
            return;
        }
       currentTurret= Instantiate(bManager.GetTurretToBuild(), transform.position, Quaternion.identity, transform);
        GameManager.instance.MoneyCount -= bManager.turretToBuild.cost;
    }
    private void OnMouseExit()
    {
        rendererek.material.color = startColor;
    }

    #region Save/Load methods
    public NodeData GetNodeData()
    {
        if (currentTurret != null)
        {
            return new NodeData(gameObject.name, currentTurret);
        }
        else
        {
            return null;
        }
    }
    public void SetNodeData(NodeData data)
    {
        currentTurret = GetObject(data.build);
        if (currentTurret != null)
        {
            Instantiate(currentTurret, transform.position, Quaternion.identity, transform);
        }
    }
    GameObject GetObject(string tName)
    {
        foreach (InGameTurret item in BuildManager.instance.turrets)
        {
            if (item.Prefab.gameObject.name + "(Clone)" == tName)
            {
                return item.Prefab;
            }
        }
        return null;
    }
    #endregion
}
