using UnityEngine;
using UnityEngine.SceneManagement;

namespace FredericRP.ProjectTemplate
{
  public class TransitionToScene : LoadScene
  {
    bool transitionInProgress;

    public override void StartLoading()
    {
      transitionInProgress = false;
      base.StartLoading();
      if (async && AsyncOperation != null)
      {
        AsyncOperation.allowSceneActivation = false;
      }
    }

    private void Update()
    {
      // Unity Inside Hack : operation is locked at 0.9 until allowSceneActivation is set to true,
      // so we must monitor this value to smoothly launch the transition, then allow the scene to be activated
      if (AsyncOperation != null && AsyncOperation.progress == 0.9f && !transitionInProgress)
      {
        Debug.Log(Time.time + ":" + gameObject.name + " > AsynOperation Progress is 0.9, launch transition");
        LaunchTransition();
      }
    }

    void LaunchTransition()
    {
      transitionInProgress = true;
      // Once scene has been loaded, show the transition
      Transition.Transition.Show();
    }

    /// <summary>
    /// Allow scene activation, must be called when transition has been shown
    /// </summary>
    public void AllowSceneActivation()
    {
      Debug.Log(Time.time + ":" + gameObject.name + " > Activation !");
      transitionInProgress = false;
      AsyncOperation.allowSceneActivation = true;
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      base.OnSceneLoaded(scene, mode);
      // Everything is ready, hide the transition !
      Transition.Transition.Hide();
    }
  }
}