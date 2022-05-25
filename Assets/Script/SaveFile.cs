using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
using Newtonsoft.Json;

public class SaveFile : MonoBehaviour
{
    string password = "";
    Dictionary<string, bool> StageClear = new Dictionary<string, bool>()
    {
        {"Tutorial", false},
        {"Stage 1", false},
        {"Stage 2", false},
        {"Stage 3", false},
        {"Stage 4", false},
        {"Stage 5", false},
        {"Stage 6", false},
        {"Stage 7", false},
        {"Stage 8", false},
        {"Stage 9", false},
        {"Stage 10", false},
        {"Final", false}
    };
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
            StageClear = JsonConvert.DeserializeObject<Dictionary<string, bool>>(Decrypt(PlayerPrefs.GetString("StageClear")));
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
