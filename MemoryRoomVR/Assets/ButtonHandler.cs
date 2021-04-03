using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // This are the UI objects where input will be displayed
    public Text myText;         // drag your text object on here
    public Text debug;          // drag your text object on here
    public Slider loadingBar;   // drag your text object on here
    public RawImage error;      // drag your text object on here
    public RawImage errorMsg;   // drag your text object on here
    public RawImage successMsg; // drag your text object on here

    // Start is called before the first frame update
    void Start()
    {
        // Hide UI elements at start of scene load
        loadingBar.gameObject.SetActive(false);
        error.gameObject.SetActive(false);
        errorMsg.gameObject.SetActive(false);
        successMsg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This adds a number to the text item above if it has less than 5 digits
    public void ClickNumber(string numberClicked)
    {
        string tempCurString = myText.text;

        // Check it's less than 5 digits
        if(tempCurString.Length <= 5)
        {        
            string tempNewString = tempCurString + numberClicked;
            myText.text = tempNewString;
        }
    }

    // This removes a number from the text item above if there are numbers to remove
    public void ClickBackspace()
    {
        string tempGetString = myText.text;

        // Check there are numbers to remove
        if(tempGetString.Length > 0)
        {
            string tempString = tempGetString.Substring(0, tempGetString.Length -1);
            myText.text = tempString;
        }
    }
	

    // This function checks what key has been submitted and loads the corresponding memory room
    public void LoadMemoryRoom()
    {
        string tempGetString = myText.text;
        if(String.Equals(tempGetString, "100"))
        {
            successMsg.gameObject.SetActive(true);
            error.gameObject.SetActive(false);
            StartCoroutine(LoadYourAsyncScene("Room_v1"));
        }

        else if(String.Equals(tempGetString, "200"))
        {
            successMsg.gameObject.SetActive(true);
            error.gameObject.SetActive(false);
            StartCoroutine(LoadYourAsyncScene("Room_v2"));
        }
        else if(String.Equals(tempGetString, "300"))
        {
            successMsg.gameObject.SetActive(true);
            error.gameObject.SetActive(false);
            StartCoroutine(LoadYourAsyncScene("Room_v6"));
        }
        else
        {
            successMsg.gameObject.SetActive(false);
            error.gameObject.SetActive(true);
            errorMsg.gameObject.SetActive(true);
        }
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        loadingBar.value = 0f;
        loadingBar.gameObject.SetActive(true);
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

}
