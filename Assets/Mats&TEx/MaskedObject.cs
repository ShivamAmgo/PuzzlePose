using UnityEngine;

public class MaskedObject : MonoBehaviour
{
	public GameObject[] ObjMasked;
	
	private const int RenderQueueNumber = 3002;
	
	void Start()
	{
		
	}


	public void SetRenderForMask()
	{
		int count = ObjMasked[0].GetComponent<SkinnedMeshRenderer>().materials.Length;
		for (int i = 0; i < count; i++)
		{
			ObjMasked[0].GetComponent<SkinnedMeshRenderer>().materials[i].renderQueue = RenderQueueNumber;
		}
	}


	
	
}