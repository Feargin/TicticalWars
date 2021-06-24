using UnityEngine;

public class Entity2 : SaveableMono
{
	[Saveable] public Inventory inv;
	public Stats stats;
	
	//[Saveable] public SaveableMono[] saveable_test_arr;
	
	[Saveable] public int[] my_array;
	[Saveable] public Inventory[] my_inv_arr;
	
	[Saveable] public int a = 4;
	[Saveable] public float fl = 13.7f;
	[Saveable] public bool myBool = false;
}