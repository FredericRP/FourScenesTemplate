using FredericRP.EventManagement;
using UnityEngine;


namespace FredericRP.ProjectTemplate
{
  public class MonitorProgress : MonoBehaviour
  {
    [SerializeField]
    LoadScene sceneLoader = null;
    [SerializeField]
    GameEvent loadingProgressEvent = null;
    [SerializeField]
    float delayToUpdateProgress = 0.2f;

    protected AsyncOperation asyncOperation;
    protected float nextProgressUpdateTime;

    protected int progress;

    private void Start()
    {
      nextProgressUpdateTime = 0;
      progress = 0;
    }

    private void Update()
    {
      if (Time.time > nextProgressUpdateTime)
      {
        if (asyncOperation == null && sceneLoader != null)
        {
          asyncOperation = sceneLoader.AsyncOperation;
        }
        if (asyncOperation != null)
        {
          nextProgressUpdateTime = Time.time + delayToUpdateProgress;
          progress = Mathf.RoundToInt(asyncOperation.progress * 100);
          SendProgressEvent(progress);
        }
      }
    }

    protected void SendProgressEvent(int progress)
    {
      EventHandler.TriggerEvent<int>(loadingProgressEvent, progress);
    }
  }
}