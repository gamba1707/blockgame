using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�v���C���[�̈ړ��Ɋւ���v���O����
public class Player_move : MonoBehaviour
{
    CharacterController controller;
    private Camera maincamera;
    private GameObject Player_t;//Player���������̕����ɕ������邽��
    private Animator anim;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 cameraForward = Vector3.zero;
    private static float x, y;
    [SerializeField] private float speed=5F;
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
        //���͒l
        x = Input.GetAxis("Horizontal");    //���E���L�[�̒l(-1.0~1.0)
        y = Input.GetAxis("Vertical");      //�㉺���L�[�̒l(-1.0~1.0)
        //�A�j���[�V����
        if (x != 0 || y != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        cameraForward = Vector3.Scale(maincamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        moveDirection = cameraForward * y * speed + maincamera.transform.right * x * speed;
        moveDirection = transform.TransformDirection(moveDirection);
        //�����Ă���Ƃ��͏�ɉ�����Ă�������������Ăق���
        if (x != 0 || y != 0) Player_t.transform.localRotation = Quaternion.LookRotation(cameraForward * y + maincamera.transform.right * x);
        //�ŏI�I�ɓ�����
        controller.Move(moveDirection * Time.deltaTime);

    }

}
