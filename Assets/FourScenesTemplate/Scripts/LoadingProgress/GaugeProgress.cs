using FredericRP.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FredericRP.ProjectTemplate
{
  public class GaugeProgress : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField]
    RectTransform gaugeRect = null;
    [SerializeField]
    FloatGameEvent progressEvent = null;
    [Header("Config")]
    [SerializeField]
    float anchorMin = 0.1f;
    [SerializeField]
    float ratio = 1;
    [SerializeField]
    float offset = 0;


    private void OnEnable()
    {
      progressEvent?.Listen<float>(UpdateProgress);
    }

    private void OnDisable()
    {
      progressEvent?.Delete<float>(UpdateProgress);
    }

    // Update is called once per frame
    void UpdateProgress(float progress)
    {
      Vector2 anchorMax = gaugeRect.anchorMax;
      // At least some pixels width
      anchorMax.x = Mathf.Max(progress * ratio + offset, anchorMin);
      gaugeRect.anchorMax = anchorMax;
    }
  }

}