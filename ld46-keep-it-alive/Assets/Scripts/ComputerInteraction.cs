using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
	private Platform[] allPlatforms;

	public DialogueBox DialogueBox;

	public AudioSource AudioSource;

	public SimpleAudioEvent PCAudio;

	private static string computerName = "<color=#5B774A>Computer:</color>";
	private static string computerTextColor = "#5ED674";
	private static string computerNumberColor = "#F2DB2E";

	private void Awake()
	{
		allPlatforms = FindObjectsOfType<Platform>();
	}

	void IInteractable.Interact()
	{
		int readyCount = 0;
		int objectCount = 0;

		foreach (var currentPlatform in allPlatforms)
		{
			if (currentPlatform.CurrentItemOnPlatform == ItemType.None) continue;

			if (currentPlatform.IsReady)
			{
				readyCount++;
			}

			foreach (var platform in allPlatforms)
			{
				if (currentPlatform.CurrentItemOnPlatform == platform.PlatformItemType)
				{
					objectCount++;
					break;
				}
			}

		}

		PCAudio.Play(AudioSource);

		DialogueBox.ShowDialogueBox(new string[] { $"{computerName}<color=#5ED674> Computing chances of survival...</color>",
			$"{computerName}\n<color={computerNumberColor}>{objectCount}</color><color={computerTextColor}> correct objects.</color>" +
			$"\n<color={computerNumberColor}>{readyCount}</color><color={computerTextColor}> objects on the right position." +
			$"\nChances of survival: <color={computerNumberColor}>{1f - (0.5f - (0.5f * (readyCount/4f))):P}</color><color={computerTextColor}>.</color>"});
	}
}
