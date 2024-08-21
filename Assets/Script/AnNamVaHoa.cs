using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnNamVaHoa : MonoBehaviour
{

    GameObject Mario;
    public GameObject score;
   
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player"))
        {
            if (Mario.GetComponent<MarioScript>().CapDo < 3)
            {
                FindObjectOfType<Score1Animation>().PlayAnimation();
                FindObjectOfType<LoseManager>().currentScore += 2000;
              
                Mario.GetComponent<MarioScript>().CapDo += 2;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Destroy(gameObject);
            }
        }
    }
}

    

