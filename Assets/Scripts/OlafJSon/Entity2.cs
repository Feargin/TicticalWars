using UnityEngine;
using SoundSteppe.JsonSS;

public class Entity2 : SaveableMono
{
	public Inventory inv;
	public Stats stats;
	
	[Saveable] public Vector3 _myPos;
	[Saveable] public int[] my_array;
	public Inventory[] my_inv_arr;
	
	[Saveable] public int a = 4;
	[Saveable] public float fl = 13.7f;
	[Saveable] public bool myBool = false;
	
	public override void OnSave()
	{
		_myPos = transform.position;
	}
	
	public override void Init()
	{
		transform.position = _myPos;
	}
}