using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float fireRate; // biến độ trễ
    float m_curFireRate;// lưu lại FireRate hiện tại

    public GameObject viewFinder;// ta khai báo một biến viewFinder
    GameObject m_viewFinderClone;


    bool m_isShooted;//biến để kiểm tra đã bắn hay chưa


    private void Start()
    {
        if (viewFinder) //khi chương trình bắt đầu chạy chúng ta sẽ kiểm tra nếu viewFinder được tham chiếu chúng ta sẽ tạo ra một GameObject trên Scene của chúng ta
           m_viewFinderClone =  Instantiate(viewFinder, Vector3.zero, Quaternion.identity);// Vector3.zero tòa độ (0,0,0) ở giữa màn hình. Quaternion.identity không xoay
    }
    private void Awake()
    {
        // trước khi chương trình bắt đầu chạy ta sẽ gán m_curFirerate = firerate;
        m_curFireRate = fireRate; // chúng ta sẽ lưu dữ liệu firerate vào thằng m_curFirerate;
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // lấy vị trí con trỏ chuột, chúng ta sẽ chuyển đổi tọa độ ở trên màn hình của người chơi vào sang tọa độ của Unity


        if (Input.GetMouseButtonDown(0) && !m_isShooted) //, khi tham số là 0 sẽ kiểm tra xem chuột phải nhấn hay ko và biến m_isShooted  = false
        {
            Shot(mousePos);
        }
        //chúng ta sẽ gọi phương thức shot



       if(m_isShooted) //Kiểm tra nếu chúng ta đã bắn
        {
            m_curFireRate -= Time.deltaTime;// chúng ta sẽ giảm FireRate này từ từ

            if(m_curFireRate <= 0)
            {
                //nếu firerate <= 0 chúng ta sẽ gán isShooted = false
                m_isShooted = false; // súng chưa dc bắn thì lúc này người chơi có thể bắn được súng
                m_curFireRate = fireRate;// ta sẽ gán thằng firerate này bằng với thằng firerate ban đầu
            }

            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate); // cập nhật firerate
        }

        if (m_viewFinderClone)// chúng ta sẽ kiểm tra mviewFinderClone, nếu mà trên Scene của chúng ta có đối tượng ViewFinderClone hay ko thì chúng ta sẽ cập nhật tọa độ của ống ngấm
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f); // chúng ta sẽ gán vị trí con trỏ chuột vào vị trí của ổng ngấm
    
    }
    void Shot(Vector3 mousePos) // phương thức này sẽ nhận một tham số là một Vector 3 (là vị trí con trỏ chuột)
    {

        m_isShooted = true;// ta gán isShooted = true (nếu người dùng click bắn súng thì isShooted = true )
        Vector3 shootDir = Camera.main.transform.position - mousePos; // hướng từ Camera đến con trỏ chuột
        
        
        shootDir.Normalize(); //  giảm ngược vector bằng phương thức Normalize() (giảm ngược để cho máy tính dễ tính toán)

        // tạo ra mảng Raycast
        //Raycast là một đường thẳng, khi đường thẳng này chạm bất cứ thứ gì trên đường di của nó thì nó
        // sẽ trả về một RaycastHit. Trong raycasthead đó chúng ta sẽ biết đối tượng đó tên là gì hay kiểu gì và vị vị trí như thế nào.. chúng ta sẽ biết tất cả thông tin khi cái đường Raycast này chạm phải một vật nào đó
        //Raycast rất phổ biến khi chúng ta làm Game bắn súng,
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);  // Ở đây chúng ta sẽ lấy tất cả thông  tin khi thằng raycast này chạm phải.
        //vị trí bắt đầu của chúng ta là vị trí con trỏ chuột mousePos, và hướng của chúng ta là shootDir(hướng từ Camera đến con trỏ chuột)

         

        //Chúng ta sẽ kiểm tra nếu mảng hit này khác null và phân tử hit.lenght > 0 
        if(hits != null && hits.Length > 0)
        {
            // ở đây ta sẽ tao ra một vọng lặp để duyệt mảng hit này.
            for(int i = 0; i< hits.Length; i++)
            {
                // trong dây ta sẽ có một RaycastHit
                RaycastHit2D hit = hits[i];
                
                //chúng ta sẽ kiểm tra
            
                if( hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    // Debug.Log(hit.collider.name);
                    // nếu khoảng cách giữa vật mà Raycast chạm phải đến con trỏ chuột nhỏ hơn 0.4
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird)// nếu mà vật chạm phải Raycast có chứa thành phần là Bird
                    {   
                        bird.Die(); // chúng ta sẽ gọi phương thức Die
                    }
                 }
            }    
        }
        CineController.Ins.ShakeTrigger();

        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }

}
