using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Ground Instance;

    void Awake()
    {
        Instance = this;
    }
}
