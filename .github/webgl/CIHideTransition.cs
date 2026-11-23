using UnityEngine;
using UnityEngine.SceneManagement;

// Copied into Assets during the CI build only (never committed to the project).
// The GameQuest sample scene played after the menu has no HideTransitionOnStart object,
// so the loading transition would stay on screen: add one when that scene is loaded.
static class CIHideTransition
{
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  static void Register()
  {
    SceneManager.sceneLoaded += (scene, mode) =>
    {
      if (scene.name == "GameQuestExample")
        new GameObject("HideTransitionOnStart (CI)").AddComponent<HideTransitionOnStart>();
    };
  }
}
