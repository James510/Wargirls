using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandButtons : MonoBehaviour
{
    public bool forwards, stop, reverse, retreat;
    private GameObject shipBuilderCore;
    private Button loadButton;
    private GameObject parent;

    void Start()
    {

    }

    void Awake()
    {
        loadButton = GetComponent<Button>();
        SetAction();
    }

    public void SetAction()
    {
        
        loadButton.onClick.RemoveAllListeners();
        
        if (forwards)
            loadButton.onClick.AddListener(() => { transform.root.SendMessage("GoForwards"); });
        if (stop)
            loadButton.onClick.AddListener(() => { transform.root.SendMessage("GoStop"); });
        if (reverse)
            loadButton.onClick.AddListener(() => { transform.root.SendMessage("GoBackwards"); });
        if (retreat)
            loadButton.onClick.AddListener(() => { transform.root.SendMessage("GoRetreat"); });
    }
}
