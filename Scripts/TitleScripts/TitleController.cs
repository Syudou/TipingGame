using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // �u�啶���E�������ҁv�{�^��
    public void LoadUpperLowerCaseScene()
    {
        SceneManager.LoadScene("Main"); // Main �V�[�������[�h
    }

    // �u�P��ҁv�{�^��
    public void LoadWordModeScene()
    {
        SceneManager.LoadScene("Main2"); // Main2 �V�[�������[�h
    }
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
