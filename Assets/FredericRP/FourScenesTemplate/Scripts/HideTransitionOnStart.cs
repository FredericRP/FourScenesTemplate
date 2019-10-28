using FredericRP.Transition;
using UnityEngine;

public class HideTransitionOnStart : MonoBehaviour
{
  private void Start()
  {
    Transition.Hide();
  }
}
