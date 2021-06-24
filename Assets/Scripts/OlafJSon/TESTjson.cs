using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using SimpleJSON;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TESTjson : MonoBehaviour
{
	public Entity2[] entities;
	
	private void Update()
	{
		if(Keyboard.current.sKey.wasPressedThisFrame)
	    {
	    	Save();
	    }
		if(Keyboard.current.lKey.wasPressedThisFrame)
	    {
	    	Load(); 
	    }
    }
    
	private void Save()
	{
		string json = "{}";
		//string json = "{\"array:\"[";
		
		//for(int i = 0; i < entities.Length; i++)
		//{
		//	json += (i != 0 ? "," : "") + entities[i].Save();
		//}
		
		//json = entities[0].Save();
		json = entities[0].SaveGameObject();
		
		//json += "]}";
		//JSONNode node = JSON.Parse(json);
		
		//var intArr = node["testArr"].AsArray;
		//intArr.Add(7);
		//intArr.Add(4);
		//intArr.Add(8);
		
		//node["MyArr"] = arr;
		
		//node.Add("MyArr", arr);
		
		//print(node.ToString());
		
		string path = Application.dataPath + "/enetity.data";
		File.WriteAllText(path, json);
		print("json saved:  " + json);
	} 
	 
	private void Load()
	{
		string path = Application.dataPath + "/enetity.data";
		string json = File.ReadAllText(path);
		entities[0].LoadGameObject(json);
	}
}

public class SaveableMono : MonoBehaviour
{
	public string SaveGameObject()
	{
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
			print(componentJson);
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
	
	public string Save()
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
				else
				{
					node[field.Name] = field.GetValue(this).ToString();
				}
			}
		}
		
		return node.ToString();
	}
	
	public void Load(string json)
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
			valueObj = System.Convert.ChangeType(valueJson, field.FieldType);
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
