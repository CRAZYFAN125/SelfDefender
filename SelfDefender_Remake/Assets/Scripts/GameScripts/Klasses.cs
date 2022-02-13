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
        public string build;

        public NodeData(string _nodeName, GameObject _build) { nodeName = _nodeName;build = _build.name; }
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
namespace Crazy.ModSystem
{

    public class TurretModCore
    {
        [Header("Turret")]
        public string turretName = "New Turret";
        public string turretArtPath = "<Path to image>";
        public int turretCost = 200;
        public string turretAmmo = "Default/Rocket/Laser";
        public float turretDamage = 1;
        public float turretRange = 3;
        public float turretFireRate = 1f;
        public bool isTurretRotating = true;
        public Vector3[] TurretFirePoints = { new Vector3(0, 0, 0) };

        //[Header("Bullet")]
        //public string bulletArtPath = "<Path to image>";
        //public bool isBulletRotating = false;

        public static void CreateBundle()
        {
            TurretModCore turret = new TurretModCore();

            string x = JsonUtility.ToJson(turret,true);
            System.IO.File.WriteAllText(Application.dataPath+"/TurretMod.json", x);
        }
    }
}