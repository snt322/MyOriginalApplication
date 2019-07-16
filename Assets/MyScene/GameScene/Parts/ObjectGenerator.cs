using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {
    [SerializeField]
    GameObject m_EnemyObj = null;




	// Use this for initialization
	void Start () {
        StartCoroutine("GenerateEnemy");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GenerateEnemy()
    {
        List<GameObject> enemyList = new List<GameObject>();
        
        for(; ;)
        {
            float xOffset = -82f;
            float zOffset = -142f;

            float x = UnityEngine.Random.Range(-10f, 10f) + xOffset;
            float y = UnityEngine.Random.Range(0, 10f);
            float z = UnityEngine.Random.Range(-10f, 10f) + zOffset;
            Vector3 pos = new Vector3(x, y, z);

            enemyList.Add(Instantiate(m_EnemyObj, pos, Quaternion.identity));
            if(enemyList.Count >= 11)
            {
                for(int i= enemyList.Count - 1; i>=0; i--)
                {
                    Destroy(enemyList[i]);
                    enemyList.RemoveAt(i);    
                }
            }


            yield return new WaitForSeconds(1f);
        }
    }
}
