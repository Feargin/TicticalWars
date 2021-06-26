using UnityEngine;
using SoundSteppe.JsonSS;

[System.Serializable]
public class Entity2 : MonoBehaviour, ISaveable
{
	public Inventory inv;
	public Stats stats;
	
	[Saveable] public S_Weapon weapon;
	[Saveable] public S_Weapon[] weaponssss;
	[Saveable] public string aaa;
	
	[Saveable] public Vector3 _myPos;
	[Saveable] public int[] my_array;
	public Inventory[] my_inv_arr;
	
	[Saveable] public int a = 4;
	[Saveable] public float fl = 13.7f;
	[Saveable] public bool myBool = false;
	
	public void OnSave()
	{
		_myPos = transform.position;
	}
	
	public void Init()
	{
		transform.position = _myPos;
	}
}