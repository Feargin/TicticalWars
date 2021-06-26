using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

// SaveComponent()
// SaveGameObject()
// do it with method extensions

namespace SoundSteppe.JsonSS
{
	public static class JsonSS
	{
		private static readonly string crypt_password = "l2Kfrql5o4m89545";
		
		private static readonly byte[] crypt_iv = 
		{ 0x12, 0x3, 0x9, 0x3, 0x16, 
			0x15, 0x6, 0x15, 0x14, 0x12, 
			0x14, 0x15, 0x12, 0x15, 0x14, 0x8 
		};
		
		public static void SaveArray(string ID, SaveableMono[] array)
		{
			string json = "";
			
			foreach(var e in array)
			{
				json += "*{" + e.SaveGameObject() + "}*";
			}
			
			string path = Application.dataPath + "/" + ID + ".data";
			Encrypt(path, json);
		}
		
		private static void Encrypt(string path, string json)
		{
			using(Aes iAes = Aes.Create())
			{
				iAes.IV = crypt_iv;
				
				iAes.Key = ConvertToKeyBytes(iAes, crypt_password);
				var textBytes = Encoding.UTF8.GetBytes(json);
				var aesEncryptor = iAes.CreateEncryptor();
				var encryptedBytes = aesEncryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
				
				File.WriteAllText(path, System.Convert.ToBase64String(encryptedBytes));
			}
		}
		
		private static string Decrypt(string path)
		{
			if (File.Exists(path))
			{
				using (Aes aes = Aes.Create())
				{
					string json = File.ReadAllText(path);
					var ivBytes = crypt_iv;
					
					byte[] encryptedTextBytes = null;
					try
					{
						encryptedTextBytes = System.Convert.FromBase64String(json);
					}
					catch
					{
						Debug.LogError("Datafile is corrupted!");
						return "";
					}
					var decryptor = aes.CreateDecryptor(ConvertToKeyBytes(aes, crypt_password), ivBytes);
					
					byte[] decryptedBytes = null;
					try
					{
						decryptedBytes = decryptor.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);
					}
					catch
					{
						Debug.LogError("Datafile is corrupted!");
						return "";
					}
					
					if(decryptedBytes != null)
						json = Encoding.UTF8.GetString(decryptedBytes);
					return json;
				}
			}
			return "";
		}
		
		private static byte[] ConvertToKeyBytes(SymmetricAlgorithm algorithm, string password)
		{
			algorithm.GenerateKey();

			var keyBytes = Encoding.UTF8.GetBytes(password);
			var validKeySize = algorithm.Key.Length;

			if (keyBytes.Length != validKeySize)
			{
				var newKeyBytes = new byte[validKeySize];
				System.Array.Copy(keyBytes, newKeyBytes, Mathf.Min(keyBytes.Length, newKeyBytes.Length));
				keyBytes = newKeyBytes;
			}

			return keyBytes;
		}
		
		public static List<string> LoadArray(string ID)
		{
			string path = Application.dataPath + "/" + ID + ".data";
			string json = "";
			
			json = Decrypt(path);
				
			List<string> elements = new List<string>();
			
			string element = "";
			while(json.Length > 0)
			{
				int start = json.IndexOf("*{");
				int end = json.IndexOf("}*", start);
				if(start < 0 || end < 0)
					break;
				element = json.Substring(start, end - start + 2);
				json = json.Remove(start, end - start + 2);
				element = element.Replace("*{", "{");
				element = element.Replace("}*", "}");
				elements.Add(element);
			}
			return elements;
		} 
	    
		public static void SaveObject(string ID, SaveableMono obj)
		{
			string json = "{}";
			
			json = obj.SaveGameObject();
			
			string path = Application.dataPath + "/" + ID + ".data";
			Encrypt(path, json);
		} 
		 
		public static string LoadObject(string ID)
		{
			string path = Application.dataPath + "/" + ID + ".data";
			string json = "";
			
			json = Decrypt(path);
				
			return json;
		}
	}
}