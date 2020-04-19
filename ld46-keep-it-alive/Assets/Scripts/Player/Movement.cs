using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
	public float MovementSpeed = 5f;

	public Vector2 LookDirection { get; private set; }

	public bool IsMoving { get; private set; }

	private Vector3 _targetposition;

	private Vector2 _inputDirection;

	private Vector2 _currentPosition;

	private Vector2 _currentDirection;

	public static string[] Layers = new string[] { "Level", "Interaction" };

	void Start()
	{
		transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
		_currentPosition = transform.position;

		IsMoving = false;
	}

	void FixedUpdate()
	{
		if (!IsMoving)
		{
			if (_inputDirection != Vector2.zero && !_turning)
			{
				var swipePosition = _currentPosition + _inputDirection;

				if (Physics2D.OverlapPoint(swipePosition, LayerMask.GetMask(Layers)) == null)
				{
					_currentDirection = _inputDirection;
					_targetposition = swipePosition;
					IsMoving = true;
				}

				LookDirection = _inputDirection;
			}

		}

		if (IsMoving)
		{
			var distanceLeft = Vector3.Distance(transform.position, _targetposition);
			if (distanceLeft >= MovementSpeed * Time.deltaTime)
			{
				transform.Translate(_currentDirection * MovementSpeed * Time.deltaTime);
			}
			else
			{
				transform.Translate(_currentDirection * distanceLeft);
			}
		}

		if (transform.position == _targetposition)
		{
			_currentPosition = transform.position;
			IsMoving = false;
		}
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		if (context.canceled) _inputDirection = Vector2.zero;
	}

	private bool _turning;

	public void OnTurn(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			_turning = true;

			_inputDirection = context.ReadValue<Vector2>();
			LookDirection = _inputDirection;

			_inputDirection = context.ReadValue<Vector2>();

			if (_inputDirection == Vector2.zero) return;

			if (Mathf.Abs(_inputDirection.x) > Mathf.Abs(_inputDirection.y))
			{
				_inputDirection.x = 1 * Mathf.Sign(_inputDirection.x);
				_inputDirection.y = 0;
			}
			else
			{
				_inputDirection.x = 0;
				_inputDirection.y = 1 * Mathf.Sign(_inputDirection.y);
			}
		}
		else
		{
			_turning = false;
		}
	}
}
