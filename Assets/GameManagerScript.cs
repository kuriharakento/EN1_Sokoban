using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    int[] map;
    // Start is called before the first frame update

    /// <summary>
    /// ������̏o��
    /// </summary>
    void PrintArray()
    {
        //�ǉ��B������̐錾�Ə�����
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //�ύX�B��������������Ă���
            debugText += map[i].ToString() + ",";
        }
        //����������������o��
        Debug.Log(debugText);
    }

    /// <summary>
    /// �v���C���[�̃C���f�b�N�X���擾����
    /// </summary>
    /// <returns></returns>
    int GetPlayerindex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number">����������</param>
    /// <param name="moveFrom">���������̃C���f�b�N�X</param>
    /// <param name="moveTo">��������̃C���f�b�N�X</param>
    /// <returns></returns>
    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerindex = GetPlayerindex();

            MoveNumber(1, playerindex, playerindex + 1);
            PrintArray();
        }

        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerindex = GetPlayerindex();

            MoveNumber(1, playerindex, playerindex - 1);
            PrintArray();
        }

        
    }
}
