﻿using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class RxObject : MonoBehaviour, IPointerDownHandler
{
    EventSystem eventSystem;
    OutlineEffect outlineEffect;

    public UnityEvent OnElemCombinSuccess;

    public bool isHiden = true;

    void Awake()
    {
        outlineEffect = GetComponent<OutlineEffect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (eventSystem == null)
        {
            Debug.Log("echec");
        }

        Hide(true);
    }

    public void Hide(bool bFlag)
    {
        if (bFlag)
        {
            GetComponent<Image>().color = Color.black;
        }
        else
        {
            Debug.Log(GetComponent<Image>().color.ToString());
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tag == eventData.lastPress.tag)
        {
            eventData.lastPress.GetComponent<Item>().ValidItem();
            
            for (int i = 0; i < transform.parent.parent.childCount; i++)
            {
                for (int j = 0; j < transform.parent.parent.GetChild(i).childCount; j++)
                {
                    /*Fucking game jam, asshole code*/
                    if (transform.parent.parent.GetChild(i).GetChild(j).tag == tag)
                    {
                        transform.parent.parent.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        transform.parent.parent.GetChild(i).GetChild(j).GetComponent<RxObject>()?.Hide(false);
                        //transform.parent.parent.GetChild(i).GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
            
            Hide(false);

            OnElemCombinSuccess?.Invoke();
        }
    }
}
