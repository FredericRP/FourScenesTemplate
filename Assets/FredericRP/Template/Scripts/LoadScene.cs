using FredericRP.EventManagement;
using System.Collections;
using System.Collections.Generic;
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
    GameEvent loadingProgressEvent = null;
    [SerializeField]
    protected bool async = false;
    /// <summary>
    /// When will be called the loading process ? if onCall, it's explicitly from a script
    /// </summary>
    [SerializeField]    
    protected LoadingTrigger loadingTrigger = LoadingTrigger.onStart;
    [SerializeField]
    protected GameEvent loadingEvent;

    [SerializeField]
    float delayToUpdateProgress = 0.2f;

    protected AsyncOperation asyncOperation;
    protected float nextProgressUpdateTime;

    protected int progress;

    // Start is called before the first frame update
    private void Start()
    {
      nextProgressUpdateTime = 0;
      progress = 0;
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

    protected void SendProgressEvent(int progress)
    {
      EventHandler.TriggerEvent<int>(loadingProgressEvent, progress);
    }

    private void Update()
    {
      if (Time.time > nextProgressUpdateTime && asyncOperation!=null)
      {
        nextProgressUpdateTime = Time.time + delayToUpdateProgress;
        progress = Mathf.RoundToInt(asyncOperation.progress * 100);
        SendProgressEvent(progress);
      }
    }
  }
}