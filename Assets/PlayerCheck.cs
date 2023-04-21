using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private block b;
    private string objname="obj";
    // Start is called before the first frame update
    void Start()
    {
        b= transform.parent.gameObject.GetComponent<block>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        b.OnPlayerEnter();
    }

    //���܃o�O���Ă����̂Ŋm�F�����߂�
    private void OnTriggerStay(Collider other)
    {
        if(!objname.Equals(other.gameObject.tag)&& other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[������Ă��܂�");
            objname = other.gameObject.tag;
            b.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            b.OnPlayerExit();
        objname = "null";
    }
}
