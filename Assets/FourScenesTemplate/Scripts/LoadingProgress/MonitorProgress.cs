using FredericRP.EventManagement;
using UnityEngine;

namespace FredericRP.ProjectTemplate
{
  public class MonitorProgress : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField]
    LoadScene sceneLoader = null;
    [SerializeField]
    FloatGameEvent loadingProgressEvent = null;
    [Header("Update delay")]
    [SerializeField]
    float delayToUpdateProgress = 0.2f;

    protected AsyncOperation asyncOperation;
    protected float nextProgressUpdateTime;
    protected float progress;

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
          progress = asyncOperation.progress;
          // Force 100% progress when 90% is reached as Unity does not go higher on async loading
          if (progress >= 0.9f)
            progress = 1;
          SendProgressEvent(progress);
        }
      }
    }

    protected void SendProgressEvent(float progress)
    {
      loadingProgressEvent.Raise(progress);
    }
  }
}