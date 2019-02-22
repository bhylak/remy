using System.Timers;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BigKahuna))]
public class BigKahunaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        BigKahuna kahuna = (BigKahuna)target;
        if(GUILayout.Button("Toggle Setup Mode"))
        {
            kahuna.ToggleSetup();
        }
        
        if(GUILayout.Button("Say \"10 Seconds\""))
        {
            kahuna.speechRecognizer.recognizedText = "10 Seconds";
            kahuna.speechRecognizer.finalized = true;
        }
    }
}