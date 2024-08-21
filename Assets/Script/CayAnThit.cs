using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CayAnThit : MonoBehaviour
{
    private float distance = 1.45f; // Khoảng cách di chuyển theo phương y
    private float duration = 2f; // Thời gian để di chuyển và quay về vị trí cũ
    GameObject Mario;
    private Vector3 originalPosition; // Vị trí ban đầu của đối tượng

    void Start()
    {
        originalPosition = transform.position; // Lưu trữ vị trí ban đầu
        MoveAndReturn(); // Bắt đầu di chuyển và quay trở lại
    }
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    void MoveAndReturn()
    {
        // Di chuyển đối tượng lên trên theo phương y bằng DOTween
        transform.DOMoveY(transform.position.y + distance, duration)
            .OnComplete(() => // Khi di chuyển hoàn tất
            {
                // Di chuyển đối tượng trở lại vị trí ban đầu và lặp lại
                transform.DOMove(originalPosition, duration).OnComplete(() =>
                {
                    MoveAndReturn();
                });
            });
    }
    IEnumerator LonNho()
    {
        Mario.GetComponent<MarioScript>().CapDo = 1;
        Mario.GetComponent<MarioScript>().BienHinh = true;
        yield return new WaitForSeconds(0.65f);
        Mario.GetComponent<MarioScript>().CapDo = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player"))
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

            else if (Mario.GetComponent<MarioScript>().CapDo == 0)
            {
                Mario.GetComponent<MarioScript>().MarioChet();
            }
        }
    }

}
