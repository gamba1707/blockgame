using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    private Animator anim;
    //���������邱�Ƃ��o���邩�i��Ƀu���b�N������ꍇfalse�j
    [SerializeField] private bool pointer = true;
    //�ʏ펞�̏��ƑI�����̃}�e���A��
    [SerializeField] Material[] blockmaterials = new Material[3];
    //�v���n�u�̃u���b�N
    [SerializeField] GameObject cubeObject;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer= GetComponent<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�|�C���^�[������ƌĂ΂��i�F��I����Ԃɕς���j
    public void OnPointerEnter(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        Debug.Log(pointerObject.gameObject.name);
        Debug.Log(pointer);
        //��ɒu����ꍇ�͐A�u���Ȃ��ꍇ�͐�
        if(pointer)m_Renderer.material = blockmaterials[1];
        else m_Renderer.material = blockmaterials[2];
    }

    //�|�C���^�[���o�Ă����Ƃ��ɌĂ΂��i�F��߂��j
    public void OnPointerExit(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        m_Renderer.material = blockmaterials[0];
    }

    public void OnPointerClick(BaseEventData data)
    {
        if (pointer)
        {
            GameObject pointerObject = (data as PointerEventData).pointerClick;

            switch ((data as PointerEventData).pointerId)
            {
                case -1:
                    Debug.Log("Left Click");
                    Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
                    m_Renderer.material = blockmaterials[2];
                    GameManager.I.Block_move=true;
                    pointer = false;
                    break;
                case -2:
                    Debug.Log("Right Click");
                    Ray ray = new Ray(transform.position, -transform.up);
                    Debug.DrawRay(transform.position, -transform.up * 1.0f, Color.red);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1.0f))
                    {
                        Debug.Log(hit.collider.gameObject);
                        anim.SetTrigger("down");
                        GameManager.I.Block_move = true;
                        hit.collider.gameObject.SetActive(false);
                    }
                    break;
                case -3:
                    Debug.Log("Middle Click");
                    break;
            }
            
        }
    }

    //�v�Z�̌덷�Ő���Ȉʒu�ɖ߂�Ȃ����Ƃ����邽�߃A�j���[�V�����I�����Ɉꉞ�C��
    IEnumerator adjust()
    {
        yield return new WaitForSeconds(0.02f);
        GameManager.I.Block_move = false;
        gameObject.transform.position=new Vector3(transform.position.x, Mathf.Round(transform.position.y*100.0f)/100.0f,transform.position.z);
        Debug.Log(transform.position);
    }
}
