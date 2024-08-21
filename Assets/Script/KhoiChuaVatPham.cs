using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class KhoiChuaVatPham : MonoBehaviour
{
    private float DoNayCuaKhoi = 0.5f;
    private float TocDoNay = 4f;
    private bool DuocNay = true;
    private Vector3 VitriLucDau;
 

    //Cac bien de gan cac Item cho no(Gia su nhu xu,nam,sao..)
    public bool ChuaNam = false;
    public bool ChuaXu = false;
    public bool ChuaSao = false;
    public bool ChuaQuaiVat = false;
    //Cho phep so luong xu hien thi
    public int SoLuongXu = 1;
    //lay Cap Do cua Mario hien tai
    GameObject Mario;
    public float moveDuration = 1f; // Thời gian di chuyển
    public float moveDistance = 1f;
    public GameObject score;
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
   
    private void OnCollisionEnter2D(Collision2D col)
    {
        //Neu nhu doi tuong la Mario va va cham phia duoi khoi vuong
        if (col.collider.tag == "VaCham" && col.contacts[0].normal.y > 0)
        {
           
            VitriLucDau = transform.position;
            KhoiNayLen();

        }
        //Neu nhu doi tuong la Mario va va cham phia duoi khoi vuong
      

    }
    void KhoiNayLen()
    {
        if (DuocNay)
        {
            StartCoroutine(KhoiNay());
            DuocNay = false;
            if (ChuaNam) NamVaHoa();
            else if (ChuaXu) HienThiXu();
            else if (ChuaQuaiVat) QuaiVat();
            else Mario.GetComponent<MarioScript>().TaoAmThanh("ThamLam");
        }
    }
    IEnumerator KhoiNay()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (transform.localPosition.y >= VitriLucDau.y + DoNayCuaKhoi) break;//cho nay len den dinh
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (transform.localPosition.y <= VitriLucDau.y) break;//cho nay len den dinh
            Destroy(gameObject);
            GameObject KhoiRong = (GameObject)Instantiate(Resources.Load("Prefabs/KhoiTrong"));
            KhoiRong.transform.position = VitriLucDau;
            yield return null;
        }
    }

    void NamVaHoa()
    {
        int CapDoHienTai = Mario.GetComponent<MarioScript>().CapDo;
        GameObject Nam = null;
        if (CapDoHienTai == 0)
        {
            Nam = (GameObject)Instantiate(Resources.Load("Prefabs/NamAn"));
        }
        else Nam = (GameObject)Instantiate(Resources.Load("Prefabs/Hoa"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("NamMoc");
        Nam.transform.SetParent(this.transform.parent);
        Nam.transform.localPosition = new Vector2(VitriLucDau.x, VitriLucDau.y + 1f);

    }
    void HienThiXu()
    {
        GameObject DongXu = (GameObject)Instantiate(Resources.Load("Prefabs/XuNay"));
        DongXu.transform.SetParent(this.transform.parent);
        DongXu.transform.localPosition = new Vector2(VitriLucDau.x, VitriLucDau.y + 1f);
        StartCoroutine(XuNayLen(DongXu));
    }
    IEnumerator XuNayLen(GameObject DongXu)
    {
        while (true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (DongXu.transform.localPosition.y >= VitriLucDau.y + 100f) break;//cho nay len den dinh
            yield return null;
        }
        while (true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (DongXu.transform.localPosition.y <= VitriLucDau.y) break;//cho nay len den dinh
            Destroy(DongXu.gameObject);

            yield return null;
        }
    }
    void QuaiVat()
    {
       GameObject QuaiVat = null;
        QuaiVat = (GameObject)Instantiate(Resources.Load("Prefabs/RuaXanh"));
        QuaiVat.transform.SetParent(this.transform.parent);
        QuaiVat.transform.localPosition = new Vector2(VitriLucDau.x, VitriLucDau.y + 1f);
    }
}
