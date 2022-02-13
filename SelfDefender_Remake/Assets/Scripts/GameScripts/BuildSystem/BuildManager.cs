using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standartTurretPrefab;
    [HideInInspector]public InGameTurret turretToBuild;
    #region Shop Menu Creator
    [Header("Shop:")]
    public Transform ShopMenu;
    public MenuPlaner MenuPrefab;
    public List<InGameTurret> turrets;

    private void Start()
    {
        foreach (InGameTurret item in turrets)
        {
            GameObject g = Instantiate(MenuPrefab.gameObject, ShopMenu.transform);
            g.GetComponent<MenuPlaner>().InitializeIt(item);
            g.SetActive(true);
        }
    }

    #endregion
    #region Main
    public GameObject GetTurretToBuild()
    {
        if (turretToBuild != null && turretToBuild.Prefab!=null)
        {
            return turretToBuild.Prefab;
        }
        else
        {
            return null;
        }
    }

    void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }
    public void SetTurretToBuild(InGameTurret turret)
    {
        turretToBuild = turret;
    }
    #endregion
}