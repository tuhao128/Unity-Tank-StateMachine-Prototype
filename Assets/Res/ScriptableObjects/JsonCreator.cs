using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

[CreateAssetMenu(fileName = "jsonCreator", menuName = "Custom Creator/Json Data")]
public class JsonCreator : ScriptableObject
{
    public StateClipConfig config = new StateClipConfig();

    [ContextMenu("CreateJson")]
    public void CreateJson()
    {
        string json = JsonUtility.ToJson(config);
        string path = Path.Combine(Application.persistentDataPath, config.name + ".json");
        File.WriteAllText(path, json);
        Debug.Log("项目已保存在：" + path);
    }
}
