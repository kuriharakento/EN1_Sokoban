using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    // Start is called before the first frame update

    Vector3 IndexToPosition(Vector2Int index)
    {
        return new Vector3(
            -(map.GetLength(1) / 2.0f - index.x),
            -(map.GetLength(0) / 2.0f - index.y),
            0
        );
    }


    /// <summary>
    /// プレイヤーのインデックスを取得する
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
    /// <param name="moveFrom">動かす元のインデックス</param>
    /// <param name="moveTo">動かす先のインデックス</param>
    /// <returns></returns>
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {

        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }


        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        
        
        //パーティクルの生成
        for (int i = 0; i < 8; ++i)
        {
            Instantiate(
                particlePrefab,
                IndexToPosition(GetPlayerindex()),
                Quaternion.identity
            );        
        }


        //プレイヤー・箱かかわらずの移動処理
        Vector3 moveToPosition = IndexToPosition(moveTo);
        field[moveFrom.y,moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }

    bool IsCleard()
    {
        //Vector2Int型の可変長配列の作成
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }

        return true;
    }


    public GameObject playerPrefab;
    public GameObject boxPrefab;
    int[,] map;
    GameObject[,] field;

    public GameObject clearText;
    public GameObject particlePrefab;
    public GameObject goalPrefab;

    void Start()
    {
        Screen.SetResolution(1280,720,false);

        map = new int[,]
        {
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,1,0,2,3,0,0,0 },
            { 3,2,0,0,2,3,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
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
                        IndexToPosition(new Vector2Int(x,y)),
                        Quaternion.identity
                    );
                }

                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        IndexToPosition(new Vector2Int(x,y)),
                        Quaternion.identity
                    );
                }

                if (map[y, x] == 3)
                {
                    Instantiate(
                        goalPrefab,
                        new Vector3(-(map.GetLength(1) / 2.0f - x), -(map.GetLength(0) / 2.0f - y), 0.01f) ,
                        Quaternion.identity
                    );
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerindex = GetPlayerindex();

            MoveNumber(
                playerindex,
                playerindex + new Vector2Int(1, 0)
            );
        }

        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerindex = GetPlayerindex();

            MoveNumber(
                playerindex,
                playerindex + new Vector2Int(-1, 0)
                );
        }

        //上移動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerindex = GetPlayerindex();

            MoveNumber(
                playerindex,
                playerindex + new Vector2Int(0, 1)
            );
        }

        //下移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerindex = GetPlayerindex();

            MoveNumber(
                playerindex,
                playerindex + new Vector2Int(0, -1)
            );
        }

        //クリアしたら
        clearText.SetActive(IsCleard());
    }
   
}
