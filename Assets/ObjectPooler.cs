using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;  //usado para poder chamar métodos deste script em outros scripts sem precisar refêrenciar com GetComponent();
    public List<GameObject> pooledUpgrade1;
    public GameObject objUpgrade1;
    public int amountUpgrade1;

    private void Awake()
    {
        SharedInstance = this;

        pooledUpgrade1 = new List<GameObject>();
        for (int i = 0; i < amountUpgrade1; i++)
        {
            GameObject obj = Instantiate(objUpgrade1);
            obj.SetActive(false);
            pooledUpgrade1.Add(obj);
        }
    }

    #region  ---Getters---
    public GameObject GetParticleUpgrade()
    {
        for (int i = 0; i < pooledUpgrade1.Count; i++)
        {
            if (!pooledUpgrade1[i].activeInHierarchy)
            {
                return pooledUpgrade1[i];
            }
        }
        return null;
    }

    #endregion
}