using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeCreatorMover))]
public class MoverUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NodeCreatorMover move = (NodeCreatorMover)target;
        if (GUILayout.Button("Move All"))
        {
            move.MoveNode();
        }
    }
}
