using FredericRP.EventManagement;
using TMPro;
using UnityEngine;

namespace FredericRP.ProjectTemplate
{
  public class DisplayProgress : MonoBehaviour
  {
    [SerializeField]
    FloatGameEvent progressEvent = null;
    [SerializeField]
    TextMeshProUGUI text = null;
    [SerializeField]
    float ratio = 1;
    [SerializeField]
    float offset = 0;

    private void OnEnable()
    {
      progressEvent.Listen<float>(UpdateProgress);
    }

    private void OnDisable()
    {
      progressEvent.Delete<float>(UpdateProgress);
    }

    // Update is called once per frame
    void UpdateProgress(float progress)
    {
      // P0 format displays wrong char, must investigate
      text.text = Mathf.RoundToInt(100 * (progress * ratio + offset)).ToString("F0") + " %";
    }
  }
}