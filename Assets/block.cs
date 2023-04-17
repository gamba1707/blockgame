using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    private Animator anim;
    //�����Ƀ|�C���^�[���������Ă��邩
    [SerializeField] private bool onpointer=false;
    //���������邱�Ƃ��o���邩�i��Ƀu���b�N������ꍇfalse�j
    [SerializeField] private bool addblock = true;
    //�ʏ펞�̏��ƑI�����̃}�e���A��(�D�F�A�A��)
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
        onpointer= true;
        Debug.Log(pointerObject.gameObject.name);
        Debug.Log(addblock);
        //��ɒu����ꍇ�͐A�u���Ȃ��ꍇ�͐�
        if(addblock) m_Renderer.material = blockmaterials[1];
        else m_Renderer.material = blockmaterials[2];
    }

    //�|�C���^�[���o�Ă����Ƃ��ɌĂ΂��i�F��߂��j
    public void OnPointerExit(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        onpointer= false;
        m_Renderer.material = blockmaterials[0];
    }

    //���̂ɓ������Ă���Ƃ��Ƀ}�E�X�̃{�^����������
    public void OnPointerClick(BaseEventData data)
    {
        //�����o�������邱�Ƃ��o���锠�ł��違���u���b�N�������Ă���r���ł͂Ȃ�
        if (addblock && !GameManager.I.Block_move)
        {
            GameObject pointerObject = (data as PointerEventData).pointerClick;

            switch ((data as PointerEventData).pointerId)
            {
                case -1:
                    //���N���b�N����
                    Debug.Log("Left Click");
                    Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
                    m_Renderer.material = blockmaterials[2];
                    GameManager.I.Block_move=true;//�}�l�[�W���[�Ƀu���b�N�������Ă���Œ����ƒm�点��
                    addblock = false;
                    break;
                case -2:
                    //�E�N���b�N����
                    Debug.Log("Right Click");
                    Ray ray = new Ray(transform.position, -transform.up);
                    Debug.DrawRay(transform.position, -transform.up * 1.0f, Color.red);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1.0f))
                    {
                        Debug.Log("��������obj:"+hit.collider.gameObject+"�^�O�F"+hit.collider.tag);
                        anim.SetTrigger("down");//�����郂�[�V�����Đ�
                        GameManager.I.Block_move = true;//�}�l�[�W���[�Ƀu���b�N�������Ă���Œ����ƒm�点��
                        //�G�ꂽ���̂�cube�̃^�O�������Ă�����̂Ȃ�I�u�W�F�N�g���\���ɂ���
                        if (hit.collider.transform.parent.gameObject.CompareTag("cube"))
                        {
                            hit.collider.transform.parent.gameObject.SetActive(false);
                        }else if (hit.collider.gameObject.CompareTag("cube"))
                        {
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                    break;
                case -3:
                    Debug.Log("Middle Click");
                    break;
            }
            
        }
    }

    //�v�Z�̌덷�Ő���Ȉʒu�ɖ߂�Ȃ����Ƃ����邽�߃A�j���[�V�����I�����ɌĂяo���ꉞ�C��
    IEnumerator adjust()
    {
        yield return new WaitForSeconds(0.02f);
        GameManager.I.Block_move = false;//�u���b�N�������Ă��Ȃ��i�������Ă������j
        float y = Mathf.Round(transform.position.y / 1.5f);//1.5�̔{���̈ʒu�ɕK�����������̂�1.5���H���v�Z
        //�����̂���Ȃ̂ő��������ł����������
        gameObject.transform.position=new Vector3(transform.position.x, y*1.5f,transform.position.z);
        
        Debug.Log("�C���ʒu�F"+transform.position);
    }

    public void OnPlayerEnter()
    {
        Debug.Log("false: "+this.gameObject.name);
        addblock = false;
        if(onpointer) m_Renderer.material = blockmaterials[2];
    }
    public void OnPlayerExit()
    {
        Debug.Log("true: " + this.gameObject.name);
        addblock = true;
        if (onpointer) m_Renderer.material = blockmaterials[1]; 
    }
}
