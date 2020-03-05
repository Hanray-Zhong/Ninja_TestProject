using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity;

public class BossSkills : MonoBehaviour
{
    [System.Serializable]
    public struct GenerateEnemiesRegionGroup {
        public BoxCollider2D[] generateEnemiesRegions;
    }


    public Transform PlayerTrans;

    // private BossFSM bossFSM;

    private Rigidbody2D _rigidbody2D;
    [Header("Generate Enemies")]
    public GameObject EnemyPrefab;
    public GameObject EnemiesFather;
    public GenerateEnemiesRegionGroup[] generateEnemiesRegionGroups;
    public Vector2 Margin;
    [Header("Generate Float Block")]
    public GameObject FloatBlockPrefab;
    public Vector2 floatBlockOriginPos;
    [Header("Throw Throns")]
    public GameObject[] Thorns;
    public float throwForce;
    [Header("Dash")]
    // public bool isReadyDash = false;
    public bool canDash = false;
    public Vector2 dir2Player;
    public float aim_offset;
    public float speedParamater;
    public AnimationCurve speed;
    public LineRenderer warningLR;

    private void Start() {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        // bossFSM = gameObject.GetComponent<BossFSM>();
    }

    /* FSM Skills
    public void GenerateEnemies(int endTime) {
        GenerateRandom(EnemyPrefab, generateEnemiesRegionGroups);
        Invoke("SkillEnd", endTime);
    }
    private void GenerateRandom(GameObject obj, GenerateEnemiesRegionGroup[] generateEnemiesRegionGroups) {
        foreach (GenerateEnemiesRegionGroup regionGroup in generateEnemiesRegionGroups) {
            Vector2 pos;
            int index = Random.Range(0, regionGroup.generateEnemiesRegions.Length);
            Bounds bounds = regionGroup.generateEnemiesRegions[index].bounds;
            pos = new Vector2(Random.Range(bounds.min.x + Margin.x, bounds.max.x - Margin.x), Random.Range(bounds.min.y + Margin.y, bounds.max.y - Margin.y));
            Instantiate(obj, pos, Quaternion.identity);
        }
    }

    public void GenerateFloatBlock(int endTime) {
        Instantiate(FloatBlockPrefab, floatBlockOriginPos, Quaternion.identity);
        Invoke("SkillEnd", endTime);
    }

    public void GenerateThorns() {
        for (int i = 0; i < Thorns.Length; i++) {
            Vector3 offset = new Vector3(0.5f * Mathf.Sin(i * (Mathf.PI / 18)), 0.5f * Mathf.Cos(i * (Mathf.PI / 18)), 0);
            Thorns[i].SetActive(true);
            Thorns[i].transform.position = transform.position + transform.localScale.x * offset;
            Thorns[i].transform.up = Thorns[i].transform.position - transform.position;
        }
    }

    public void ThrowThorns(int endTime) {
        foreach (GameObject thorn in Thorns) {
            thorn.GetComponent<Rigidbody2D>().AddForce(thorn.transform.up.normalized * throwForce, ForceMode2D.Impulse);
        }
        Invoke("SkillEnd", endTime);
    }

    public void StartDash(int endTime) {
        isReadyDash = true;
        Invoke("SkillEnd", endTime);
    }
    private void ReadyDash() {
        warningLR.gameObject.SetActive(true);
        warningLR.SetPosition(0, transform.position);
        warningLR.SetPosition(1, PlayerTrans.position);
        dir2Player = PlayerTrans.position - transform.position + new Vector3(0, aim_offset, 0);

    }
    private void BossDash() {
        warningLR.gameObject.SetActive(false);
        rigidbody.velocity = speed.Evaluate(timmer / 300) * dir2Player * speedParamater;
        timmer++;
    }
    */

