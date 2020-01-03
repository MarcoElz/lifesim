using UnityEngine;
using LifeSim.Core.Interaction;
using LifeSim.Core.Dialogues;
using UnityEngine.SceneManagement;


public class DialoguePerArea : MonoBehaviour 
{
    [SerializeField] SceneDialogue[] dialoguePerScene;
    [SerializeField] Dialogue defaultDialogue;

    InteractDialogue interactDialogue;

	private void Awake()
	{
        interactDialogue = GetComponent<InteractDialogue>();
	}

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void ChangedActiveScene(Scene current, Scene next)
    {
        if (interactDialogue == null)
            return;


        if (current.name == null)
        {
            Debug.Log("New scene: " + next.name);
            interactDialogue.ChangeDialogue(GetDialogueBySceneName(next.name));
        }
    }

    Dialogue GetDialogueBySceneName(string name)
    {
        for (int i = 0; i < dialoguePerScene.Length; i++)
        {
            if (dialoguePerScene[i].scene.Equals(name))
                return dialoguePerScene[i].dialogue;
        }

        return defaultDialogue;


    }

}

[System.Serializable]
public class SceneDialogue
{
    public string scene;
    public Dialogue dialogue;
}
    
