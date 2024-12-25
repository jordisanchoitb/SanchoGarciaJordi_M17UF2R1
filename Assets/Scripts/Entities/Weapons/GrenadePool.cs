using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePool : MonoBehaviour
{
    [SerializeField]
    private GameObject grenadePrefab;
    private Stack<GameObject> grenadesPool;

    private void Awake()
    {
        grenadesPool = new Stack<GameObject>();
    }

    private void CreateGrenade()
    {
        GameObject grenadeObject = Instantiate(grenadePrefab);
        grenadeObject.SetActive(false);
        grenadesPool.Push(grenadeObject);
    }

    public GameObject GetGrenade()
    {
        if (grenadesPool.Count > 0)
        {
            GameObject grenade = grenadesPool.Pop();
            grenade.SetActive(true);
            return grenade;
        }
        else
        {
            // Expandir el pool si está vacío
            CreateGrenade();
            return GetGrenade();
        }
    }

    public void ReturnGrenade(GameObject grenade)
    {
        grenade.SetActive(false);
        grenadesPool.Push(grenade);
    }
}
