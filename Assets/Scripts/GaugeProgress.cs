using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeProgress : MonoBehaviour
{
  [SerializeField]
  LoadScene sceneLoader;
  [SerializeField]
  RectTransform gaugeRect;

  // Update is called once per frame
  void Update()
  {
    Vector2 anchorMax = gaugeRect.anchorMax;
    anchorMax.x = sceneLoader.Progress;
    gaugeRect.anchorMax = anchorMax;
  }
}
