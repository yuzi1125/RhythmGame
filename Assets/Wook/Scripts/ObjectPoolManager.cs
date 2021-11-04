using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Note = 0,
}

[System.Serializable]
public class ObjectPool
{
    public ObjectType objectType;
    public List<GameObject> Objects = new List<GameObject>();
    public GameObject Object_Prefab;
}
public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public ObjectPool[] objectPool;

    public static ObjectPoolManager Instance { get { return instance; } }
    public Transform Set;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Setobject();
    }

    void Setobject()
    {
        for (int i = 0; i < objectPool.Length; i++)
        {
            for (int j = 0; j < objectPool[i].Objects.Count; j++)
            {
                objectPool[i].Objects[j] = Instantiate(objectPool[i].Object_Prefab, transform);
                objectPool[i].Objects[j].SetActive(false);
            }

        }

    }

    //사용할 오브젝트 리턴
    public GameObject ReturnObject(ObjectType _object)
    {
        switch (_object)
        {
            case ObjectType.Note:
                {
                    var objects = objectPool[(int)_object].Objects;
                    var findobject = objects.Find(obj => !obj.activeSelf);
                    if (null == findobject)
                    {//모두 사용중이라면 생성
                        GameObject node = Instantiate(objectPool[(int)_object].Object_Prefab, transform);
                        Set.transform.parent = node.transform;
                        objects.Add(node);
                        findobject = objects[objects.Count - 1];
                        findobject.SetActive(false);
                    }
                    return findobject;
                }
        }
        return null; //사용가능한 오브젝트가 없음
    }
}
