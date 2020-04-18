using UnityEngine;

public class BoxInteraction : MonoBehaviour, IInteractable
{
	private Platform[] allPlatforms;

	private void Awake()
	{
		allPlatforms = FindObjectsOfType<Platform>();
	}

	void IInteractable.Interact()
	{
		if (CheckPlatforms())
		{
			Debug.Log("Victory");
		}
		else
		{
			Debug.Log("Not Yet");
		}
	}

	private bool CheckPlatforms()
	{
		foreach (var platform in allPlatforms)
		{
			if (!platform.IsReady) return false;
		}

		return true;
	}
}
