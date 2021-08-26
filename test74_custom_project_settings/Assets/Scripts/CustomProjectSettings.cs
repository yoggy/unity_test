using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class CustomProjectSettings : ScriptableObject
{
    public int intValue;
    public string urlString;

    public static CustomProjectSettings GetInstance()
    {
        var path = "Assets/CustomProjectSettings.asset";
        var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(path);

        if (settings == null)
        {
            settings = CreateInstance<CustomProjectSettings>();
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
        }

        return settings;
    }

    public static SerializedObject GetSerializedObject()
    {
        return new SerializedObject(GetInstance());
    }
}

public class CustomProjectSettingsProvider : SettingsProvider
{
    public CustomProjectSettingsProvider(string path, SettingsScope scope) : base(path, scope)
    {
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        Debug.Log("OnActivate");
    }

    public override void OnDeactivate()
    {
        Debug.Log("OnDeactivate");
    }

    public override void OnGUI(string searchContext)
    {
        var serializedObject = CustomProjectSettings.GetSerializedObject();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("intValue"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("urlString"));
        serializedObject.ApplyModifiedProperties();
    }

    [SettingsProvider]
    private static SettingsProvider Create()
    {
        var path = "Project/Custom/CustomProjectSettings";
        var provider = new CustomProjectSettingsProvider(path, SettingsScope.Project);
        var serializedObject = CustomProjectSettings.GetSerializedObject();
        provider.keywords = GetSearchKeywordsFromSerializedObject(serializedObject);

        return provider;
    }
}
