using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using DG.Tweening;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private float fallDuration = 2f;
    [SerializeField] private float WaitToMove = 2f;
    [SerializeField] private float scaleDuration = 2f;

    [SerializeField] private bool firstButton = false;
    [SerializeField] private bool scalable = true;
    [SerializeField] private ItemMenu nextItemMenu;

    public UnityEvent OnClickItem;

    [Header("Bounce settings")]
    [SerializeField] private Vector3 jumpOffset;
    [SerializeField] private int jumpCount = 4;
    [SerializeField] private float jumpDuration = 2f;

    
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite overSprite;
    // Start is called before the first frame update
    void Start()
    {
        baseSprite = GetComponent<SpriteRenderer>().sprite;
        if(firstButton)
            Invoke("Move", WaitToMove);
    }

    public void Move()
    {
        transform.DOMove(endPosition.position, fallDuration).OnComplete(Bounce);
    }
    public void Bounce()
    {
        if (nextItemMenu != null)
        {
            nextItemMenu.Move();
        }

        if(scalable)
        {
            transform.DOScaleY(1, scaleDuration);
            transform.DOScaleZ(1, scaleDuration);
            transform.DOScaleX(3.8f, scaleDuration);
        }
       
       

        transform.DOJump(transform.position + jumpOffset, 1, jumpCount, jumpDuration)
            .OnComplete(MoveNextButton);
    }

    
    public void MoveNextButton()
    {

        GetComponent<Collider>().enabled = true;

        //if(nextItemMenu != null)
        //{
        //    nextItemMenu.Move();
        //}
            
    }

    private void OnMouseDown()
    {
        OnClickItem.Invoke();
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = overSprite;
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = baseSprite;
    }

}
