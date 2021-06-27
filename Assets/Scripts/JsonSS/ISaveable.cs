public interface ISaveable
{
	// OnLoad is called after instantiate with FabricGO
	public void OnLoad();
	
	// OnSave is called before saving
	public void OnSave();
}
