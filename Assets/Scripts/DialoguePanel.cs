using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePanel : MonoBehaviour
{
    public Image background;
    public TMP_Text dialogue;
    public TMP_Text continue_text;
    public AudioSource speeker;

    Image speekerImage;

    public void Awake()
    {
        speekerImage = speeker.gameObject.GetComponent<Image>();

        background.enabled = false;
        speekerImage.enabled = false;
        continue_text.enabled = false;
        dialogue.text = "";
    }

    public void createDialogue(string speechText, AudioClip sound, float volume)
    {   
        if(speechText != ""){
            background.enabled = true;
            speekerImage.enabled = true;
            continue_text.enabled = true;
            dialogue.text = speechText;
        }else{
            background.enabled = false;
            continue_text.enabled = false;
            dialogue.text = "";
        }

        // if(sound != null && !speeker.isPlaying){
        //     speeker.PlayOneShot(sound, volume);
        // }
    }

    public void endDialogue()
    {
        background.enabled = false;
        speekerImage.enabled = false;
        continue_text.enabled = false;
        dialogue.text = "";
    }

}
