using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
	private Platform[] allPlatforms;

	private void Awake()
	{
		allPlatforms = FindObjectsOfType<Platform>();
	}

	void IInteractable.Interact()
	{
		int readyCount = 0;
		int objectCount = 0;

		foreach (var currentPlatform in allPlatforms)
		{
			if (currentPlatform.CurrentItemOnPlatform == ItemType.None) continue;

			if (currentPlatform.IsReady)
			{
				readyCount++;
			}

			foreach (var platform in allPlatforms)
			{
				if (currentPlatform.CurrentItemOnPlatform == platform.CurrentItemOnPlatform)
				{
					objectCount++;
					break;
				}
			}

		}

		Debug.Log($"ready: {readyCount}, count: {objectCount}");
	}
}
