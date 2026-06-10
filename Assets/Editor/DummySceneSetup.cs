using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;
using ZUN;
using Newtonsoft.Json.Linq;
using UnityCliConnector;

[UnityCliTool(Name = "setup_chapter1_dummy", Description = "Adds DummyInstaller to the Chapter_1 SceneContext")]
public static class SetupChapter1Dummy
{
    public static object HandleCommand(JObject parameters)
    {
        var sc = Object.FindFirstObjectByType<SceneContext>();
        if (sc == null)
            return "ERROR: SceneContext not found. Make sure Chapter_1 is the active scene.";

        if (sc.gameObject.GetComponent<DummyInstaller>() != null)
            return "SKIP: DummyInstaller already attached to SceneContext.";

        var installer = Undo.AddComponent<DummyInstaller>(sc.gameObject);

        var so = new SerializedObject(sc);
        var prop = so.FindProperty("_monoInstallers");
        prop.InsertArrayElementAtIndex(prop.arraySize);
        prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue = installer;
        so.ApplyModifiedProperties();

        EditorSceneManager.SaveOpenScenes();

        return "OK: DummyInstaller added to SceneContext and Chapter_1 scene saved.";
    }
}
