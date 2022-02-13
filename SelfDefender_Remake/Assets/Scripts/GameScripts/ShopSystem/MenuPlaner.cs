using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MenuPlaner : MonoBehaviour
{
    public Image Art;
    public Button button;
    public Text price;

    public void InitializeIt(InGameTurret turret)
    {
        Art.sprite = turret.ArtWork;
        price.text = turret.cost+" Credits";
        button.onClick.AddListener(() => BuildManager.instance.SetTurretToBuild(turret));
    }
}
