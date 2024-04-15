using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    int[] map;
    // Start is called before the first frame update

    /// <summary>
    /// 文字列の出力
    /// </summary>
    void PrintArray()
    {
        //追加。文字列の宣言と初期化
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //変更。文字列を結合していく
            debugText += map[i].ToString() + ",";
        }
        //結合した文字列を出力
        Debug.Log(debugText);
    }

    /// <summary>
    /// プレイヤーのインデックスを取得する
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
    /// <param name="number">動かす数字</param>
    /// <param name="moveFrom">動かす元のインデックス</param>
    /// <param name="moveTo">動かす先のインデックス</param>
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
        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerindex = GetPlayerindex();

            MoveNumber(1, playerindex, playerindex + 1);
            PrintArray();
        }

        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerindex = GetPlayerindex();

            MoveNumber(1, playerindex, playerindex - 1);
            PrintArray();
        }

        
    }
}
