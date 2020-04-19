using UnityEngine;

public enum ItemType
{
	None,
	Bascketball,
	Globe,
	Book,
	Camera,
	Rocket,
	Microscope,
	Erlenmeyer,
	Magnet
}

[System.Serializable]
public struct GameItem
{
	public ItemType ItemType;
	public GameObject Prefab;
}

public class Inventory : MonoBehaviour
{
	public Movement PlayerMovement;

	public float Offset = 1f;

	public Animator PlayerAnimator;

	public ItemType CurrentObjectType { get; set; }

	public GameItem[] ItemPrefabs;

	public void Awake()
	{
		CurrentObjectType = ItemType.None;
	}

	public void GetObject(ItemType objectType)
	{
		CurrentObjectType = objectType;
		PlayerAnimator.SetBool("Carrying", true);
	}

	public void DropObject()
	{
		Instantiate(GetCurrentPrefab(), transform.position + (new Vector3(PlayerMovement.LookDirection.x, PlayerMovement.LookDirection.y, 0) * Offset), Quaternion.identity);
		CurrentObjectType = ItemType.None;
		PlayerAnimator.SetBool("Carrying", false);
	}

	private GameObject GetCurrentPrefab()
	{
		for (int i = 0; i < ItemPrefabs.Length; i++)
		{
			var itemPrefab = ItemPrefabs[i];
			if (itemPrefab.ItemType == CurrentObjectType)
			{
				return itemPrefab.Prefab;
			}
		}

		return null;
	}
}
