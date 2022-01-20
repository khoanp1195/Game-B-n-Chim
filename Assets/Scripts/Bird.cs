using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float xSpeed; 
    public float minYspeed;
    public float maxYspeed;

    public GameObject deathVfx; // hiệu ứng máu 
    public GameObject floatingTextPrefab;



    Rigidbody2D m_rb;
    bool m_moveLeftOnStart; // biến này sẽ kiểm tra xem con chim có di chuyển về phía bên trái hay không

    bool m_isDead;// biến kiểm tra con chim chết hay chưa

    private void Awake()
    {
        //trước khi mà game chạy ta phải tham chiếu đến Rigidbody 2d của con chim
        m_rb = GetComponent<Rigidbody2D>();// ta dùng phương thức GetComponent để tham chiếu đến nó và đưa vào thành phần mà chúng ta cần tham chiếu


    }
    private void Start()
    {
        RandomMovingDirection();// khi mà game bắt đầu chạy ta sẽ random
    }
    private void Update()
    {
        //ở đây chúng ta sẽ thay đổi vận tốc của Rigidbody 2d trên con chim
        m_rb.velocity = m_moveLeftOnStart ? // sau dấu chấm hỏi là điều kiện dúng, nếu moveLeftOnStart  =  true thì nó sẽ di chuyển về phía bên trái, còn false di chuyển về hướng bên pahi3
            new Vector2(-xSpeed, Random.Range(minYspeed, maxYspeed)) // bên tay trái

            //sau dấu 2 chấm là điều kiện false
            : new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed)); //bên tay phải (ở đây ta lấy ngẫu nhiên trong khoảng minY và maxY, để lấy ngẫu nhiên ta dùng phương thức Range)
        //  m_rb.velocity = new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed)); // ở đây chúng ta sẽ thay đổi vận tộc rigidbody2d trên con chim

        Flip();
    }


    public void RandomMovingDirection() // ngẫu nhiên hướng di chuyển của con chim
    {
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;// ở đây ta sẽ kiểm tra, nếu mà vị trí x của con chim > 0 có nghĩa bên tay phải thì ta sẽ di chuyển nó về phia bên trái
        // nếu mà nó <= 0 có nghĩa là nó nằm phía bên trái thì ta sẽ di chuyển nó về phía tay phải
    }


    void Flip()
    {
        if (m_moveLeftOnStart) //chúng ta sẽ kiểm tra con chim đó đang di chuyển ở phía bên trái hay ko
        {
            if (transform.localScale.x < 0) return; // kiểm tra nếu cái scale của nó < 0 thì chúng ta sẽ không làm gì cả
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);//còn nếu nó lớn hnơn 0 chúng ta sẽ thay đổi scale


        }
        else
        {
            if (transform.localScale.x > 0) return;// chúng ta sẽ kiếm tra nếu scale mà dương thì chúng ta cũng không làm gì cả

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
    }

    public void Die()
    {
        ShowPoint("+1");
        m_isDead = true; //ở đây chúng ta chết chung ta sẽ m_isDead = true



        GameManager.Ins.BirdKilled++;

        Destroy(gameObject);

        if (deathVfx) //khi con chim chết chúng ta sẽ tạo ra máu bắn ra
            Instantiate(deathVfx, transform.position, Quaternion.identity);


        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
        


    }
    public void ShowPoint(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;



        }
    }
}
