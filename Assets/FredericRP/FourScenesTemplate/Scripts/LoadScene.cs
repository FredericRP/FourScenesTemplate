using FredericRP.EventManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FredericRP.ProjectTemplate
{
  public class LoadScene : MonoBehaviour
  {
    /// <summary>
    /// Loading process can be called: by a script, onStart automatically, on specified event
    /// </summary>
    protected enum LoadingTrigger { onCall, onStart, onEvent};

    [SerializeField]
    string sceneName = null;
   
    [SerializeField]
    protected bool async = false;
    /// <summary>
    /// When will be called the loading process ? if onCall, it's explicitly from a script
    /// </summary>
    [SerializeField]    
    protected LoadingTrigger loadingTrigger = LoadingTrigger.onStart;
    [SerializeField]
    protected GameEvent loadingEvent;

    private AsyncOperation asyncOperation;

    public AsyncOperation AsyncOperation { get => asyncOperation; }

    // Start is called before the first frame update
    private void Start()
    {
      if (loadingTrigger == LoadingTrigger.onStart)
        StartLoading();
    }

    private void OnEnable()
    {
      SceneManager.sceneLoaded += OnSceneLoaded;
      if (loadingTrigger == LoadingTrigger.onEvent)
        EventHandler.AddEventListener(loadingEvent, StartLoading);
    }

    void OnDisable()
    {
      SceneManager.sceneLoaded -= OnSceneLoaded;
      if (loadingTrigger == LoadingTrigger.onEvent)
        EventHandler.RemoveEventListener(loadingEvent, StartLoading);
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if (loadingTrigger == LoadingTrigger.onEvent && loadingEvent != null)
        EventHandler.TriggerEvent(loadingEvent);

      Debug.Log(Time.time + ":" + gameObject.name + " > Scene Loaded !");
    }

    public virtual void StartLoading()
    {
      if (async)
      {
        Debug.Log(Time.time + ":" + gameObject.name + " >Loading ASYNC scene " + sceneName);
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
      }
      else
      {
        Debug.Log(Time.time + ":" + gameObject.name + " >Loading scene " + sceneName);
        SceneManager.LoadScene(sceneName);
      }
    }

    
  }
}