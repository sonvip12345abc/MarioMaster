using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    //Mario bat tu trong 1 khoang thoi gian khi bien hình
    IEnumerator LonNho()
    {
        Mario.GetComponent<MarioScript>().CapDo = 1;
        Mario.GetComponent<MarioScript>().BienHinh = true;
        yield return new WaitForSeconds(0.65f);
        Mario.GetComponent<MarioScript>().CapDo = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player") && ((collision.contacts[0].normal.x >0)|| (collision.contacts[0].normal.x <0)))
        {

            //Thu nho khi cham vao
           if (Mario.GetComponent<MarioScript>().CapDo == 2)
            {
                StartCoroutine(LonNho());
            }
            else if (Mario.GetComponent<MarioScript>().CapDo == 4)
            {
                StartCoroutine(LonNho());
            }

            else if(Mario.GetComponent<MarioScript>().CapDo == 0)
            {
                Mario.GetComponent<MarioScript>().MarioChet();
            }
        }
       
        
    }
}
