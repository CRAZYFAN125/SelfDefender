using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="New Turret Data", menuName ="Crazy/Turret")]
public class InGameTurret : ScriptableObject
{
    public Sprite ArtWork;
    public new string name = "New Turret";
    public GameObject Prefab;
    public int cost = 200;
}
