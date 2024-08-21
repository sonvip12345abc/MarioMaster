using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RuaBep : MonoBehaviour
{
    Vector2 ViTriChet;
   // Update is called once per frame
    void Update()
    {
        ViTriChet = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player") && (collision.contacts[0].normal.y < 0))
        {
          
            FindObjectOfType<LoseManager>().currentScore += 200;
            FindObjectOfType<Score2Animation>().PlayAnimation();
            Destroy(gameObject);
            //thay bang game object khac
            GameObject HinhBep = (GameObject)Instantiate(Resources.Load("Prefabs/RuaBep"));
            HinhBep.transform.localPosition = ViTriChet;
            }
        }
    }
