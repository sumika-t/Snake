using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    // 方向
    Vector2 dir;
    bool vertical = true;
    bool horizontal = true;

	// Use this for initialization
	void Start () {
        // へびを0.3fごとに動かす
        InvokeRepeating("Move", 0.3f, 0.3f);
	}

    // Update is called once per frame
    void Update() {
        // 押したキーに応じて方向を取得
        if (Input.GetKey(KeyCode.RightArrow) && horizontal)
        {
            horizontal = false;
            vertical = true;
            dir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && vertical)
        {
            horizontal = true;
            vertical = false;
            dir = -Vector2.up;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && horizontal)
        {
            horizontal = false;
            vertical = true;
            dir = -Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && vertical)
        {
            horizontal = true;
            vertical = false;
            dir = Vector2.up;
        }
	}

    void Move()
    {
        // 取得した方向に向かい動かす
        transform.Translate(dir);
    }

    //snakeがkinematicの場合は、borderのBoxCollider2DのIsTriggerにチェックを入れればOnTriggerEnter2Dが実行されます。
    //ここのCollision アクションマトリックスに組み合わせがかいてあります。→ https://docs.unity3d.com/ja/540/Manual/CollidersOverview.html
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ほにゃらら");
    }
}
