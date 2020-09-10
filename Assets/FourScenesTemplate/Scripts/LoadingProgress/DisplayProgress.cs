using FredericRP.EventManagement;
using TMPro;
using UnityEngine;

namespace FredericRP.ProjectTemplate
{
  public class DisplayProgress : MonoBehaviour
  {
    [SerializeField]
    IntGameEvent progressEvent = null;
    [SerializeField]
    TextMeshProUGUI text = null;

    private void OnEnable()
    {
      progressEvent.Listen<int>(UpdateProgress);
    }

    private void OnDisable()
    {
      progressEvent.Delete<int>(UpdateProgress);
    }

    // Update is called once per frame
    void UpdateProgress(int value)
    {
      // P0 format displays wrong char, must investigate
      text.text = value.ToString("F0") + " %";
    }
  }
}