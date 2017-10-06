using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        MapGenerator mapGen = (MapGenerator)target;
        if (DrawDefaultInspector()) //if any value was changed?
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
