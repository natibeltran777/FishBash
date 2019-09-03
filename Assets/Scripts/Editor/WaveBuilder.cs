using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class WaveBuilder : EditorWindow
{
    [MenuItem("Window/UIElements/WaveBuilder")]
    public static void ShowExample()
    {
        WaveBuilder wnd = GetWindow<WaveBuilder>();
        wnd.titleContent = new GUIContent("WaveBuilder");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/WaveBuilder.uxml");
        VisualElement tree = visualTree.CloneTree();
        root.Add(tree);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/WaveBuilder.uss");
        root.styleSheets.Add(styleSheet);
    }
}