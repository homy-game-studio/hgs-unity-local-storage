using System;
using HGS.LocalStorage;
using HGS.LocalStorage.JsonExtension;
using UnityEngine;

[CreateAssetMenu(menuName = "User")]
public class User : ScriptableObject
{
  [SerializeField] Storage storage = null;
  [SerializeField] string key = "userData";
  UserData _data = new UserData();

  public string UID => _data.uid;
  public string DisplayName => _data.displayName;
  public int Coins => _data.coins;

  public void SetUID(string uid)
  {
    _data.uid = uid;
  }

  public void SetDisplayName(string displayName)
  {
    _data.displayName = displayName;
  }

  public void AddCoins(int amount)
  {
    _data.coins += amount;
  }

  public void RemoveCoins(int amount)
  {
    _data.coins -= Math.Max(Coins - amount, 0);
  }

  public void Save()
  {
    storage.WriteObject(key, _data);
  }

  public void Load()
  {
    if (!storage.Exists(key)) return;
    _data = storage.ReadObject<UserData>(key);
  }
}
