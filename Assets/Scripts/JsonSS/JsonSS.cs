using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using UnityEngine;
using System.Linq;
using SimpleJSON;

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
		
		#region Public methods
		public static void SaveGameObject(string ID, MonoBehaviour obj)
		{
			string json = "{}";
			
			json = GetObjectJson(obj);
			
			string path = Application.dataPath + "/" + ID + ".data";
			Encrypt(path, json);
		} 
		
		public static void LoadGameObject(string ID, MonoBehaviour mono)
		{
			string json = JsonSS.LoadObjectJson(ID);
			
			if(string.IsNullOrEmpty(json) == false)
			{
				var saveable = mono.gameObject.GetComponents<MonoBehaviour>();
				foreach(var s in saveable)
				{
					string componentJson = ExtractJsonTo(s, json);
					PassValuesToFields(s, componentJson);
				}
			}
		}
		
		public static void LoadGameObject(MonoBehaviour mono, string json)
		{
			if(string.IsNullOrEmpty(json) == false)
			{
				var saveable = mono.gameObject.GetComponents<MonoBehaviour>();
				foreach(var s in saveable)
				{
					string componentJson = ExtractJsonTo(s, json);
					PassValuesToFields(s, componentJson);
				}
			}
		}
		
		public static void SaveArray(string ID, MonoBehaviour[] array)
		{
			string json = "";
			
			foreach(var e in array)
			{
				json += "*{" + GetObjectJson(e) + "}*";
			}
			
			string path = Application.dataPath + "/" + ID + ".data";
			Encrypt(path, json);
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
		#endregion
		
		#region Private methods
		private static string LoadObjectJson(string ID)
		{
			string path = Application.dataPath + "/" + ID + ".data";
			string json = "";
			
			json = Decrypt(path);
				
			return json;
		}
		
		private static string GetObjectJson(MonoBehaviour mono)
		{
			string json = "";
			var saveable = mono.GetComponents<ISaveable>();
			foreach(var s in saveable)
			{
				s.OnSave();
				
			}
			var ss = mono.GetComponents<MonoBehaviour>();
			foreach(var s in ss)
			{
				json += s.GetType() + ":|" + GetFieldsJson(s) + "|";
			}
			return json;
		}
		
		private static string GetFieldsJson(MonoBehaviour s)
		{
			string json = "{}";
			JSONNode node = JSON.Parse(json);
			
			FieldInfo[] fields = s.GetType().GetFields();
			foreach(FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<Saveable>() != null)
				{
					if(field.FieldType.IsArray)
					{
						var jsonArray = node[field.Name].AsArray;
						IEnumerable array = field.GetValue(s) as IEnumerable;
						
						if(array != null)
						{
							foreach(var obj in array)
							{
								System.Type t = obj.GetType();
								if(t.IsValueType == true && t.IsPrimitive == false)
								{
									jsonArray.Add(JsonUtility.ToJson(obj));
								}
								else
								{
									JSONNode jn = JSONNode.Parse(obj.ToString());
									if(jn != null)
										jsonArray.Add(jn);
								}
							}
						}
					}
					else if(field.FieldType == typeof(Vector3))
					{
						Vector3 v = (Vector3)field.GetValue(s);
						node[field.Name] = v;
					}
					else if(field.FieldType.IsValueType == true && field.FieldType.IsPrimitive == false)
					{
						string j = JsonUtility.ToJson(field.GetValue(s));
						node[field.Name] = j;
					}
					else
					{
						node[field.Name] = field.GetValue(s).ToString();
					}
				}
			}
			return node.ToString();
		}
		
		private static void PassValuesToFields(MonoBehaviour s, string json)
		{
			JSONNode node = JSON.Parse(json);
			
			FieldInfo[] fields = s.GetType().GetFields();
			foreach(FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<Saveable>() != null)
				{
					if(field.FieldType.IsArray)
					{
						SetArrayFieldValue(s, node, field);
					}
					else
					{
						SetFieldValue(s, node, field);
					}
				}
			}
		}
		
		private static string ExtractJsonTo(MonoBehaviour s, string json)
		{
			string compJson = "";
			
			string compName = s.GetType() + ":|{";
			int start = json.IndexOf(compName);
			if(start < 0)
				return "";
			int end = json.IndexOf("}|", start);
			if(end < 0)
				return "";
			compJson = json.Substring(start + compName.Length - 1, end - start - compName.Length + 2);
			return compJson;
		}
		
		private static void SetArrayFieldValue(MonoBehaviour s, JSONNode node, FieldInfo field)
		{
			var valueObj = node[field.Name].AsStringList;
			System.Type elementType = field.FieldType.GetElementType();
						
			List<object> listObj = new List<object>();
			if(elementType.IsValueType == true && elementType.IsPrimitive == false)
			{
				for(int i = 0; i < valueObj.Count; i++)
				{
					listObj.Add(valueObj[i]);
				}
			}
			else
			{
				for(int i = 0; i < valueObj.Count; i++)
				{
					object obj = System.Convert.ChangeType(valueObj[i], field.FieldType.GetElementType());
					listObj.Add(obj);
				}
			}
			
			if(elementType == typeof(System.String))
			{
				field.SetValue(s, listObj);
			}
			else if(elementType == typeof(System.Int32))
			{
				var array = listObj.Cast<System.Int32>().ToArray();
				field.SetValue(s, array);
			}
			else if(elementType == typeof(System.Boolean))
			{
				var array = listObj.Cast<System.Boolean>().ToArray();
				field.SetValue(s, array);
			}
			else if(elementType == typeof(System.Single))
			{
				var array = listObj.Cast<System.Single>().ToArray();
				field.SetValue(s, array);
			}
			else if(elementType == typeof(System.Double))
			{
				var array = listObj.Cast<System.Double>().ToArray();
				field.SetValue(s, array); 
			}
			else if(elementType == typeof(System.Byte))
			{
				var array = listObj.Cast<System.Byte>().ToArray();
				field.SetValue(s, array);
			}
			else if(elementType == typeof(System.Int64))
			{
				var array = listObj.Cast<System.Int64>().ToArray();
				field.SetValue(s, array);
			}
			else if(elementType.IsValueType == true && elementType.IsPrimitive == false)
			{
				System.Array arr = System.Array.CreateInstance(elementType, listObj.Count);
				for(int i = 0; i < listObj.Count; i++)
				{
					arr.SetValue(JsonUtility.FromJson((string)listObj[i], elementType), i);
				}
				field.SetValue(s, arr);
			}
			else
			{
				Debug.LogWarning("Unsupported array type: " + elementType);
			}
		}
		
		private static void SetFieldValue(MonoBehaviour s, JSONNode node, FieldInfo field)
		{
			string valueJson = node[field.Name].Value;
			object valueObj = null;
			try
			{
				if(field.FieldType == typeof(Vector3))
				{
					valueObj = node[field.Name].ReadVector3();
				}
				else if(field.FieldType == typeof(string))
				{
					valueObj = node[field.Name].Value;
				}
				else if(field.FieldType.IsValueType == true && field.FieldType.IsPrimitive == false)
				{
					string json = node[field.Name].Value;
					valueObj = JsonUtility.FromJson(json, field.FieldType);
				}
				else
				{
					valueObj = System.Convert.ChangeType(valueJson, field.FieldType);
				}
			}
				catch(System.Exception e)
				{
					valueObj = null;
					Debug.LogWarning(e.Message);
				}
			if(valueObj != null)
				field.SetValue(s, valueObj);
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
		#endregion
	}
}