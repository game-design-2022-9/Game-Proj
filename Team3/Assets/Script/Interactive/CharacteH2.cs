using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueControler))]
public class CharacteH2 : Interactive
{
    private DialogueControler dialogueControler;

    public AudioSource audioSource1;

    private void Awake()
    {
        dialogueControler = GetComponent<DialogueControler>();

    }

    public override void EmptyClicked()
    {
        audioSource1.Play();
        if (isDone)
            dialogueControler.ShowDialogueFinish();
        else
        //对话内容A
            dialogueControler.ShowDialogueEmpty();
    }

    protected override void OnClickedAction()
    {
        //对话内容B
        audioSource1.Play();
        dialogueControler.ShowDialogueFinish();
    }
}
