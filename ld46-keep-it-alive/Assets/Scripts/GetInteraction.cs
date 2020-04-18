using UnityEngine;

public class GetInteraction : MonoBehaviour, IInteractable
{
	public ItemType ItemType;

	private Inventory _playerInventory;

	void IInteractable.Interact()
	{
		if (_playerInventory == null)
		{
			Debug.LogError("PlayerInventory should never be null on Interactions");
			return;
		}

		_playerInventory.GetObject(ItemType);

		Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (_playerInventory != null) return;

		_playerInventory = collision.GetComponent<Inventory>();
	}
}
