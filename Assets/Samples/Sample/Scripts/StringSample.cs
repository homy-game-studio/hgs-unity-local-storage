using HGS.LocalStorage;
using HGS.LocalStorage.StringExtension;
using UnityEngine;
using UnityEngine.UI;

public class StringSample : MonoBehaviour
{
  [SerializeField] Storage storage;
  [SerializeField] string key;
  [SerializeField] InputField inputField;
  [SerializeField] Button writeBtn;
  [SerializeField] Button readBtn;
  [SerializeField] Button clearBtn;

  void Awake()
  {
    writeBtn.onClick.AddListener(HandleOnClickBtnWrite);
    readBtn.onClick.AddListener(HandleOnClickBtnRead);
    clearBtn.onClick.AddListener(HandleOnClickBtnClear);
  }

  private void HandleOnClickBtnClear()
  {
    storage.Clear();
    inputField.text = "";
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
