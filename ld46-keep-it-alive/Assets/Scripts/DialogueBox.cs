using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
	public GameObject Panel;

	public TMPro.TextMeshProUGUI DialogueText;

	public PlayerInput PlayerInput;

	public Animator CanvasAnimator;

	public AudioSource AudioSource;

	public SimpleAudioEvent ChatBeepAudio;

	public bool EndGame { get; set; }

	private bool canRestart;

	private Queue<string> Texts;

	private void Awake()
	{
		Texts = new Queue<string>();
		DialogueText.text = "";
		EndGame = false;
		canRestart = false;
	}

	public void ShowDialogueBox(string[] texts)
	{
		PlayerInput.SwitchCurrentActionMap("DialogActions");
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
			DialogueText.text = "";

			if (!EndGame)
			{
				PlayerInput.SwitchCurrentActionMap("PlayerActions");
			}
			else
			{
				CanvasAnimator.SetTrigger("End");
			}

			return;
		}

		ChatBeepAudio.Play(AudioSource);

		DialogueText.text = Texts.Dequeue();
	}

	public void CanRestart()
	{
		canRestart = true;
	}

	public void OnNext(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (EndGame && canRestart)
			{
				SceneManager.LoadScene(0);
				return;
			}

			ShowNextDialogue();
		}
	}
}
