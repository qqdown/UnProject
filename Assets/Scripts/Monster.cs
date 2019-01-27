using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1;
    public float move_time = 4;
    public Vector3 size = new Vector3(4, 4, 4);
    public Vector3 center = new Vector3(3, 0, 2);
    private float timer = 0;

    public float disappear_time = 4;
    private float vis_down = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vis_down <= 0)
        {
            MoveRandom();
        }
        else
        {
            CheckVisibility();
        }
    }

    void MoveRandom()
    {
        timer += Time.deltaTime;
        if (timer > move_time)
        {
            Debug.Log(transform.position);
            Quaternion new_rotation;
            Vector3 pos_to_center = transform.position - center;
            if (Mathf.Abs(pos_to_center.x) <= size.x && Mathf.Abs(pos_to_center.z) <= size.z)
            {
                Debug.Log("Random");
                float dir_y = Random.Range(0f, 360f);//取随机数，参数为浮点型，取得随机数就是浮点型
                new_rotation = Quaternion.Euler(0, dir_y, 0);
            }
            else
            {
                Debug.Log("Center");
                Vector3 v = center - transform.position;
                new_rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), v);
            }
            transform.rotation = new_rotation;//旋转指定度数
            timer = 0;//当timer>move_time秒置空，重新生成随机数即改变运动方向
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void CheckVisibility()
    {
        vis_down -= Time.deltaTime;
        if (vis_down <= 0)
        {
            var r = gameObject.GetComponentInChildren<Renderer>();
            r.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerLogic>();
        if (player == null)
        {
            return;
        }
        if (!player.UseItemWithResult(ItemId.BEAR))
        {
            player.SanDown(20);
        }
        else
        {
            var r = gameObject.GetComponentInChildren<Renderer>();
            r.enabled = false;
            vis_down = disappear_time;
        }
    }

}
