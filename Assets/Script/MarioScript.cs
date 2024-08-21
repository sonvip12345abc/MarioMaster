using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioScript : MonoBehaviour
{
    private float VanToc = 7;
    private float VanTocToiDa = 12f;//Van toc toi da khi giu phim Z-Chay nhanh
    private float TocDo = 0;//Toc do cua Mario
    public bool DuoiDat = true;//Kiem tra Mario co o duoi dat khong
    private bool ChuyenHuong = false;//Xem Mario co dang chuyen huong khong
    private float NhayCao = 450;//Lay toc do nhay cua Mario
    private float NhayThap = 5;//Ap dung khi MArio nhay thap, nhan nhanh va buong phim X
    private float RoiXuong = 5;//Luc hut roi xuong cua MArio
    private bool QuayPhai = true;//Kiem tra xem Mario quay ve phia nao
    private float KTGiuPhim = 0.2f;
    private float TGGiuPhim = 0;
    private Rigidbody2D r2d;
    private Animator HoatHoa;
    private Vector3 VitriLucDau2;
    public int winnumber;
   //Hien thi cap do va do lon cua Mario
    public int CapDo = 0;
    //Vi tri luc Mario chet
    private Vector2 VitriChet;
    public bool BienHinh = false;
    private AudioSource AmThanh;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        HoatHoa = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
     
      

    }
   // Update is called once per frame
    void Update()
    {
        
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        BanDanVaTangToc();
        if(BienHinh==true)
        {
            switch (CapDo)
            {
                case 0:
                    {
                        StartCoroutine(MarioThuNho());
                        TaoAmThanh("MarioNhoDi");
                        BienHinh = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioThuNho());
                        TaoAmThanh("MarioNhoDi");
                        BienHinh = false;
                        break;
                    }

                case 2:
                    {
                        StartCoroutine(MarioAnNam());
                        TaoAmThanh("MarioLonLen");
                        BienHinh = false;
                        break;
                    }
               
                case 4:
                    {
                        StartCoroutine(MarioAnHoa());
                        TaoAmThanh("MarioLonLen");
                        BienHinh = false;
                        break;
                    }
                default:BienHinh = false; 
                break ;
            }
        }
        if(gameObject.transform.position.y<-8f)
        {
            MarioChet();
            
        }
       
    }
    private void FixedUpdate()
    {
        DiChuyen();
        
    }
    void DiChuyen()
    {
        //Chon 1 phim di chuyen cho Mario(Phim <- va -> hoac a d)
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");//hien khoang -1 den 1
        r2d.velocity = new Vector2(VanToc * PhimNhanPhaiTrai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMatMario();
    }
    void HuongMatMario()
    {
        //Neu mario khong quay phai thi
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;

        transform.localScale = HuongQuay;
        if (TocDo > 0.8) StartCoroutine(MarioChuyenHuong());

    }
    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DuoiDat == true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            TaoAmThanh("MarioNhay");
            DuoiDat = false;
        }
        //Ap dung luc hut trai dat cho Mario-roi xuong nhanh hon
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }
    public void BatNhay()
    {
        r2d.AddForce((Vector2.up) * 1500);
        TaoAmThanh("MarioNhay");
        DuoiDat = false;
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }
    
    //Ban dan va chay nhanh hon
    void BanDanVaTangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim == KTGiuPhim)
            {
                VanToc = VanToc * 1.01f;
                if (VanToc > VanTocToiDa) VanToc = VanTocToiDa;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))//Buong phim
        {
            VanToc = 7f;
            TGGiuPhim = 0;
        }
    }

    //Thay doi do lon cua Mario
    IEnumerator MarioAnNam()
    {
       
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }
    IEnumerator MarioAnHoa()
    {
       
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }
    IEnumerator MarioThuNho()
    {
       
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }

    public void TaoAmThanh(string FileAmThanh)
    {
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + FileAmThanh));
    }

    public void MarioChet()
    {
            VitriChet = transform.localPosition;
            GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
            MarioChet.transform.localPosition = VitriChet;
            Destroy(gameObject);
        PlayerPrefs.DeleteKey("CapDo");


    }
    //Mario thắng
    IEnumerator ChienThang()
    {
        TaoAmThanh("10Diem");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void QuaMan()
    {
        StartCoroutine(ChienThang());
    }
    IEnumerator Win()
    {
        TaoAmThanh("10Diem");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(4);
        PlayerPrefs.DeleteKey("CapDo");

    } public void HoanThanh()
    {
        StartCoroutine(Win());
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
       if (col.collider.tag == "Thang")//thắng
        {
           
            QuaMan();
        }
        else
       if (col.collider.tag == "Win")//thắng
        {

            HoanThanh();
        }
    }
    

 }
