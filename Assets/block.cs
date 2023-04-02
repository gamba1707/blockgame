using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    //���������邱�Ƃ��o���邩�i��Ƀu���b�N������ꍇfalse�j
    private bool pointer;
    //�ʏ펞�̏��ƑI�����̃}�e���A��
    [SerializeField] Material[] blockmaterials = new Material[3];
    //�v���n�u�̃u���b�N
    [SerializeField] GameObject cubeObject;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer= GetComponent<MeshRenderer>();
        pointer= true;
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
        GameObject pointerObject = (data as PointerEventData).pointerClick;
        pointer= false;
        m_Renderer.material = blockmaterials[2];
        Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
        
    }
}
