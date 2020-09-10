using FredericRP.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FredericRP.ProjectTemplate
{
  public class GaugeProgress : MonoBehaviour
  {
    [SerializeField]
    IntGameEvent progressEvent = null;
    [SerializeField]
    RectTransform gaugeRect = null;
    [SerializeField]
    float anchorMin = 0.1f;
    [SerializeField]
    [Tooltip("Change the speed used to lerp from previous value to new value")]
    float fillSpeed = 20;

    private void OnEnable()
    {
      EventHandler.AddEventListener<int>(progressEvent, UpdateProgress);
    }

    private void OnDisable()
    {
      EventHandler.RemoveEventListener<int>(progressEvent, UpdateProgress);
    }


    // Update is called once per frame
    void UpdateProgress(int value)
    {
      Vector2 anchorMax = gaugeRect.anchorMax;
      // At least some pixels width
      anchorMax.x = Mathf.Lerp(anchorMax.x, Mathf.Max((float)value/100, anchorMin), fillSpeed * Time.deltaTime);
      gaugeRect.anchorMax = anchorMax;
      //Debug.Log("SceneLoading " + value + "%");
    }
  }

}