using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    // 餌プレハブ
    public GameObject foodPrefab;

    // 壁
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderRight;
    public Transform borderLeft;

	// Use this for initialization
	void Start () {

        // スポーンメソッドを実行
        Spawn();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // スポーン
    public void Spawn()
    {
        // x軸の左端から右端を求める
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);

        // y軸の下端から上端を求める
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        // ランダムな位置に餌を生成する
        GameObject food = Instantiate(foodPrefab) as GameObject;
        food.transform.position = new Vector3(x, y, 0);
    }
}
