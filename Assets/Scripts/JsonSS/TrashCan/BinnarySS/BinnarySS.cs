using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public class BinnarySS : MonoBehaviour
{
	public void Save(string ID, object data)
	{
		string path = Application.dataPath + "/" + ID + ".data";
		FileStream dataStream = new FileStream(path, FileMode.Create);

		BinaryFormatter converter = new BinaryFormatter();
		converter.Serialize(dataStream, data);

		dataStream.Close();
	}
	
	public object Load(string ID)
	{
		string path = Application.dataPath + "/" + ID + ".data";
		
		if(File.Exists(path))
		{
			FileStream dataStream = new FileStream(path, FileMode.Open);

			BinaryFormatter converter = new BinaryFormatter();
			object saveData = converter.Deserialize(dataStream);

			dataStream.Close();
			return saveData;
		}
		return null;
	}
}
