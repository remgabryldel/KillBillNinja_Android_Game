using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndirectionLogin : MonoBehaviour
{
    [SerializeField] public string playerName;
    
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}

