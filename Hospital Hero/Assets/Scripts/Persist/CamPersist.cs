using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPersist : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<CamPersist>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
