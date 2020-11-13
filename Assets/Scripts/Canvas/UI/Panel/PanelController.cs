using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public List<GameObject> PanelList;

    private Stack<GameObject> PanelStack;

    private void Awake()
    {
        PanelStack = new Stack<GameObject>();
    }

    public void InstantiatePanel(int index)
    {
        Debug.Log(index);
        Debug.Log(PanelList.Count);
        GameObject newPanel = Instantiate(PanelList[index], gameObject.transform);
        PanelStack.Push(newPanel);
    }

    public void PopPanel()
    {
        GameObject panel = PanelStack.Pop();
        Destroy(panel);
    }
}
