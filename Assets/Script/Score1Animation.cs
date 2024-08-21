using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score1Animation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Lấy Animator component từ GameObject
        animator = GetComponent<Animator>();
    }

    // Hàm để chơi animation
    public void PlayAnimation()
    {
        StartCoroutine(CouroutineAnimation());




    }
    IEnumerator CouroutineAnimation()
    {
        
        animator.SetBool("isMove", true);
        animator.SetBool("isStart", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isStart", false);
        animator.SetBool("isExit", true);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<UIManager>().score += 2000;
    }

}