    // TimeLine Skills
    public void GenerateEnemies_TimeLine() {
        GenerateRandom(EnemyPrefab, generateEnemiesRegionGroups);
    }
    private void GenerateRandom(GameObject obj, GenerateEnemiesRegionGroup[] generateEnemiesRegionGroups) {
        foreach (GenerateEnemiesRegionGroup regionGroup in generateEnemiesRegionGroups) {
            Vector2 pos;
            int index = Random.Range(0, regionGroup.generateEnemiesRegions.Length);
            Bounds bounds = regionGroup.generateEnemiesRegions[index].bounds;
            pos = new Vector2(Random.Range(bounds.min.x + Margin.x, bounds.max.x - Margin.x), Random.Range(bounds.min.y + Margin.y, bounds.max.y - Margin.y));
            Instantiate(obj, pos, Quaternion.identity, EnemiesFather.transform);
        }
    }

    public void GenerateFloatBlock_TimeLine() {
        Instantiate(FloatBlockPrefab, floatBlockOriginPos, Quaternion.identity);
    }

    public void ThrowThorns_TimeLine() {
        foreach (GameObject thorn in Thorns) {
            thorn.GetComponent<Rigidbody2D>().AddForce(thorn.transform.up.normalized * throwForce, ForceMode2D.Impulse);
        }
    }
    public void GenerateThorns() {
        for (int i = 0; i < Thorns.Length; i++) {
            Vector3 offset = new Vector3(0.5f * Mathf.Sin(i * (Mathf.PI / 18)), 0.5f * Mathf.Cos(i * (Mathf.PI / 18)), 0);
            Thorns[i].SetActive(true);
            Thorns[i].transform.position = transform.position + transform.localScale.x * offset;
            Thorns[i].transform.up = Thorns[i].transform.position - transform.position;
        }
    }


    public void ReadyDash() {
        warningLR.gameObject.SetActive(true);
        warningLR.SetPosition(0, transform.position);
        warningLR.SetPosition(1, PlayerTrans.position);
        dir2Player = PlayerTrans.position - transform.position + new Vector3(0, aim_offset, 0);
    }
    public void BossDash(float timmer) {
        if (canDash) {
            _rigidbody2D.velocity = speed.Evaluate(timmer / 300) * dir2Player * speedParamater;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ground") {
            _rigidbody2D.velocity = Vector2.zero;
            canDash = false;
        }
    }

/*    private void SkillEnd() {
        bossFSM.currentStateEnd = true;
    }*/



    /* FSM
    [CustomEditor(typeof(BossSkills))]
    public class BossSkillsEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            BossSkills skills = (BossSkills)target;

            if (GUILayout.Button("Generate Enemies")) {
                skills.GenerateEnemies(10);
            }
            if (GUILayout.Button("Generate Float Block")) {
                skills.GenerateFloatBlock(10);
            }
            if (GUILayout.Button("Generate Thorns")) {
                skills.GenerateThorns();
            }
            if (GUILayout.Button("Throw Thorns")) {
                skills.ThrowThorns(10);
            }
            if (GUILayout.Button("Start Dash")) {
                skills.isReadyDash = true;
            }
        }
    }
    */
    private void OnDrawGizmos() {
        if (generateEnemiesRegionGroups.Length == 0) return;
        Gizmos.color = new Color(1, 0, 0, 1);
        foreach (var enerateEnemiesRegionGroup in generateEnemiesRegionGroups) {
            foreach (BoxCollider2D col in enerateEnemiesRegionGroup.generateEnemiesRegions) {
                Vector3 a = new Vector3(col.bounds.max.x - Margin.x, col.bounds.max.y - Margin.y, 0);
                Vector3 b = new Vector3(col.bounds.max.x - Margin.x, col.bounds.min.y + Margin.y, 0);
                Vector3 c = new Vector3(col.bounds.min.x + Margin.x, col.bounds.min.y + Margin.y, 0);
                Vector3 d = new Vector3(col.bounds.min.x + Margin.x, col.bounds.max.y - Margin.y, 0);
                Gizmos.DrawLine(a, b);
                Gizmos.DrawLine(a, d);
                Gizmos.DrawLine(c, b);
                Gizmos.DrawLine(c, d);
            }
        }
        Gizmos.DrawSphere(floatBlockOriginPos, 0.1f);

    }
}
