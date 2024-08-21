using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaBay : MonoBehaviour
{
    public float VanTocVat;
    public bool DiChuyenTrai = true;
    private void FixedUpdate()
    {
        Vector2 DiChuyen = transform.localPosition;
        if (DiChuyenTrai)
        {
            DiChuyen.x -= VanTocVat * Time.deltaTime;
            DiChuyen.y += VanTocVat * Time.deltaTime;
        }
        else
        {
            DiChuyen.x += VanTocVat * Time.deltaTime;
            DiChuyen.y += VanTocVat * Time.deltaTime;
        }
        transform.localPosition = DiChuyen;//gan lai moi di duoc
    }
    //Xu li va cham
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player" && collision.contacts[0].normal.x > 0)
        {
            DiChuyenTrai = true;
            QuayMat();
        }
        else if (collision.collider.tag != "Player" && collision.contacts[0].normal.x < 0)
        {
            DiChuyenTrai = false;
            QuayMat();
        }
    }
    void QuayMat()
    {
        //Doi huong mat cua Vat khi doi huong
        DiChuyenTrai = !DiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
    }

}
