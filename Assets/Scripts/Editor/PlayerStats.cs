using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerStats : MonoBehaviour
{
    public int Intellgence;
}


public class StatsWindow : EditorWindow
{
    string myString = "HelloWorld";
    bool groupEnabled;
    bool myBool = true;
    int myInt = 1;
    float myFloat = 1.23f;

    [MenuItem ("Window/ Player Stats")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StatsWindow));
    }

    static void CloseWindow()
    {
        StatsWindow closeWindow = (StatsWindow)EditorWindow.GetWindowWithRect(typeof(StatsWindow), new Rect(100, 100, 400, 400));

        closeWindow.saveChangesMessage = "This Window has unsaved changes. Would you like to save?";
        closeWindow.Show();
    }

    private void OnGUI()
    {
        saveChangesMessage = EditorGUILayout.TextField(saveChangesMessage);

        EditorGUILayout.LabelField(hasUnsavedChanges ? "I have changes!" : "No changes.", EditorStyles.wordWrappedLabel);
        EditorGUILayout.LabelField("Try to close the Window.");


            GUILayout.Label("Stats", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Description", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optinonal Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("ON/OFF", myBool);
            myFloat = EditorGUILayout.Slider("Player Intelligence", myFloat, -3, 3);
            myInt = EditorGUILayout.IntSlider ("Player Health", myInt, -3, 3);
        EditorGUILayout.EndToggleGroup();
        

        using (new EditorGUI.DisabledScope(!hasUnsavedChanges))
        {
            if (GUILayout.Button("Save"))
                SaveChanges();

            if (GUILayout.Button("Discard"))
                DiscardChanges();

        }
    }

    public override void SaveChanges()
    {
        Debug.Log($"{this} saved successfully");
        base.SaveChanges();
    }

    public override void DiscardChanges()
    {
        Debug.Log($"{this} discarded changes");
        base.DiscardChanges();
    }
}
