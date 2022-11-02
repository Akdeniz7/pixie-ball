using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

public class CopyButtonWindow : EditorWindow
{

    [MenuItem("Tools/Copy Button")]
    static void Init()
    {
        searchForButtons();
        CopyButtonWindow window = (CopyButtonWindow)EditorWindow.GetWindow(typeof(CopyButtonWindow), false, "Copy Button");
        window.minSize = new Vector2(300, 400);
        window.Show();
    }

    public static List<Button> buttons; // lsit of buttons in the scene
    static Object targetbutton; // copy from this gameObjec's component
    public static bool[] includeButton;


    Vector2 scrollVect = Vector2.zero;
    void OnGUI()
    {

        if (buttons == null)
            searchForButtons();

        targetbutton = EditorGUILayout.ObjectField("Copy from Button", targetbutton, typeof(Button), true);

        if (targetbutton == null)
            EditorGUILayout.HelpBox("Select a Button to copy from.", MessageType.Warning);

        GUILayout.Height(20);

        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        selectedType = EditorGUILayout.Popup(selectedType, types.ToArray(), EditorStyles.toolbarDropDown);

        if (GUILayout.Button("Select All", EditorStyles.toolbarButton))
        {
            selection(true);
        }
        if (GUILayout.Button("Unselect All", EditorStyles.toolbarButton))
        {
            selection(false);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        scrollVect = EditorGUILayout.BeginScrollView(scrollVect, GUILayout.Height(200));

        bool canCopy = false;
        for (int x = 0; x < buttons.Count; x++)
        {
            if ((Button)targetbutton == buttons[x] || (selectedType != 0 && !(types[selectedType] == buttons[x].GetType().ToString())))
                continue;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(buttons[x].name, EditorStyles.toolbarButton))
            {
                EditorGUIUtility.PingObject(buttons[x].gameObject);
                Selection.activeGameObject = buttons[x].gameObject;
            }
            includeButton[x] = EditorGUILayout.Toggle(includeButton[x], GUILayout.Width(15));
            if (includeButton[x])
                canCopy = true;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Last check buttons count: " + buttons.Count);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Search for Buttons"))
        {
            searchForButtons();
        }
        EditorGUILayout.EndHorizontal();


        GUILayout.FlexibleSpace();

        GUI.enabled = buttons.Count > 0 && targetbutton != null && canCopy;
        if (GUILayout.Button("Copy"))
        {

            SerializedObject obj = new SerializedObject(targetbutton);

            bool copied = false;
            for (int x = 0; x < buttons.Count; x++)
            {
                if (!includeButton[x] || ((Button)targetbutton == buttons[x] || (selectedType != 0 && !(types[selectedType] == buttons[x].GetType().ToString()))))
                    continue;
                SerializedObject btnObj = new SerializedObject(buttons[x]);
                if (btnObj != null)
                {
                    SerializedProperty prop = btnObj.GetIterator();
                    while (prop.NextVisible(true))
                    {
                        if (prop.name != "m_Script" && prop.name.StartsWith("m_"))
                        {
                            btnObj.CopyFromSerializedProperty(obj.FindProperty(prop.propertyPath));
                        }
                    }
                    prop.Reset();
                }
                btnObj.ApplyModifiedProperties();

                copied = true;

            }

            if (copied)
                EditorUtility.DisplayDialog("Success", "The selected button component has been copied successfully to the selected Button(s).", "ok");

        }
        GUILayout.Space(20);
    }


    void selection(bool active)
    {
        for (int x = 0; x < includeButton.Length; x++)
            includeButton[x] = active;
    }

    int selectedType;
    static List<string> types;
    static void searchForButtons()
    {
        buttons = new List<Button>(GameObject.FindObjectsOfType<Button>());
        types = new List<string> { "ALL" };
        foreach (Button btn in buttons)
        {
            string type = btn.GetType().ToString();
            if (!types.Contains(btn.GetType().ToString()))
                types.Add(type);
        }
        includeButton = new bool[buttons.Count];
    }



}
