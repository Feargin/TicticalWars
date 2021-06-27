using UnityEngine;
using SoundSteppe.JsonSS;

public class Entity2 : MonoBehaviour, ISaveable
{
	public Inventory[] my_inv_arr;
	public Inventory inv;
	public Stats stats;
	
	[Saveable] public S_Weapon weapon;
	[Saveable] public S_Weapon[] weaponssss;
	[Saveable] public string aaa;
	
	[Saveable] public Vector3 _myPos;
	[Saveable] public int[] my_array;
	
	[Saveable] public int a = 4;
	[Saveable] public float fl = 13.7f;
	[Saveable] public bool myBool = false;
	
	public void OnSave()
	{
		_myPos = transform.position;
	}
	
	public void OnLoad()
	{
		transform.position = _myPos;
	}
}