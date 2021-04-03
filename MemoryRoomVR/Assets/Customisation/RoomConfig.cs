using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConfig : MonoBehaviour
{
    [SerializeField]
    private string configName;
    [SerializeField]
    private GameObject[] objects;



    //take game objects, set them to true. By default, everything is deactivated and Big Momma needs to run at least 
    public string getName()
    {
        return configName;
    }

    public GameObject[] getObjects()
    {
        return objects;
    }
}
