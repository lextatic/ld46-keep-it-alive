using UnityEngine;

public class BoxInteraction : MonoBehaviour, IInteractable
{
	private Platform[] allPlatforms;

	private void Awake()
	{
		allPlatforms = FindObjectsOfType<Platform>();

		var itemsList = new ItemType[] {
			ItemType.Bascketball,
			ItemType.Globe,
			ItemType.Book,
			ItemType.Camera,
			ItemType.Rocket,
			ItemType.Microscope,
			ItemType.Erlenmeyer,
			ItemType.Magnet
		};

		for (int i = 0; i < itemsList.Length; i++)
		{
			var temp = itemsList[i];
			int randomIndex = Random.Range(i, itemsList.Length);
			itemsList[i] = itemsList[randomIndex];
			itemsList[randomIndex] = temp;
		}

		for (int i = 0; i < allPlatforms.Length; i++)
		{
			allPlatforms[i].PlatformItemType = itemsList[i];
		}
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
