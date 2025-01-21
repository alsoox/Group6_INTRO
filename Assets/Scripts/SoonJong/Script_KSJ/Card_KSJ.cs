using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_KSJ : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public Animator anim;
    public int idx = 0;
    public SpriteRenderer frontImage;
    AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }
    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);//오디오소스가 겹치지않고 한번만 재생
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }
    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }
    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }
}

