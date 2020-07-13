using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public static HouseManager instance;

    // Start is called before the first frame update
    void Start()
    {
       instance = this; 
    }
}
