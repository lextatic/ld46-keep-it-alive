using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBox : MonoBehaviour
{
	public GameObject Panel;

	public TMPro.TextMeshProUGUI DialogueText;

	public PlayerInput PlayerInput;

	private Queue<string> Texts;

	private void Awake()
	{
		Texts = new Queue<string>();
		DialogueText.text = "";
	}

	public void ShowDialogueBox(string[] texts)
	{
		Panel.SetActive(true);

		for (int i = 0; i < texts.Length; i++)
		{
			Texts.Enqueue(texts[i]);
		}

		ShowNextDialogue();
	}

	private void ShowNextDialogue()
	{
		if (Texts.Count == 0)
		{
			Panel.SetActive(false);
			PlayerInput.SwitchCurrentActionMap("PlayerActions");
			DialogueText.text = "";
			return;
		}

		DialogueText.text = Texts.Dequeue();
	}

	public void OnNext(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			ShowNextDialogue();
		}
	}
}
