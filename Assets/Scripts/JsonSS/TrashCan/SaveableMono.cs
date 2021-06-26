using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEngine;
using SimpleJSON;

namespace SoundSteppe.JsonSS
{
	public class SaveableMono : MonoBehaviour
	{
		// Init is called after instantiate with FabricGO
		//public virtual void Init(){}
		
		// OnSave is called before saving
		//public virtual void OnSave(){}
		
		
		
		
		
		//public string SaveGameObject()
		//{
		//	OnSave();
			
		//	string json = "";
		//	var saveable = gameObject.GetComponents<SaveableMono>();
		//	foreach(var s in saveable)
		//	{
		//		json += s.GetType() + ":|" + s.Save() + "|";
		//	}
		//	return json;
		//}
		
		//public void LoadGameObject(string json)
		//{
		//	var saveable = gameObject.GetComponents<SaveableMono>();
		//	foreach(var s in saveable)
		//	{
		//		string componentJson = ExtractJsonTo(s, json);
		//		s.Load(componentJson);
		//	}
		//}
		
		//private string ExtractJsonTo(SaveableMono s, string json)
		//{
		//	string compJson = "";
			
		//	string compName = s.GetType() + ":|{";
		//	int start = json.IndexOf(compName);
		//	if(start < 0)
		//		return "";
		//	int end = json.IndexOf("}|", start);
		//	if(end < 0)
		//		return "";
		//	compJson = json.Substring(start + compName.Length - 1, end - start - compName.Length + 2);
		//	return compJson;
		//}
		
		//private string Save()
		//{
		//	string json = GetJSON();
		//	return json;
		//}
		
		//private string GetJSON()
		//{
		//	string json = "{}";
		//	JSONNode node = JSON.Parse(json);
			
		//	FieldInfo[] fields = GetType().GetFields();
		//	foreach(FieldInfo field in fields)
		//	{
		//		if(field.GetCustomAttribute<Saveable>() != null)
		//		{
		//			if(field.FieldType.IsArray)
		//			{
		//				var jsonArray = node[field.Name].AsArray;
		//				IEnumerable array = field.GetValue(this) as IEnumerable;
						
		//				if(array != null)
		//				{
		//					foreach(var obj in array)
		//					{
		//						System.Type t = obj.GetType();
		//						if(t.IsValueType == true && t.IsPrimitive == false)
		//						{
		//							jsonArray.Add(JsonUtility.ToJson(obj));
		//						}
		//						else
		//						{
		//							JSONNode jn = JSONNode.Parse(obj.ToString());
		//							if(jn != null)
		//								jsonArray.Add(jn);
		//						}
		//					}
		//				}
		//			}
		//			else if(field.FieldType == typeof(Vector3))
		//			{
		//				Vector3 v = (Vector3)field.GetValue(this);
		//				node[field.Name] = v;
		//			}
		//			else if(field.FieldType.IsValueType == true && field.FieldType.IsPrimitive == false)
		//			{
		//				string j = JsonUtility.ToJson(field.GetValue(this));
		//				node[field.Name] = j;
		//			}
		//			else
		//			{
		//				node[field.Name] = field.GetValue(this).ToString();
		//			}
		//		}
		//	}
			
		//	return node.ToString();
		//}
		
		//private void Load(string json)
		//{
		//	JSONNode node = JSON.Parse(json);
			
		//	FieldInfo[] fields = GetType().GetFields();
		//	foreach(FieldInfo field in fields)
		//	{
		//		if(field.GetCustomAttribute<Saveable>() != null)
		//		{
		//			if(field.FieldType.IsArray)
		//			{
		//				SetArrayFieldValue(node, field);
		//			}
		//			else
		//			{
		//				SetFieldValue(node, field);
		//			}
		//		}
		//	}
		//}
		
		//private void SetFieldValue(JSONNode node, FieldInfo field)
		//{
		//	string valueJson = node[field.Name].Value;
		//	object valueObj = null;
		//	try
		//	{
		//		if(field.FieldType == typeof(Vector3))
		//		{
		//			valueObj = node[field.Name].ReadVector3();
		//		}
		//		else if(field.FieldType == typeof(string))
		//		{
		//			valueObj = node[field.Name].Value;
		//		}
		//		else if(field.FieldType.IsValueType == true && field.FieldType.IsPrimitive == false)
		//		{
		//			string json = node[field.Name].Value;
		//			valueObj = JsonUtility.FromJson(json, field.FieldType);
		//		}
		//		else
		//		{
		//			valueObj = System.Convert.ChangeType(valueJson, field.FieldType);
		//		}
		//	}
		//		catch(System.Exception e)
		//		{
		//			valueObj = null;
		//			Debug.LogWarning(e.Message);
		//		}
		//	if(valueObj != null)
		//		field.SetValue(this, valueObj);
		//}
		
		//private void SetArrayFieldValue(JSONNode node, FieldInfo field)
		//{
		//	var valueObj = node[field.Name].AsStringList;
		//	System.Type elementType = field.FieldType.GetElementType();
						
		//	List<object> listObj = new List<object>();
		//	if(elementType.IsValueType == true && elementType.IsPrimitive == false)
		//	{
		//		for(int i = 0; i < valueObj.Count; i++)
		//		{
		//			listObj.Add(valueObj[i]);
		//		}
		//	}
		//	else
		//	{
		//		for(int i = 0; i < valueObj.Count; i++)
		//		{
		//			object obj = System.Convert.ChangeType(valueObj[i], field.FieldType.GetElementType());
		//			listObj.Add(obj);
		//		}
		//	}
			
		//	if(elementType == typeof(System.String))
		//	{
		//		field.SetValue(this, listObj);
		//	}
		//	else if(elementType == typeof(System.Int32))
		//	{
		//		var array = listObj.Cast<System.Int32>().ToArray();
		//		field.SetValue(this, array);
		//	}
		//	else if(elementType == typeof(System.Boolean))
		//	{
		//		var array = listObj.Cast<System.Boolean>().ToArray();
		//		field.SetValue(this, array);
		//	}
		//	else if(elementType == typeof(System.Single))
		//	{
		//		var array = listObj.Cast<System.Single>().ToArray();
		//		field.SetValue(this, array);
		//	}
		//	else if(elementType == typeof(System.Double))
		//	{
		//		var array = listObj.Cast<System.Double>().ToArray();
		//		field.SetValue(this, array); 
		//	}
		//	else if(elementType == typeof(System.Byte))
		//	{
		//		var array = listObj.Cast<System.Byte>().ToArray();
		//		field.SetValue(this, array);
		//	}
		//	else if(elementType == typeof(System.Int64))
		//	{
		//		var array = listObj.Cast<System.Int64>().ToArray();
		//		field.SetValue(this, array);
		//	}
		//	else if(elementType.IsValueType == true && elementType.IsPrimitive == false)
		//	{
		//		System.Array arr = System.Array.CreateInstance(elementType, listObj.Count);
		//		for(int i = 0; i < listObj.Count; i++)
		//		{
		//			arr.SetValue(JsonUtility.FromJson((string)listObj[i], elementType), i);
		//		}
		//		field.SetValue(this, arr);
		//	}
		//	else
		//	{
		//		Debug.LogWarning("Unsupported array type: " + elementType);
		//	}
		//}
	}
}