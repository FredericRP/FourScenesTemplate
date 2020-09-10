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
      EventHandler.AddEventListener<int>(progressEvent, UpdateProgress);
    }

    private void OnDisable()
    {
      EventHandler.RemoveEventListener<int>(progressEvent, UpdateProgress);
    }

    // Update is called once per frame
    void UpdateProgress(int value)
    {
      // P0 format displays wrong char, must investigate
      text.text = value.ToString("F0") + " %";
    }
  }
}