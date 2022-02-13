using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crazy;
using System.IO;

public class SaveSystemCore : MonoBehaviour
{
    public static SaveSystemCore instance;
    void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Save();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }
    public void Load()
    {
        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "/save0.json"));

        if (save.wave > 1)
        {
            FindObjectOfType<WaveSpawner>().waveIndex = save.wave-1;
        }
        else
        {
            FindObjectOfType<WaveSpawner>().waveIndex = 1;
        }

        GameManager.instance.MoneyCount = save.money;
        Node[] nodes = FindObjectsOfType<Node>();
        NodeData[] saveNodes = save.SavedNodes.nodeDatas;
        foreach (Node node in nodes)
        {
            foreach (NodeData nodeData in saveNodes)
            {
                if (node.gameObject.name==nodeData.nodeName)
                {
                    node.SetNodeData(nodeData);
                }
            }
        }
    }
    public void Save()
    {
        SaveData save = new SaveData(GameType.Default);
        save.money = GameManager.instance.MoneyCount;
        save.wave = FindObjectOfType<WaveSpawner>().waveIndex;
        save.SavedNodes = new SaveNode();
        Node[] nodes = FindObjectsOfType<Node>();
        List<NodeData> saveNodes = new List<NodeData>();
        for (int i = 0; i < nodes.Length; i++)
        {
            NodeData nodeData = nodes[i].GetNodeData();
            if (nodeData!=null)
            {
                saveNodes.Add(nodeData);
            }
        }
        save.SavedNodes.nodeDatas = saveNodes.ToArray();
        string saveData = JsonUtility.ToJson(save,true);
        print(saveData);
        File.WriteAllText(Application.dataPath + "/save0.json", saveData);
    }
}
