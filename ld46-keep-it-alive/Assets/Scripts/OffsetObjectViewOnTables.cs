using UnityEngine;

public class OffsetObjectViewOnTables : MonoBehaviour
{
	public GameObject View;

	public Vector3 Offset;

	private static string[] Layers = new string[] { "Table" };

	void Start()
	{
		var col = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask(Layers));

		if (col != null)
		{
			View.transform.Translate(Offset);
		}
	}
}
