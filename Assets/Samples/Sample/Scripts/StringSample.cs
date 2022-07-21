using HGS.FileControl;
using HGS.FileControl.StringExtension;
using UnityEngine;
using UnityEngine.UI;

public class StringSample : MonoBehaviour
{
  [SerializeField] Storage storage;
  [SerializeField] string key;
  [SerializeField] InputField inputField;
  [SerializeField] Button writeBtn;
  [SerializeField] Button readBtn;

  void Awake()
  {
    writeBtn.onClick.AddListener(HandleOnClickBtnWrite);
    readBtn.onClick.AddListener(HandleOnClickBtnRead);
  }

  private void HandleOnClickBtnRead()
  {
    inputField.text = storage.ReadString(key);
  }

  private void HandleOnClickBtnWrite()
  {
    storage.WriteString(key, inputField.text);
  }
}
