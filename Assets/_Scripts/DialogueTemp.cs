using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class d1s1 : MonoBehaviour
{
    public Text textfield;
    public GameObject background;
    public GameObject UIController;
    public int nextScene;
    string currenttext = "";
    bool canGo = true;
    bool isrunning = false;
    bool isforcedcomplete = false;
    int dialoguecounter = 1;
    void printchar(char c)
    {
        textfield.text = textfield.text + c;
    }
    void dialoguecall(string passstring)
    {
        isrunning = true;
        StartCoroutine(stringcall(passstring));
    }
    IEnumerator stringcall(string dialogue)
    {
        textfield.text = "";
        char[] chardialogue;
        chardialogue = dialogue.ToCharArray();
        for (int x = 0; x < dialogue.Length; x++)
        {
            yield return new WaitForSeconds(.025f);
            printchar(chardialogue[x]);
            if (x == dialogue.Length - 1)
            {
                isrunning = false;
            }
            if (isforcedcomplete == true)
            {
                textfield.text = dialogue;
                isforcedcomplete = false;
                isrunning = false;
                break;

            }
        }
    }
    // Use this for initialization
    void Start()
    {
        //First dialogue display
        currenttext = "You: Another morning, another long day ahead."; //String type
        dialoguecall(currenttext);
    }

    // Update is called once per frame
    void Update()
    {
        //Input Triggers
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.Return)))
        {
            if (isrunning == false)
            {
                dialoguecounter++;
                switch (dialoguecounter)
                {
                    case 2://Second dialogue to display
                        currenttext = "I'm an oncologist at the local city hospital. Or in other words, I treat cancer.";
                        dialoguecall(currenttext);
                        break;
                    case 3:
                        currenttext = "But ironically enough, I need cigarettes to get through the day.";
                        dialoguecall(currenttext);
                        break;
                    case 4:
                        currenttext = "If I get too stressed, I have to have a smoke, or else I shut down.";
                        dialoguecall(currenttext);
                        break;
                    case 5:
                        background.SendMessage("ChangeBackground", 1);
                        isrunning = true;
                        StartCoroutine("SceneChangeWait", 2.0f);
                        break;
                    case 6:
                        currenttext = "I try to smoke in the garage to prevent my kids from picking up the habit.";
                        dialoguecall(currenttext);
                        break;
                    case 7:
                        currenttext = "Your wife enters the garage.";
                        dialoguecall(currenttext);
                        break;
                    case 8:
                        currenttext = "Wife: Morning. Oh you have finished one already? Will you stay and smoke another with me?";
                        dialoguecall(currenttext);
                        break;
                    case 9:
                        currenttext = "You: I dunno hun, I might be late for work…";
                        dialoguecall(currenttext);
                        break;
                    case 10:
                        currenttext = "(What will you do?)";
                        dialoguecall(currenttext);
                        break;
                    case 11://Path 1
                        string[] choices = new string[2];
                        choices[0] = "Risk it";
                        choices[1] = "Go Get Ready";
                        UIController.SendMessage("Decision", choices);
                        isrunning = true;
                        canGo = false;
                        break;
                    case 12:
                        currenttext = "You: Alright, yeah. Hand me the lighter?";
                        dialoguecall(currenttext);
                        break; 
                    case 13:
                        currenttext = "Wife: So Jenna has a dance recital this week.";
                        dialoguecall(currenttext);
                        break;
                    case 14:
                        currenttext = "You: Alright, I will make sure to clear my schedule for it.";
                        dialoguecall(currenttext);
                        break;
                    case 15:
                        currenttext = "...";
                        dialoguecall(currenttext);
                        break;
                    case 16:
                        currenttext = "You two finish your cigarettes in silence.";
                        dialoguecall(currenttext);
                        break;
                    case 17:
                        currenttext = "You: I need to go get ready now. Love you.";
                        dialoguecounter = 18;
                        dialoguecall(currenttext);
                        break;
                    case 18://Path 2
                        currenttext = "You: Yeah, I’m sorry hun, but I have to go get ready for work. I love you and I will see you tonight.";
                        dialoguecall(currenttext);
                        break;
                    case 19:
                        currenttext = "You leave to get ready for work.";
                        dialoguecall(currenttext);
                        break;
                    default:
                        background.SendMessage("ChangeBackground", 1);
                        isrunning = true;
                        StartCoroutine("SceneChangeNext", 2.0f);
                        break;
                }
            }
            else
            {
                if(canGo)
                    isforcedcomplete = true;
            }
        }
    }
    IEnumerator SceneChangeWait(float f)
    {
        yield return new WaitForSeconds(f);
        currenttext = "...";
        dialoguecall(currenttext);
        isrunning = false;
    }
    IEnumerator SceneChangeNext(float f)
    {
        yield return new WaitForSeconds(f);
        UIController.SendMessage("NextScene",nextScene);
    }
    void ButtonOne()
    {
        canGo = true;
        UIController.SendMessage("Decided");
        dialoguecounter = 11;
        currenttext = "...";
        dialoguecall(currenttext);
    }
    void ButtonTwo()
    {
        canGo = true;
        UIController.SendMessage("Decided");
        dialoguecounter = 17;
        currenttext = "...";
        dialoguecall(currenttext);
    }
}
