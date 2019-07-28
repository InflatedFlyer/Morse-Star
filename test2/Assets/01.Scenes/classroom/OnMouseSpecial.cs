using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using PixelCrushers.DialogueSystem;
public class OnMouseSpecial : MonoBehaviour, IPointerClickHandler
{
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotspot = new Vector2(100f, 100f);
    private float origin;
    private bool ifChangeToState1;
    private bool ifChangeToState2;
    private float ChangeDegree = 1.0f;
    private float number = 0;
    //private Material mat;
    public bool ifStay = false;
    private void ChangeToState1()
    {
        if (transform.localScale.x <= origin)
        {
            this.transform.localScale += new Vector3(ChangeDegree * number * number, ChangeDegree * number * number, ChangeDegree * number * number);
            if (number <= 0.14)
                number += Time.deltaTime / 2;
            if (number >= 0.14 && number <= 0.16)
                number += Time.deltaTime / 3;
            if (number >= 0.16)
                number += Time.deltaTime;
            //mat.SetFloat("_MainColorTimes", 4.8f);
        }
        else
        {
            ifChangeToState1 = false;
            number = 0;
        }
    }

    private void ChangeToState2()
    {
        if (transform.localScale.x >= 0.4f * origin)
        {
            this.transform.localScale -= new Vector3(ChangeDegree * number * number, ChangeDegree * number * number, ChangeDegree * number * number);
            if (number <= 0.14)
                number += Time.deltaTime / 2;
            if (number >= 0.14 && number <= 0.16)
                number += Time.deltaTime / 3;
            if (number >= 0.16)
                number += Time.deltaTime;
            //mat.SetFloat("_MainColorTimes", 4.8f);
        }
        else
        {
            ifChangeToState2 = false;
            number = 0;
        }
    }
    // Use this for initialization
    void Start()
    {
        origin = transform.localScale.x;
        //transform.localScale = new Vector3(origin * 0.4f, origin * 0.4f, origin * 0.4f);
        ifChangeToState1 = false;
        ifChangeToState2 = false;
        //mat = GetComponent<Renderer>().material;
    }

    private void OnMouseEnter()
    {
        GameObject liming = GameObject.FindWithTag("Character");
        if (liming != null)
        {
            if (DialogueManager.IsConversationActive == false
            /*&& liming.GetComponent<WalkCharacter>().state != WalkCharacter.PersonState.LoseControl*/)
            {
                ifChangeToState1 = true;
                ifChangeToState2 = false;


                Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

                StartCoroutine(Stay());
            }
        }

    }

    private void OnMouseExit()
    {
        ifChangeToState1 = false;
        Cursor.SetCursor(null, hotspot, cursorMode);
        ifChangeToState2 = true;
        StartCoroutine(NoStay());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ifStay)
        {
            Invoke("SetActive", 0.1f);
            ifChangeToState1 = false;
        }




    }


    public IEnumerator Stay()
    {
        while (ifChangeToState1)
        {
            ifStay = true;
            yield return null;
        }
    }

    public IEnumerator NoStay()
    {
        while (!ifChangeToState1)
        {
            ifStay = false;
            yield return null;
        }
    }

    private void SetActive()
    {
        Debug.Log(gameObject.name);
        GameObject go = GameObject.FindWithTag("Character");


        Vector3 collider_center = transform.position + GetComponent<BoxCollider>().center;
        Vector2 collider_size = GetComponent<BoxCollider>().size;

        //if (go != null && Mathf.Abs(collider_center.x - go.transform.position.x) < transform.lossyScale.x * collider_size.x / 2 + 2)
        //{
            EventManager.getInstance().TriggerEvent(gameObject.name);
        //}
    }
    private void SetDisactive()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}