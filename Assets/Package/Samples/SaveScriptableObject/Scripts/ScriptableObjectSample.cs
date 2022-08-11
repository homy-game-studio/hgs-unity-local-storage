using UnityEngine;
using UnityEngine.UI;

public class ScriptableObjectSample : MonoBehaviour
{
  [SerializeField] User user;

  [Header("Fields")]
  [SerializeField] InputField coinField;
  [SerializeField] InputField uidField;
  [SerializeField] InputField displayNameField;

  [Header("Buttons")]
  [SerializeField] Button addCoinBtn;
  [SerializeField] Button removeCoinBtn;
  [SerializeField] Button saveBtn;
  [SerializeField] Button loadBtn;

  void Awake()
  {
    addCoinBtn.onClick.AddListener(HandleOnClickAddCoin);
    removeCoinBtn.onClick.AddListener(HandleOnClickRemoveCoin);
    saveBtn.onClick.AddListener(HandleOnClickSave);
    loadBtn.onClick.AddListener(HandleOnClickLoad);
  }

  void Start()
  {
    Load();
  }

  void Save()
  {
    user.SetDisplayName(displayNameField.text);
    user.SetUID(uidField.text);
    user.Save();
  }

  void Load()
  {
    user.Load();
    coinField.text = user.Coins.ToString();
    uidField.text = user.UID;
    displayNameField.text = user.DisplayName;
  }

  private void HandleOnClickLoad()
  {
    Load();
  }

  private void HandleOnClickSave()
  {
    Save();
  }

  private void HandleOnClickRemoveCoin()
  {
    user.RemoveCoins(1);
    coinField.text = user.Coins.ToString();
  }

  private void HandleOnClickAddCoin()
  {
    user.AddCoins(1);
    coinField.text = user.Coins.ToString();
  }
}
