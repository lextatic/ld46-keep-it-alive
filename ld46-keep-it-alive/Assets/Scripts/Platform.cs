using UnityEngine;

public class Platform : MonoBehaviour
{
	public ItemType PlatformItemType;
	public bool IsReady { get; private set; }

	public ItemType CurrentItemOnPlatform;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var item = collision.GetComponent<GetInteraction>();

		CurrentItemOnPlatform = item.ItemType;

		if (PlatformItemType == item.ItemType)
		{
			IsReady = true;
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (IsReady)
		{
			var item = collision.GetComponent<GetInteraction>();

			if (PlatformItemType == item.ItemType)
			{
				IsReady = false;
			}
		}

		CurrentItemOnPlatform = ItemType.None;
	}
}
