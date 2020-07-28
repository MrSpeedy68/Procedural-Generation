using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if(DrawDefaultInspector()) //If any value was changed in the editor then generate a map
        {
            if(mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if(GUILayout.Button ("Generate")) //If this button is pressed run the method to generate a map
        {
            mapGen.GenerateMap();
        }
    }
}
