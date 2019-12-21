using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossSkills : MonoBehaviour
{
    [System.Serializable]
    public struct GenerateEnemiesRegionGroup {
        public BoxCollider2D[] generateEnemiesRegions;
    }
    [Header("Generate Enemies")]
    public GameObject EnemyPrefab;
    public GenerateEnemiesRegionGroup[] generateEnemiesRegionGroups;
    public Vector2 Margin;
    [Header("Generate Float Block")]
    public GameObject FloatBlockPrefab;
    public Vector2 floatBlockOriginPos;

    public void GenerateEnemies() {
        GenerateRandom(EnemyPrefab, generateEnemiesRegionGroups);
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

    public void GenerateFloatBlock() {
        Instantiate(FloatBlockPrefab, floatBlockOriginPos, Quaternion.identity);
    }






    [CustomEditor(typeof(BossSkills))]
    public class BossSkillsEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            BossSkills skills = (BossSkills)target;

            if (GUILayout.Button("Generate Enemies")) {
                skills.GenerateEnemies();
            }
            if (GUILayout.Button("Generate Float Block")) {
                skills.GenerateFloatBlock();
            }
        }
    }
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
