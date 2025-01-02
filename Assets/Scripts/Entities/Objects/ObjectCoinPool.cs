using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCoinPool : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectPrefab;
    private Stack<GameObject> objects;

    private void Awake()
    {
        this.objects = new Stack<GameObject>();
    }

    public GameObject GetObject()
    {
        if (this.objects.Count > 0)
        {
            GameObject obj = this.objects.Pop();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(this.ObjectPrefab);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        this.objects.Push(obj);
    }
}
