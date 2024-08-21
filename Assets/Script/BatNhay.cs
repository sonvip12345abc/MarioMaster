using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNhay : MonoBehaviour
{
    private Animator animator;
    GameObject Mario;
    private void Start()
    {
        // Lấy Animator component từ GameObject
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player") && (collision.contacts[0].normal.y < 0))
        {

            Mario.GetComponent<MarioScript>().BatNhay();
            PlayAnimation();
        }


    }
    public void PlayAnimation()
    {
        StartCoroutine(CouroutineAnimation());




    }
    IEnumerator CouroutineAnimation()
    {
        animator.SetBool("DangNhay", true);
        yield return null;
    }
}
