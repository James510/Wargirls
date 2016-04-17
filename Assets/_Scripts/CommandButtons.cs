using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandButtons : MonoBehaviour
{
    public bool forwards, stop, reverse, retreat;
    private GameObject target;
    private Button loadButton;
    private GameObject parent;

    void Awake()
    {
        loadButton = GetComponent<Button>();
        SetAction();
    }

    public void SetAction()
    {
        
        loadButton.onClick.RemoveAllListeners();
        //if(target != null)
        //{
            if (forwards)
                loadButton.onClick.AddListener(() => { target.SendMessage("GoForwards"); });
            if (stop)
                loadButton.onClick.AddListener(() => { target.SendMessage("GoStop"); });
            if (reverse)
                loadButton.onClick.AddListener(() => { target.SendMessage("GoBackwards"); });
            if (retreat)
                loadButton.onClick.AddListener(() => { target.SendMessage("GoRetreat"); });

        //}
    }

    public void SetTarget(GameObject tgt)
    {
        target = tgt;
    }
}
