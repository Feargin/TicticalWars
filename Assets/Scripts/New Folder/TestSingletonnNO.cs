using UnityEngine;

public class TestSingletonnNO : MonoBehaviour, IService
{
	public int TestInt = 33;
	
	public void DoThis()
    {
	    print(TestInt);
    }
}

public interface IService
{
	public void DoThis();
}


