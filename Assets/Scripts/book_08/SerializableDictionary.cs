using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 由于JsonUtility无法转换字典类型。通过ISerializationCallbackReceiver接口，间接的实现字典序列化
/// </summary>
public class SerializableDictionary<K, V> : ISerializationCallbackReceiver
{
    //定义 key的集合
    [SerializeField]
    private List<K> m_keys;

    //定义 value的集合
    [SerializeField]
    private List<V> m_values;

    //定义泛性字典
    private Dictionary<K, V> m_Dictionary = new Dictionary<K, V>();

    //字典 存取值
    public V this[K key]
    {
        get
        {
            if (m_Dictionary.ContainsKey(key))
            {
                return m_Dictionary[key];
            }
            else
            {
                Debug.LogError(default);
                return default;
            }
        }
        set
        {
            m_Dictionary[key] = value;
        }
    }

    //解析json  转换为字典
    public void OnAfterDeserialize()
    {
        int length = m_keys.Count;
        m_Dictionary = new Dictionary<K, V>();
        for (int i = 0; i < length; i++)
        {
            Debug.LogFormat("Key = {0} , Value = {1}", m_keys[i], m_values[i]);
            m_Dictionary.Add(m_keys[i], m_values[i]);
        }
        m_keys = null;
        m_values = null;
    }

    //字典类转换为json
    public void OnBeforeSerialize()
    {
        m_keys = new List<K>();
        m_values = new List<V>();

        //测试 for循环 遍历字典方式
        for (int i = 0; i < m_Dictionary.Count; i++)
        {
            var item = m_Dictionary.ElementAt(i);
            Debug.LogError(item.Key);
            Debug.LogError(item.Value);
        }

        foreach (var item in m_Dictionary)
        {
            Debug.LogFormat("Key = {0} , Value = {1}", item.Key, item.Value);
            m_keys.Add(item.Key);
            m_values.Add(item.Value);
        }
    }
}