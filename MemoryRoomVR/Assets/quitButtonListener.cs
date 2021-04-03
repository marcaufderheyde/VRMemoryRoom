using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class quitButtonListener : MonoBehaviour
{
    public XRController controller = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuTarget))
        {
            if (menuTarget)
            {  
                LoadLoadingScreen();
            }
        }
        
    }

    public void LoadLoadingScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
