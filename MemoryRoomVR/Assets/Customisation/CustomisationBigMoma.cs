using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationBigMoma : MonoBehaviour
{


    [SerializeField]
    private RoomConfig[] roomConfigs;
    [SerializeField]
    private RoomConfig activeConfig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            changeRoom("Hell");
        }
    }

    public void changeRoom(string roomName)
    {
        for (int i = 0; i < roomConfigs.Length; i++)
        {
            //Once I find a matching name, load the list after deactivating all previous objects.
            if(roomName == roomConfigs[i].getName())
            {
                killTheLastRoom();
                activateRoom(roomConfigs[i]);
                setActiveConfig(roomConfigs[i]);
                i = roomConfigs.Length;

            }
        }

    }

    public void killTheLastRoom()
    {
        GameObject[] objs = activeConfig.getObjects();
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(false);
        }
    }
    public void activateRoom(RoomConfig r)
    {
        GameObject[] robjs = r.getObjects();
        for(int i = 0; i < robjs.Length; i++)
        {
            robjs[i].SetActive(true);
        }
    }


    public RoomConfig getActiveConfig()
    {
        return activeConfig;
    }
    public void setActiveConfig(RoomConfig r)
    {
        activeConfig = r;
    }
}

