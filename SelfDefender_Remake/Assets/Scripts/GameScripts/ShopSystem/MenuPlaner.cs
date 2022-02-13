using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MenuPlaner : MonoBehaviour
{
    public Image Art;
    public Button button;
    public Text price;
    public Text TurretName;

    public void InitializeIt(InGameTurret turret)
    {
        Art.sprite = turret.ArtWork;
        TurretName.text = turret.name;
        price.text = turret.cost+" Credits";
        button.onClick.AddListener(() => BuildManager.instance.SetTurretToBuild(turret));
    }
}
