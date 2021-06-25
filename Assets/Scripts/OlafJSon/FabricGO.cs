using UnityEngine;

namespace SoundSteppe.JsonSS
{
	public sealed class FabricGO : MonoBehaviour
	{
		public JsonSS jsonSS;
		public SaveableMono Prefab;
		
		public void SaveObject(string ID, SaveableMono obj)
		{
			jsonSS.SaveObject(ID, obj);
		}
		
		public void SaveObjects(string ID, SaveableMono[] array)
		{
			jsonSS.SaveArray(ID, array);
		}
		
		public void LoadObject(string ID)
		{
			string json = jsonSS.LoadObject(ID);
			SaveableMono e = Instantiate(Prefab);
			e.LoadGameObject(json);
			e.Init();
		}
		
		public void LoadObjects(string ID)
		{
			string[] json = jsonSS.LoadArray(ID).ToArray();
			for(int i = 0; i < json.Length; i++)
			{
				SaveableMono e = Instantiate(Prefab);
				e.LoadGameObject(json[i]);
				e.Init();
			}
		}
	}
}