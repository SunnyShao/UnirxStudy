using UnityEditor;
using UnityEngine;

/// <summary>
/// 由于JsonUtility无法转换字典类型。通过ISerializationCallbackReceiver接口，间接的实现字典序列化
/// </summary>
public class book_08_Json01
{
    [MenuItem("Json/字典转Json以及对应的解析")]
    private static void LoadJsonDictionary()
    {
        //创建字典类 和 对应数据
        SerializableDictionary<int, string> m_serializableDictionary = new SerializableDictionary<int, string>();
        m_serializableDictionary[0] = "000";
        m_serializableDictionary[1] = "111";
        m_serializableDictionary[2] = "222";
        //转换字典类 为 json类型
        string jsonDic = JsonUtility.ToJson(m_serializableDictionary);
        Debug.LogError("生成的Json内容：" + jsonDic);

        //创建字典类 接收 json格式的字典
        SerializableDictionary<int, string> serializableDictionary = JsonUtility.FromJson<SerializableDictionary<int, string>>(jsonDic);
        Debug.Log(serializableDictionary[2]);
    }
}