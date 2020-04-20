using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
	public float MovementSpeed = 5f;

	public Animator PlayerAnimator;

	public AudioSource AudioSource;

	public SimpleAudioEvent FootstepSound;

	public Vector2 LookDirection { get; private set; }

	public bool IsMoving { get; private set; }

	private Vector3 _targetposition;

	private Vector2 _inputDirection;

	private Vector2 _currentPosition;

	private Vector2 _currentDirection;

	public static string[] Layers = new string[] { "Level", "Interaction", "Table" };

	public void PlayFootstepSound()
	{
		FootstepSound.Play(AudioSource);
	}

	void Start()
	{
		transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
		_currentPosition = transform.position;
		PlayerAnimator.SetBool("Walking", false);

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
					PlayerAnimator.SetBool("Walking", true);

					UpdateLookSprite();
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

			if (_inputDirection == Vector2.zero)
			{
				PlayerAnimator.SetBool("Walking", false);
			}

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
			_inputDirection = context.ReadValue<Vector2>();

			if (_inputDirection == Vector2.zero) return;

			if (Mathf.Abs(_inputDirection.x) > Mathf.Abs(_inputDirection.y))
			{
				_inputDirection.x = 1 * Mathf.Sign(_inputDirection.x);
				_inputDirection.y = 0;
			}
			else if (Mathf.Abs(_inputDirection.x) == Mathf.Abs(_inputDirection.y))
			{
				if (_inputDirection.x != LookDirection.x)
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
				_inputDirection.x = 0;
				_inputDirection.y = 1 * Mathf.Sign(_inputDirection.y);
			}

			LookDirection = _inputDirection;

			if (IsMoving) return;

			_turning = true;

			UpdateLookSprite();
		}
		else
		{
			_turning = false;
		}
	}

	private void UpdateLookSprite()
	{
		if (LookDirection.x > 0)
		{
			PlayerAnimator.SetInteger("Direction", 2);
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (LookDirection.x < 0)
		{
			PlayerAnimator.SetInteger("Direction", 2);
			transform.localScale = new Vector3(-1, 1, 1);
		}

		if (LookDirection.y > 0)
		{
			PlayerAnimator.SetInteger("Direction", 1);
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (LookDirection.y < 0)
		{
			PlayerAnimator.SetInteger("Direction", 0);
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void SetLookDirection(Vector2 direction)
	{
		LookDirection = direction;
		UpdateLookSprite();
	}
}
