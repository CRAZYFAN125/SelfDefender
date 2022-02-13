using Crazy.ModSystem;
using Crazy.TurretSystem;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ModSystem : MonoBehaviour
{
    [System.Serializable]
    public class Defaults
    {
        public GameObject Bullet;
        public GameObject Rocket;
        public Sprite TurretDefaultSprite;
        public Sprite builded;
    }
    public bool isTest = true;
    public string modsPath = "../Mods";
    public Material MaterialToCopy;
    public Sprite ModSprite;
    public Defaults defaults;
    GameManager gameManager;
    BuildManager buildManager;
    void Start()
    {
        gameManager = GameManager.instance;
        buildManager = BuildManager.instance;
        modsPath = Path.Combine(Application.dataPath, modsPath);

        if (isTest)
        {
            TurretModCore.CreateBundle();
            buildManager.RunMaking();
            return;
        }

        try
        {
            if (!Directory.Exists(modsPath))
            {
                Directory.CreateDirectory(modsPath);
                return;
            }
            string[] modsPaths = FindMods();
            foreach (string modPath in modsPaths)
            {
                MakeTurret(JsonUtility.FromJson<TurretModCore>(File.ReadAllText(modPath + "/TurretMod.json")),modPath);
            }

            buildManager.RunMaking();
        }
        catch (System.Exception error)
        {
            Debug.LogWarning(error.ToString());
        }
    }
    string[] FindMods()
    {
        List<string> mods = new List<string>();
        string[] modsFolders = Directory.GetDirectories(modsPath);
        foreach (string folder in modsFolders)
        {
            if (File.Exists(folder + "/TurretMod.json"))
            {
                mods.Add(folder);
            }
        }
        return mods.ToArray();
    }

    void MakeTurret(TurretModCore turret, string folder)
    {
        GameObject @object = new GameObject(turret.turretName+"_ModedPrefab", new System.Type[] { typeof(Turret), typeof(SpriteRenderer) });
        Turret t = @object.GetComponent<Turret>();
        t.ApplyVectors(turret.TurretFirePoints);
        t.DamageAmount = turret.turretDamage;
        t.range = turret.turretRange;
        t.isRotating = turret.isTurretRotating;
        if (t.isRotating)
        {
            t.partToRotate = t.transform;
        }
        switch (turret.turretAmmo)
        {
            case "Default":
                t.bulletPrefab = defaults.Bullet;
                break;
            case "Laser":
                t.Laser = true;

                break;
            case "Rocket":
                t.bulletPrefab = defaults.Rocket;
                break;
            default:
                t.bulletPrefab = defaults.Bullet;

                break;
        }

        InGameTurret inGameTurret = ScriptableObject.CreateInstance<InGameTurret>();
        Material x = new Material(MaterialToCopy);
        Texture2D texture = GetSpriteFile(folder + turret.turretArtPath);
        if (texture==null)
        {
            x.SetTexture("_MainTex", defaults.TurretDefaultSprite.texture);
        }
        else
        {
        x.SetTexture("_MainTex", texture);

        }
        x.name = turret.turretName;
        SpriteRenderer sRenderer = @object.GetComponent<SpriteRenderer>();
        sRenderer.material = x;
        sRenderer.sortingLayerName = "2nd Layer";
        sRenderer.sortingOrder = 1;
        sRenderer.sprite = defaults.builded;
        inGameTurret.Prefab = @object;
        @object.SetActive(false);


        buildManager.turrets.Add(inGameTurret);
    }

    Texture2D GetSpriteFile(string Path)
    {
        try
        {
            byte[] pictureData = File.ReadAllBytes(Path);
            Texture2D texture = new Texture2D(128, 128);
            texture.LoadImage(pictureData);
            return texture;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.ToString());
            return null;
        }
    }
}
