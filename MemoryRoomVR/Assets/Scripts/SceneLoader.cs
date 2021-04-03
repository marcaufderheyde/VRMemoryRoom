using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
  public UnityEvent OnLoadBegin = new UnityEvent();
  public UnityEvent OnLoadEnd = new UnityEvent();
  public ScreenFader ScreenFader = null;

  private bool isLoading = false;

    private void Awake()
    {
      SceneManager.sceneLoaded += SetActiveScene;
    }

    private void OnDestroy()
    {
      SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadNewScene(string sceneName)
    {
      if(!isLoading)
      {
        StartCoroutine(LoadScene(sceneName));
      }
    }

    private IEnumerator LoadScene(string sceneName)
    {
      isLoading = true;

      OnLoadBegin?.Invoke();
      yield return ScreenFader.StartFadeIn();
      yield return StartCoroutine(UnloadCurrent());

      // For testing
      // yield return new WaitForSeconds(6.0f);

      yield return StartCoroutine(LoadNew(sceneName));
      yield return ScreenFader.StartFadeOut();
      OnLoadEnd?.Invoke();

      isLoading = false;
    }

    private IEnumerator UnloadCurrent()
    {
      AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

      while(!unloadOperation.isDone)
        yield return null;
    }

    private IEnumerator LoadNew(string sceneName)
    {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      while(!loadOperation.isDone)
        yield return null;
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
      SceneManager.SetActiveScene(scene);
    }
}

