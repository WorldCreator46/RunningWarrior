using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System;
using Newtonsoft.Json;

public class SaveFile : MonoBehaviour
{
    string password = "";
    public Sprite ClearFinish;
    public Sprite ClearCoin;
    public Sprite ClearEquipment;
    private Dictionary<string, Dictionary<string, bool>> StageClear = new()
    {
        {"Tutorial", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 1", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 2", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 3", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 4", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 5", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 6", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 7", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 8", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 9", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Stage 10", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } },
        {"Final", new Dictionary<string, bool>(){ { "Finish", false }, { "Coin", false }, { "Equipment", false } } }
    };
    public void ClearStageCheck(string StageName, Image Finish, Image Coin, Image Equipment)
    {
        Finish.sprite = StageClear[StageName]["Finish"] ? ClearFinish : Finish.sprite;
        Coin.sprite = StageClear[StageName]["Coin"] ? ClearCoin : Coin.sprite;
        Equipment.sprite = StageClear[StageName]["Equipment"] ? ClearEquipment : Equipment.sprite;
    }
    public void SetStageClear(string StageName, bool Finish, bool Coin, bool Equipment)
    {
        StageClear[StageName]["Finish"] = StageClear[StageName]["Finish"] ? true : Finish;
        StageClear[StageName]["Coin"] = StageClear[StageName]["Coin"] ? true : Coin;
        StageClear[StageName]["Equipment"] = StageClear[StageName]["Equipment"] ? true : Equipment;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void Awake()
    {
        Load();
    }
    void Save()
    {
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.SetString("StageClear", Encrypt(JsonConvert.SerializeObject(StageClear)));
        PlayerPrefs.Save();
    }
    void Load()
    {
        if(PlayerPrefs.HasKey("password") && PlayerPrefs.HasKey("StageClear"))
        {
            password = PlayerPrefs.GetString("password");
            StageClear = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(Decrypt(PlayerPrefs.GetString("StageClear")));
        }
        else
        {
            SetPassword();
        }
    }
    private void SetPassword()
    {
        char[] RandomLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=".ToCharArray();
        StringBuilder temp = new();
        System.Random random = new();
        for (int i = 0; i < 16; i++) temp.Append(RandomLetters[random.Next(RandomLetters.Length)]);
        password = temp.ToString();
    }
    
    private string Encrypt(string textToEncrypt)
    {
        RijndaelManaged rijndaelCipher = new()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length) len = keyBytes.Length;
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }
    private string Decrypt(string textToDecrypt)
    {
        RijndaelManaged rijndaelCipher = new()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length) len = keyBytes.Length;
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
