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
      anchorMax.x = Mathf.Max((float)value/100, anchorMin);
      gaugeRect.anchorMax = anchorMax;
      Debug.Log("SceneLoading " + value + "%");
    }
  }

}