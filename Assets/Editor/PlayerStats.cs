using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class StatsWindow : EditorWindow
{
    public int tabs = 3;
    string[] tabOptions = new string[] { "Player", "Stats", "Inventory" };

    string objectPath = "Assets/Objects/Objectstats.asset";

    string playerName = "";
    string description = "";

    int strength = 0;
    int intelligence = 0;
    int dexterity = 0;
    int vitailty = 0;
    int resistance = 0;
    int faith = 0;

    int health = 100;
    int mana = 100;


    [MenuItem("Window/ Player Stats")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StatsWindow));
    }


    private void OnGUI()
    {
        tabs = GUILayout.Toolbar(tabs, tabOptions);

        switch (tabs)
        {
            case 0:
                Player();
                break;

            case 1:
                Stats();
                break;

            case 2:
                Inventory();
                break;
        }

    }

    private void Player()
    {
        GUILayout.Label("Player Name and Description", EditorStyles.boldLabel);

        playerName = EditorGUILayout.TextField("Player Name", playerName);
        description = EditorGUILayout.TextField("Player Description", description);

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Next Tab"))
        {
            tabs = 1;
        }
    }

    private void Stats()
    {
        GUILayout.Label("Player Health and Mana", EditorStyles.boldLabel);

        health = EditorGUILayout.IntSlider("Player Health", health, 100, 2000);
        mana = EditorGUILayout.IntSlider("Player Mana", mana, 100, 2000);

        EditorGUILayout.Space();
        GUILayout.Label("Player Stats", EditorStyles.boldLabel);

        vitailty = EditorGUILayout.IntSlider("Vitailty", vitailty, 0, 100);
        resistance = EditorGUILayout.IntSlider("Resistance", resistance, 0, 100);
        strength = EditorGUILayout.IntSlider("Strength", strength, 0, 100);
        dexterity = EditorGUILayout.IntSlider("Dexterity", dexterity, 0, 100);
        intelligence = EditorGUILayout.IntSlider("Intelligence", intelligence, 0, 100);
        faith = EditorGUILayout.IntSlider("Faith", faith, 0, 100);



        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Next Tab"))
        {
            tabs = 2;
        }
    }

    private void Inventory()
    {
        GUILayout.Label("Player Inventory Page");
        ValidInputs();
        if (ValidInputs() == false)
        {
            GUILayout.Label("You need a Player name and Description");
        }
        else if (GUILayout.Button("Generate Stats"))
        {
            if (ValidInputs() == true)
                GenerateObject();
        }

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Next Tab"))
        {
            tabs = 0;
        }
    }


    private bool ValidInputs()
    {
        if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("You Must enter a name and Description");
            return false;
        }

        Debug.Log("Name and Description added");
        return true;

    }

    private void GenerateObject()
    {
        ObjectStats statsOb = CreateInstance<ObjectStats>();

        statsOb.health = health;
        statsOb.mana = mana;

        statsOb.vitailty = vitailty;
        statsOb.resistance = resistance;
        statsOb.strength = strength;
        statsOb.dexterity = dexterity;
        statsOb.intelligence = intelligence;
        statsOb.faith = faith;

        statsOb.playerName = playerName;
        statsOb.description = description;

        string fileName = AssetDatabase.GenerateUniqueAssetPath(objectPath);

        AssetDatabase.CreateAsset(statsOb, fileName);
        AssetDatabase.SaveAssets();

    }
    
    static void CloseWindow()
    {
        StatsWindow closeWindow = (StatsWindow)EditorWindow.GetWindowWithRect(typeof(StatsWindow), new Rect(100, 100, 400, 400));

        closeWindow.saveChangesMessage = "This Window has unsaved changes. Would you like to save?";
        closeWindow.Show();
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
