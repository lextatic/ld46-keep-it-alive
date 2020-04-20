using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
	public bool CanInteract { get; set; }

	private IInteractable _interactableTarget;

	public Inventory PlayerInventory;

	public Movement PlayerMovement;

	public static string[] Layers = new string[] { "Level", "Interaction" };

	private void Awake()
	{
		CanInteract = true;
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (!context.performed || PlayerMovement.IsMoving) return;

		var swipePosition = new Vector2(transform.position.x, transform.position.y) + PlayerMovement.LookDirection;
		var col = Physics2D.OverlapPoint(swipePosition, LayerMask.GetMask(Layers));

		if (PlayerInventory.CurrentObjectType == ItemType.None)
		{
			if (col == null) return;

			_interactableTarget = col.GetComponent<IInteractable>();

			if (_interactableTarget != null)
			{
				_interactableTarget.Interact();
			}
		}
		else
		{
			if (col != null) return;

			PlayerInventory.DropObject();
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		_interactableTarget = collision.GetComponent<IInteractable>();
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		_interactableTarget = null;
	}
}
