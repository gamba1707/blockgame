using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class clime : MonoBehaviour
{
    private Player_move player_move;
    private Animator anim;

    private Vector3 blockpos;
    // Start is called before the first frame update
    void Start()
    {
        player_move= transform.root.gameObject.GetComponent<Player_move>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cube"))
        {
            blockpos=other.transform.position;
            Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z), transform.forward);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z), transform.forward * 1.0f, Color.red);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 1.0f))
            {
                Debug.Log("OK");
                anim.SetTrigger("clime");
            }
        }
    }

    //�A�j���[�V�����̃C�x���g����Ăяo�����
    //�F�X�Ȍ��ˍ������疳�������W���グ�ċ삯�オ�����悤�Ɍ����Ă�
    IEnumerator clime_move()
    {
        float f=0f;
        //�����ɂ���Đi�ޕ��������߂邽�߁i������Ȃ�}�C�i�X�A�k�����Ȃ�v���X�j
        int rotate = (transform.eulerAngles.y < 90 || transform.eulerAngles.y > 270) ? 1 : -1;
        Vector3 startpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 endpos = new Vector3(blockpos.x, transform.position.y + 1.5f, blockpos.z);
        Debug.Log(startpos);
        Debug.Log(endpos);
        //�삯�オ��܂ŌJ��Ԃ��i�⊮�҂��j
        while (f <= 1.0f)
        {
            Debug.Log("?");
            transform.root.position = Vector3.Slerp(startpos, endpos, f);
            f += 0.01f;
            yield return null;  
        }
        /*�q�I�u�W�F�N�g�̍��W��o��Ƃ������ϊ������ēo��؂������
        �e�I�u�W�F�N�g�ɔ��f�����Ďq�I�u�W�F�N�g�̒l������������
        Vector3 playerpos =transform.root.position;
        playerpos.x += transform.localPosition.x;
        playerpos.y += transform.localPosition.y;
        playerpos.z += transform.localPosition.z;
        transform.root.position= playerpos;
        transform.localPosition= new Vector3(0f,0f,0f);*/
    }

}
