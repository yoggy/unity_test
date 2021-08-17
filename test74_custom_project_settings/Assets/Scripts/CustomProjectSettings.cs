using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class CustomProjectSettings : ScriptableObject
{
    public int intValue;
    public string urlString;

    public static SerializedObject GetInstance()
    {
        var path = "Assets/CustomProjectSettings.asset";
        var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(path);

        if (settings == null)
        {
            settings = CreateInstance<CustomProjectSettings>();
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
        }

        return new SerializedObject(settings);
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
        var settings = CustomProjectSettings.GetInstance();

        EditorGUILayout.PropertyField(settings.FindProperty("intValue"));
        EditorGUILayout.PropertyField(settings.FindProperty("urlString"));
        settings.ApplyModifiedProperties();
    }

    [SettingsProvider]
    private static SettingsProvider Create()
    {
        var path = "Project/Custom/CustomProjectSettings";
        var provider = new CustomProjectSettingsProvider(path, SettingsScope.Project);
        var settings = CustomProjectSettings.GetInstance();
        provider.keywords = GetSearchKeywordsFromSerializedObject(settings);

        return provider;
    }
}
