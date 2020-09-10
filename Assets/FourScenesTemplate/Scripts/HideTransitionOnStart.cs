using FredericRP.ScreenTransitions;
using UnityEngine;

public class HideTransitionOnStart : MonoBehaviour
{
  private void Start()
  {
    Transition.GetTransition().Hide();
  }
}
