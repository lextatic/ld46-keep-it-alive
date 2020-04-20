using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxInteraction : MonoBehaviour, IInteractable
{
	private Platform[] allPlatforms;

	public DialogueBox DialogueBox;

	public Animator BoxAnimator;

	public Movement PlayerMovement;

	public PlayerInput PlayerInput;

	public AudioSource AudioSource;

	public SimpleAudioEvent CatJumpSound;

	public SimpleAudioEvent BoxSound;

	public SimpleAudioEvent CatLandSound;

	private static string shrodingerName = "<color=#A0A0A0>Scshodinger:</color>";
	private static string shrodingerTextColor = "#FFFFFF";
	private static string catName = "<color=#00FFFF>Milton</color>";

	public void PlayCatJumpSound()
	{
		CatJumpSound.Play(AudioSource);
	}

	public void PlayBoxSound()
	{
		BoxSound.Play(AudioSource);
	}

	public void PlayCatLandSound()
	{
		CatLandSound.Play(AudioSource);
	}

	private void Awake()
	{
		allPlatforms = FindObjectsOfType<Platform>();

		var itemsList = new ItemType[] {
			ItemType.Bascketball,
			ItemType.Globe,
			ItemType.Book,
			ItemType.Camera,
			ItemType.Rocket,
			ItemType.Microscope,
			ItemType.Erlenmeyer,
			ItemType.Magnet
		};

		for (int i = 0; i < itemsList.Length; i++)
		{
			var temp = itemsList[i];
			int randomIndex = Random.Range(i, itemsList.Length);
			itemsList[i] = itemsList[randomIndex];
			itemsList[randomIndex] = temp;
		}

		for (int i = 0; i < allPlatforms.Length; i++)
		{
			allPlatforms[i].PlatformItemType = itemsList[i];
		}
	}

	private void Start()
	{
		StartCoroutine(StartStory());
		PlayerMovement.SetLookDirection(new Vector2(-1, 0));
	}

	private IEnumerator StartStory()
	{
		yield return new WaitForSeconds(2);

		BoxAnimator.SetTrigger("JumpIn");

		yield return new WaitForSeconds(2);

		PlayerMovement.SetLookDirection(new Vector2(1, 0));
		yield return new WaitForSeconds(.2f);
		PlayerMovement.SetLookDirection(new Vector2(-1, 0));
		yield return new WaitForSeconds(.3f);
		PlayerMovement.SetLookDirection(new Vector2(0, 1));

		yield return new WaitForSeconds(1);

		DialogueBox.ShowDialogueBox(new string[] { $"{shrodingerName}<color={shrodingerTextColor}> Oh great... I was so happy with this new science toy that I completly forgot about {catName} and his love for boxes.</color>",
				$"{shrodingerName}<color={shrodingerTextColor}> Now my cat is both <color=red>DEAD</color> and <color=green>ALIVE</color>... again...</color>",
				$"{shrodingerName}<color={shrodingerTextColor}> I can't check the <color=yellow>box</color> until I'm completely sure it'll keep him alive.",
				$"{shrodingerName}<color={shrodingerTextColor}> Fortunatly I can influence the superposition of states with the environment around the <color=yellow>box</color>.</color>",
				$"{shrodingerName}<color={shrodingerTextColor}> All I have to do is <color=yellow>move objects</color> around to the <color=yellow>right spots</color>.</color>",
				$"{shrodingerName}<color={shrodingerTextColor}> The <color=yellow>computer program</color> I wrote the other day could help me with that." });
	}

	void IInteractable.Interact()
	{
		if (CheckPlatforms())
		{
			StartCoroutine(EndStory());
		}
		else
		{
			DialogueBox.ShowDialogueBox(new string[] { $"{shrodingerName}<color={shrodingerTextColor}> I'm not opening this box until I'm sure {catName} is safe. I have to keep it alive!</color>",
				$"{shrodingerName}<color={shrodingerTextColor}> I should check the computer first.</color>" });
		}
	}

	private IEnumerator EndStory()
	{
		PlayerInput.DeactivateInput();
		BoxAnimator.SetTrigger("JumpOut");

		yield return new WaitForSeconds(2);

		DialogueBox.EndGame = true;
		DialogueBox.ShowDialogueBox(new string[] { $"{shrodingerName}<color={shrodingerTextColor}> Phew! You gotta stop doing that {catName}.</color>"
				});
	}

	private bool CheckPlatforms()
	{
		foreach (var platform in allPlatforms)
		{
			if (!platform.IsReady) return false;
		}

		return true;
	}
}
