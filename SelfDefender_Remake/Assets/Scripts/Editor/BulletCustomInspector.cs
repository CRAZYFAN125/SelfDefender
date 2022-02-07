using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bullet))]
public class BulletCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Bullet bullet = (Bullet)target;
        if (bullet.bulletType == Bullet.BulletType.Rocket)
        {
            bullet.boomRadius = EditorGUILayout.FloatField("Explotion Radius",bullet.boomRadius);
        }
    }
}
