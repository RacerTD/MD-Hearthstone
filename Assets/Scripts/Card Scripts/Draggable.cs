using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Code nur für Drag and Drop Funktion = 1
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler // 1
{
    public Transform parentToReturnTo = null;// sets the parent (Hand)
    public bool Dragable
    {
        set
        {
            _draggable = value;
            //Debug.Log("Dragset");
        }
        get
        {
            return _draggable;
        }
    }
    public bool _draggable = true;
    public bool setsDraggableFalse = false;

    //durch den dragOffset kann die Karte überall "angefasst" und bewegt werden
    public Vector3 dragOffset;

    public GameObject gameManager;
    public GameObject manPower;
    public GameObject mana;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        manPower = GameObject.Find("ManPower");
        mana = GameObject.Find("Mana");
    }


    public void OnBeginDrag(PointerEventData eventData) // 1
    {


        //Debug.Log("0");
        if (CheckForGamestate())
        {
            //draggable = true;
            //Debug.Log("1");
            if (Dragable)
            {
                //Debug.Log("2");
                parentToReturnTo = this.transform.parent;                       // Wenn die angewählte Karte aus der Hand geschoben wird,
                this.transform.SetParent(this.transform.parent.parent);         // Ordnen sich die übrigen neu an.
                gameManager.GetComponent<GameManager>().cardInHand = gameObject.GetComponent<OneCardManager>().cardAsset.cardType;

                // Die Raycasts werden zum Zeiger durch die Karte (CanvasGroup) nicht mehr geblockt. 
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                dragOffset = this.transform.position - new Vector3(eventData.position.x, eventData.position.y, 0);
                transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            Dragable = false;
        }

    }
    public void OnDrag(PointerEventData eventData) // 1
    {
        if (CheckForGamestate())
        {
            //draggable = true;
            if (Dragable)
            {
                //Debug.Log("OnDrag"); // 1
                this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0) + dragOffset; // 1
            }
        }
        else
        {
            Dragable = false;
        }
    }
    public void OnEndDrag(PointerEventData eventData) // 1
    {
        //Debug.Log("0c");
        if (CheckForGamestate())
        {
            Dragable = true;
            gameManager.GetComponent<GameManager>().cardInHand = CardType.Nothing;

            if (setsDraggableFalse)
            {
                setsDraggableFalse = false;
                Dragable = false;
            }
            //Debug.Log("OnEndDrag"); // 1
            this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0) + dragOffset; ;
            this.transform.SetParent(parentToReturnTo);                     // Die Karte wird beim loslassen zurück in die Hand eingeordnet
            GetComponent<CanvasGroup>().blocksRaycasts = true;              // Raycasts werden wieder durch Karte geblockt.

        }
        else
        {
            Dragable = false;
        }
    }
    private bool CheckForGamestate()
    {
        if ((gameManager.GetComponent<GameManager>().gameState == GameManager.GameState.PlayerIdle) ||
            (gameManager.GetComponent<GameManager>().gameState == GameManager.GameState.PlayerCardInHand) ||
            (gameManager.GetComponent<GameManager>().gameState == GameManager.GameState.PlayerAttack) ||
            (gameManager.GetComponent<GameManager>().gameState == GameManager.GameState.PlayerAbility))
        {
            return true;
        }

        return true;
    }
}
