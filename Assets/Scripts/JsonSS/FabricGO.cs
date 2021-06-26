using UnityEngine;

namespace SoundSteppe.JsonSS
{
	public sealed class FabricGO : MonoBehaviour
	{
		public SaveableMono Prefab;
		
		public void SaveObject(string ID, SaveableMono obj)
		{
			JsonSS.SaveObject(ID, obj);
		}
		
		public void SaveObjects(string ID, SaveableMono[] array)
		{
			JsonSS.SaveArray(ID, array);
		}
		
		public void LoadObject(string ID)
		{
			string json = JsonSS.LoadObject(ID);
			SaveableMono e = Instantiate(Prefab);
			e.LoadGameObject(json);
			e.Init();
		}
		
		public void LoadObjects(string ID)
		{
			string[] json = JsonSS.LoadArray(ID).ToArray();
			for(int i = 0; i < json.Length; i++)
			{
				SaveableMono e = Instantiate(Prefab);
				e.LoadGameObject(json[i]);
				e.Init();
			}
		}
	}
}