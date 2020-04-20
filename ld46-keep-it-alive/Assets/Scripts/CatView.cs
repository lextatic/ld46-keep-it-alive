using UnityEngine;

public class CatView : MonoBehaviour
{
	public Animator CatViewAnimator;

	public void Jump()
	{
		CatViewAnimator.SetInteger("State", 2);
	}

	public void Fall()
	{
		CatViewAnimator.SetInteger("State", 3);
	}

	public void Idle()
	{
		CatViewAnimator.SetInteger("State", 0);
	}
}
