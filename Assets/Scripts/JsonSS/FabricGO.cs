using UnityEngine;

namespace SoundSteppe.JsonSS
{
	public sealed class FabricGO : MonoBehaviour
	{
		public GameObject Prefab;
		
		public void SaveObject(string ID, MonoBehaviour obj)
		{
			JsonSS.SaveGameObject(ID, obj);
		}
		
		public void SaveObjects(string ID, MonoBehaviour[] array)
		{
			JsonSS.SaveGameObjects(ID, array);
		}
		
		public void LoadObject(string ID)
		{
			GameObject e = Instantiate(Prefab);
			JsonSS.LoadGameObject(ID, e.GetComponent<MonoBehaviour>());
		}
		
		public void LoadObjects(string ID)
		{
			string[] json = JsonSS.LoadArray(ID).ToArray();
			for(int i = 0; i < json.Length; i++)
			{
				GameObject e = Instantiate(Prefab);
				JsonSS.LoadGameObject(e.GetComponent<MonoBehaviour>(), json[i]);
			}
		}
	}
}