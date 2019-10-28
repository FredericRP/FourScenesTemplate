using UnityEngine;
using UnityEngine.UI;

namespace FredericRP.ProjectTemplate
{
  public class RawImageScrolling : MonoBehaviour
  {
    [SerializeField]
    RawImage rawImage = null;
    [SerializeField]
    Vector2 scrollSpeed = Vector2.up;

    Vector2 offset;

    private void Start()
    {
      offset = rawImage.material.mainTextureOffset;
    }
    // Update is called once per frame
    void Update()
    {
      offset += scrollSpeed * Time.deltaTime;
      rawImage.material.mainTextureOffset = offset;
    }
  }

}