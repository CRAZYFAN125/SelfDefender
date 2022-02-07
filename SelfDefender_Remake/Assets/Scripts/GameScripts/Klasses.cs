using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Crazy
{
    [System.Serializable]
    public class NodeData
    {
        public NodeData() { }
        public string nodeName;
        public GameObject build;

        public NodeData(string _nodeName, GameObject _build) { nodeName = _nodeName;build = _build; }
    }

    #region Save
    [System.Serializable]
    public class SaveNode
    {
        public NodeData[] nodeDatas;
    }
    public enum GameType
    {
        Default,
        Hard,
        Lite,
        Modified
    }
    [System.Serializable]
    public class SaveData
    {
        public SaveNode SavedNodes;
        public int money = 0;
        public int wave;
        public GameType gameType;


        public SaveData(GameType _gameType)
        {
            gameType = _gameType;
        }
        public string ConvertData()
        {
            string sData = string.Empty;

            sData = JsonUtility.ToJson(this, false);

            return sData;
        }
    }
    #endregion
}