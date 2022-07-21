using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texture2DSampleLoader : MonoBehaviour
{
  [SerializeField] List<Texture2DSample> samples = new List<Texture2DSample>();
  [SerializeField] Button readBtn = null;

  void Awake()
  {
    readBtn.onClick.AddListener(HandleOnClick);
  }

  private void HandleOnClick()
  {
    samples.ForEach(img => img.Load());
  }
}
