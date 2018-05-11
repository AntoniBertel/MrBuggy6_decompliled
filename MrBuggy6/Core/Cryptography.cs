// Decompiled with JetBrains decompiler
// Type: MrBuggy6.Core.Cryptography
// Assembly: MrBuggy6, Version=0.0.2.10, Culture=neutral, PublicKeyToken=null
// MVID: 8A240CC2-2864-4FE9-9A6D-5C91EF9E6BC2
// Assembly location: C:\Projects\saving_cup\MrBuggy6_demo\MrBuggy6.exe

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MrBuggy6.Core
{
  public class Cryptography
  {
    private PasswordDeriveBytes pdb;
    private Rijndael rijndael;

    public string Password { get; private set; }

    public byte[] Salt { get; private set; }

    public byte[] Key
    {
      get
      {
        return this.pdb.GetBytes(32);
      }
    }

    public byte[] IV
    {
      get
      {
        return this.pdb.GetBytes(16);
      }
    }

    public Cryptography(string password, byte[] salt)
    {
      this.Password = password;
      this.Salt = salt;
      this.pdb = new PasswordDeriveBytes(this.Password, this.Salt);
      this.rijndael = Rijndael.Create();
      this.rijndael.Key = this.Key;
      this.rijndael.IV = this.IV;
    }

    private byte[] Encrypt(byte[] clearData)
    {
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, this.rijndael.CreateEncryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(clearData, 0, clearData.Length);
      cryptoStream.Close();
      return memoryStream.ToArray();
    }

    public string Encrypt(string clearText)
    {
      return Convert.ToBase64String(this.Encrypt(Encoding.Unicode.GetBytes(clearText)));
    }

    public string Encrypt(int clearInt32)
    {
      return Convert.ToBase64String(this.Encrypt(BitConverter.GetBytes(clearInt32)));
    }

    public string Encrypt(uint clearUInt32)
    {
      return Convert.ToBase64String(this.Encrypt(BitConverter.GetBytes(clearUInt32)));
    }

    public string Encrypt(long clearInt64)
    {
      return Convert.ToBase64String(this.Encrypt(BitConverter.GetBytes(clearInt64)));
    }

    public string Encrypt(bool clearBoolean)
    {
      return Convert.ToBase64String(this.Encrypt(BitConverter.GetBytes(clearBoolean)));
    }

    public string Encrypt(double clearDouble)
    {
      return Convert.ToBase64String(this.Encrypt(BitConverter.GetBytes(clearDouble)));
    }

    public void Encrypt(string fileIn, string fileOut)
    {
      FileStream fileStream = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
      CryptoStream cryptoStream = new CryptoStream((Stream) new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write), this.rijndael.CreateEncryptor(), CryptoStreamMode.Write);
      byte[] buffer = new byte[4096];
      int count;
      do
      {
        count = fileStream.Read(buffer, 0, buffer.Length);
        cryptoStream.Write(buffer, 0, count);
      }
      while (count != 0);
      cryptoStream.Close();
      fileStream.Close();
    }

    public byte[] Decrypt(byte[] cipherData)
    {
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, this.rijndael.CreateDecryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(cipherData, 0, cipherData.Length);
      cryptoStream.Close();
      return memoryStream.ToArray();
    }

    public string DecryptToString(string cipherText)
    {
      return Encoding.Unicode.GetString(this.Decrypt(Convert.FromBase64String(cipherText)));
    }

    public int DecryptToInt32(string cipherText)
    {
      return BitConverter.ToInt32(this.Decrypt(Convert.FromBase64String(cipherText)), 0);
    }

    public uint DecryptToUInt32(string cipherText)
    {
      return BitConverter.ToUInt32(this.Decrypt(Convert.FromBase64String(cipherText)), 0);
    }

    public long DecryptToInt64(string cipherText)
    {
      return BitConverter.ToInt64(this.Decrypt(Convert.FromBase64String(cipherText)), 0);
    }

    public bool DecryptToBoolean(string cipherText)
    {
      return BitConverter.ToBoolean(this.Decrypt(Convert.FromBase64String(cipherText)), 0);
    }

    public double DecryptToDouble(string cipherText)
    {
      return BitConverter.ToDouble(this.Decrypt(Convert.FromBase64String(cipherText)), 0);
    }

    public void Decrypt(string fileIn, string fileOut, string Password)
    {
      FileStream fileStream = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
      CryptoStream cryptoStream = new CryptoStream((Stream) new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write), this.rijndael.CreateDecryptor(), CryptoStreamMode.Write);
      int count1 = 4096;
      byte[] buffer = new byte[count1];
      int count2;
      do
      {
        count2 = fileStream.Read(buffer, 0, count1);
        cryptoStream.Write(buffer, 0, count2);
      }
      while (count2 != 0);
      cryptoStream.Close();
      fileStream.Close();
    }
  }
}
