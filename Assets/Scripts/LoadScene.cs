using FredericRP.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
  [SerializeField]
  string sceneName;
  [SerializeField]
  bool async = false;

  AsyncOperation asyncOperation;
  public float Progress { get => asyncOperation != null ? asyncOperation.progress : 0; }
  public bool Done { get => asyncOperation != null ? asyncOperation.isDone : true; }

  // Start is called before the first frame update
  void Start()
  {
    if (async)
      asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    else
      SceneManager.LoadScene(sceneName);
  }
}
