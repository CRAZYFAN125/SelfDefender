using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }
    private BuildManager buildManager;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectTurret(InGameTurret turret)
    {
        buildManager.SetTurretToBuild(turret);
    }

}
