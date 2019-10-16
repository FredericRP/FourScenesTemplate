using TMPro;
using UnityEngine;

public class DisplayProgress : MonoBehaviour
{
  [SerializeField]
  LoadScene sceneLoader;
  [SerializeField]
  TextMeshProUGUI text;

  // Update is called once per frame
  void Update()
  {
    // P0 format displays wrong char, must investigate
    text.text = (100*sceneLoader.Progress).ToString("F0")+ " %";
  }
}
