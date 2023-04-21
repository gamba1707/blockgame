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
    private Vector3 gravityDirection = Vector3.zero;
    private Vector3 cameraForward = Vector3.zero;
    private static float x, y;
    [SerializeField] private float speed=5F;
    private float gravity= -0.98f;
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
        //y = Input.GetAxis("Vertical");      //�㉺���L�[�̒l(-1.0~1.0)
        //�A�j���[�V����
        if (x != 0 || y != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);
        if (controller.isGrounded)//���n���i���Ԃ�j
        {
            gravityDirection = new Vector3(0, 0, 0);
            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            cameraForward = Vector3.Scale(maincamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
            moveDirection = cameraForward * y * speed + maincamera.transform.right * x * speed;
            moveDirection = transform.TransformDirection(moveDirection);
        }
        else//�󒆂ɂ���ꍇ
        {
            Vector3 Direction = ((maincamera.transform.right * x * 3f) + (maincamera.transform.forward * y * 3f));
            gravityDirection.y -= 5f * Time.deltaTime*0.01f;
            moveDirection = new Vector3(Direction.x, gravityDirection.y + moveDirection.y, Direction.z);
            moveDirection = transform.TransformDirection(moveDirection);
        }
        //�����Ă���Ƃ��͏�ɉ�����Ă�������������Ăق���
        if (x != 0 || y != 0) Player_t.transform.localRotation = Quaternion.LookRotation(cameraForward * y + maincamera.transform.right * x);
        
        //�ŏI�I�ɓ�����
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("trampoline"))
        {
            //�L�����N�^�[�R���g���[���[�̐ڒn���x���Ⴂ�̂ł��邯�ǂ����ŏ�����
            gravityDirection = new Vector3(0, 0, 0);
            moveDirection.y = 6f;

            Debug.Log("kiffnsofo"+ moveDirection.y);
            
        }
        
    }

}
