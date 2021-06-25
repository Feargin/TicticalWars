using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using SimpleJSON;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SoundSteppe.JsonSS
{
	public sealed class JsonSS : MonoBehaviour
	{
		public void SaveArray(string ID, SaveableMono[] array)
		{
			string json = "";
			
			foreach(var e in array)
			{
				json += "*{" + e.SaveGameObject() + "}*";
			}
			
			string path = Application.dataPath + "/" + ID + ".data";
			File.WriteAllText(path, json);
			//print(json);
		} 
		
		public List<string> LoadArray(string ID)
		{
			string path = Application.dataPath + "/" + ID + ".data";
			string json = File.ReadAllText(path);
			
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
	    
		public void SaveObject(string ID, SaveableMono obj)
		{
			string json = "{}";
			
			json = obj.SaveGameObject();
			
			string path = Application.dataPath + "/enetity.data";
			File.WriteAllText(path, json);
		} 
		 
		public string LoadObject(string ID)
		{
			string path = Application.dataPath + "/enetity.data";
			string json = File.ReadAllText(path);
			return json;
		}
	}
	
	public class SaveableMono : MonoBehaviour
	{
		// Init is called after instantiate trough FabricGO
		public virtual void Init(){}
		
		// OnSave is called before saving
		public virtual void OnSave(){}
		
		public string SaveGameObject()
		{
			OnSave();
			
			string json = "";
			var saveable = gameObject.GetComponents<SaveableMono>();
			foreach(var s in saveable)
			{
				json += s.GetType() + ":|" + s.Save() + "|";
			}
			return json;
		}
		
		public void LoadGameObject(string json)
		{
			var saveable = gameObject.GetComponents<SaveableMono>();
			foreach(var s in saveable)
			{
				string componentJson = ExtractJsonTo(s, json);
				s.Load(componentJson);
			}
		}
		
		private string ExtractJsonTo(SaveableMono s, string json)
		{
			string compJson = "";
			
			string compName = s.GetType() + ":|{";
			int start = json.IndexOf(compName);
			int end = json.IndexOf("}|", start);
			if(start < 0 || end < 0)
				return "";
			compJson = json.Substring(start + compName.Length - 1, end - start - compName.Length + 2);
			return compJson;
		}
		
		private string Save()
		{
			string json = GetJSON();
			return json;
		}
		
		private string GetJSON()
		{
			string json = "{}";
			JSONNode node = JSON.Parse(json);
			
			FieldInfo[] fields = GetType().GetFields();
			foreach(FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<Saveable>() != null)
				{
					//if(field.GetValue(this) is SaveableMono)
					//{
					//	string child = (field.GetValue(this) as SaveableMono).Save();
					//	print(child);
					//}
					//else 
					if(field.FieldType.IsArray)
					{
						var jsonArray = node[field.Name].AsArray;
						IEnumerable array = field.GetValue(this) as IEnumerable;
						
						if(array != null)
						{
							foreach(var obj in array)
							{
								JSONNode jn = JSONNode.Parse(obj.ToString());
								if(jn != null)
									jsonArray.Add(jn);
							}
						}
					}
					else if(field.FieldType == typeof(Vector3))
					{
						Vector3 v = (Vector3)field.GetValue(this);
						node[field.Name] = v;
					}
					else
					{
						node[field.Name] = field.GetValue(this).ToString();
					}
				}
			}
			
			return node.ToString();
		}
		
		private void Load(string json)
		{
			JSONNode node = JSON.Parse(json);
			
			FieldInfo[] fields = GetType().GetFields();
			foreach(FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<Saveable>() != null)
				{
					if(field.FieldType.IsArray)
					{
						SetArrayFieldValue(node, field);
					}
					else
					{
						SetFieldValue(node, field);
					}
				}
			}
		}
		
		private void SetFieldValue(JSONNode node, FieldInfo field)
		{
			string valueJson = node[field.Name].Value;
			object valueObj = null;
			try
			{
				if(field.FieldType == typeof(Vector3))
				{
					valueObj = node[field.Name].ReadVector3();
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
				field.SetValue(this, valueObj);
		}
		
		private void SetArrayFieldValue(JSONNode node, FieldInfo field)
		{
			var valueObj = node[field.Name].AsStringList;
						
			List<object> listObj = new List<object>();
			for(int i = 0; i < valueObj.Count; i++)
			{
				object obj = System.Convert.ChangeType(valueObj[i], field.FieldType.GetElementType());
				listObj.Add(obj);
			}
			
			System.Type elementType = field.FieldType.GetElementType();
			
			if(elementType == typeof(System.String))
			{
				field.SetValue(this, listObj);
			}
			else if(elementType == typeof(System.Int32))
			{
				var array = listObj.Cast<System.Int32>().ToArray();
				field.SetValue(this, array);
			}
			else if(elementType == typeof(System.Boolean))
			{
				var array = listObj.Cast<System.Boolean>().ToArray();
				field.SetValue(this, array);
			}
			else if(elementType == typeof(System.Single))
			{
				var array = listObj.Cast<System.Single>().ToArray();
				field.SetValue(this, array);
			}
			else if(elementType == typeof(System.Double))
			{
				var array = listObj.Cast<System.Double>().ToArray();
				field.SetValue(this, array); 
			}
			else if(elementType == typeof(System.Byte))
			{
				var array = listObj.Cast<System.Byte>().ToArray();
				field.SetValue(this, array);
			}
			else if(elementType == typeof(System.Int64))
			{
				var array = listObj.Cast<System.Int64>().ToArray();
				field.SetValue(this, array);
			}
			//else if(elementType == typeof(SaveableMono))
			//{
				//var array = listObj.Cast<System.Int64>().ToArray();
				//field.SetValue(this, array);
			//}
			else
			{
				Debug.LogWarning("Unsupported array type: " + elementType);
			}
		}
	}
}