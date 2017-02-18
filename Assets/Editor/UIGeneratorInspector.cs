using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircularTextureGenerator))]
public class UIGeneratorInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TextureGenerator myScript = (TextureGenerator)target;

        if (GUILayout.Button("Rebuild"))
        {
            myScript.Rebuild();
        }

        if (GUILayout.Button("Save Texture"))
        {
            myScript.SaveTexture();
        }
    }
}
