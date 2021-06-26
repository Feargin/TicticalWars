public interface ISaveable
{
	// Init is called after instantiate with FabricGO
	public void Init();
	
	// OnSave is called before saving
	public void OnSave();
}
