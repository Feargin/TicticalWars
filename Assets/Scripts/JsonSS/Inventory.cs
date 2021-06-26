using UnityEngine;
using SoundSteppe.JsonSS;

public class Inventory : SaveableMono
{
	[Saveable] public int a = 3;
	[Saveable] public int b = 33;
}