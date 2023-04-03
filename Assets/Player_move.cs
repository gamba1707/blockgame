using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//プレイヤーの移動に関するプログラム
public class Player_move : MonoBehaviour
{
    CharacterController controller;
    private Camera maincamera;
    private GameObject Player_t;//Playerをすぐその方向に歩かせるため
    private Animator anim;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 cameraForward = Vector3.zero;
    private static float x, y;
    [SerializeField] private float speed=5F;
    private float gravity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        maincamera = Camera.main;
        Player_t = transform.GetChild(0).gameObject;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力値
        x = Input.GetAxis("Horizontal");    //左右矢印キーの値(-1.0~1.0)
        y = Input.GetAxis("Vertical");      //上下矢印キーの値(-1.0~1.0)
        //アニメーション
        if (x != 0 || y != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        cameraForward = Vector3.Scale(maincamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveDirection = cameraForward * y * speed + maincamera.transform.right * x * speed;
        moveDirection = transform.TransformDirection(moveDirection);
        //動いているときは常に押されている方向を向いてほしい
        if (x != 0 || y != 0) Player_t.transform.localRotation = Quaternion.LookRotation(cameraForward * y + maincamera.transform.right * x);
        moveDirection.y -=gravity;

        Debug.Log(moveDirection.y);
        //最終的に動かす
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trampoline"))
        {
            moveDirection.y = 50f;
            moveDirection.y = 50f;
            moveDirection.y = 50f;
            Debug.Log("kiffnsofo");
            
        }
        
    }

}
