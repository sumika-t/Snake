using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour {

	// スネークオブジェクト
	GameObject[] snakes;

	// オーディオ
	public AudioClip eatSE;
	public AudioClip gameOverSE;

	AudioSource aud;

    // スコアポイント
    public int point = 1;

    // 餌ジェネレータスクリプト
    public SpawnFood spawnFood;

    // 尻尾プレハブ
    public GameObject tailPrefab;

    // 餌を食べたか
    bool eat = false;

    List<Transform> tail = new List<Transform>();

    // 方向
    Vector2 dir;
    bool vertical = true;
    bool horizontal = true;

	// Use this for initialization
	void Start () { 
		
		// ゲットコンポーネント
		this.aud = GetComponent<AudioSource>();

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
        // 位置を保存
        Vector2 v = transform.position;

        // 取得した方向に向かい動かす
        transform.Translate(dir);

        // 餌を食べたら新しい尻尾を空白に入れる
        if (eat)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            //
            tail.Insert(0, g.transform);

            // フラグリセット
            eat = false;
        }

        // 尻尾があるか
        else if(tail.Count > 0)
        {
            // 最後尾を現在頭があるところまで移動する
            tail.Last().position = v;

            // リストの頭に追加、後ろから消す
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Food") {
			// 食べた？
			eat = true;

			// SE
			this.aud.PlayOneShot (this.eatSE);

			// 餌にぶつかると餌が消える
			FindObjectOfType<Score> ().AddPoint (point);
			Destroy (c.gameObject);
			spawnFood.Spawn ();
		} else {
			// SE
			this.aud.PlayOneShot (this.gameOverSE);
			// Snakeタグのついたゲームオブジェクトを隠す
			snakes = GameObject.FindGameObjectsWithTag ("Snake");
			foreach (GameObject snake in snakes) {
				snake.GetComponent<Renderer> ().enabled = false;
			}
			// コルーチン
			StartCoroutine ("gameOver");

			// ハイスコアの保存
			//FindObjectOfType<Score> ().Save ();

			// シーン遷移
			//SceneManager.LoadScene ("GameOverScene");
		}
	}

	IEnumerator gameOver(){
		// ハイスコアの保存
		FindObjectOfType<Score> ().Save ();
		// ウエイト
		yield return new WaitForSeconds(0.5f);
		// シーンの遷移
		SceneManager.LoadScene ("GameOverScene");
	}
    //snakeがkinematicの場合は、borderのBoxCollider2DのIsTriggerにチェックを入れればOnTriggerEnter2Dが実行されます。
    //ここのCollision アクションマトリックスに組み合わせがかいてあります。→ https://docs.unity3d.com/ja/540/Manual/CollidersOverview.html
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //  Debug.Log("OnTriggerEnter2D");
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //  Debug.Log("ほにゃらら");
    //}
}