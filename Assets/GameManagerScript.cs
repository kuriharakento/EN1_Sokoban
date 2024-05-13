using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    // Start is called before the first frame update


    /// <summary>
    /// ������̏o��
    /// </summary>
    //void PrintArray()
    //{
    //    //�ǉ��B������̐錾�Ə�����
    //    string debugText = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        //�ύX�B��������������Ă���
    //        debugText += map[i].ToString() + ",";
    //    }
    //    //����������������o��
    //    Debug.Log(debugText);
    //}

    /// <summary>
    /// �v���C���[�̃C���f�b�N�X���擾����
    /// </summary>
    /// <returns></returns>
    Vector2Int GetPlayerindex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag">�^�O</param>
    /// <param name="moveFrom">���������̃C���f�b�N�X</param>
    /// <param name="moveTo">��������̃C���f�b�N�X</param>
    /// <returns></returns>
    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {

        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(0)) { return false; }

        //
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if(!success) { return false; }
        }

        //�v���C���[�E��������炸�̈ړ�����
        field[moveFrom.y,moveFrom.x].transform.position = new Vector3(moveTo.x,moveTo.y,0);
        field[moveTo.y,moveTo.x] = field[moveFrom.y,moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }
    public GameObject playerPrefab;
    int[,] map;
    GameObject[,] field;

    void Start()
    {

        map = new int[,]
        {
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,1 },
        };

        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(
                            x - map.GetLength(1) / 2 + 0.5f,
                            -y + map.GetLength(0) / 2,
                            0
                            ),
                        Quaternion.identity
                    );
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerindex = GetPlayerindex();

            MoveNumber("Player", 
                playerindex, 
                playerindex + new Vector2Int(1,0)
            );
            //PrintArray();
        }

        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
             Vector2Int playerindex = GetPlayerindex();

            MoveNumber("Player", 
                playerindex, 
                playerindex + new Vector2Int(-1,0)
                );
            //PrintArray();
        }


    }
}
