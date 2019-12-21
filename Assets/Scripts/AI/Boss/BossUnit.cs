using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : EnemyUnit
{
    public List<GameObject> status = new List<GameObject>();

    public void ChangeStatus(int index) {
        foreach (GameObject _status in status) {
            if (status.IndexOf(_status) != index) {
                _status.SetActive(false);
            }
            else {
                _status.SetActive(true);
            }
        }
    }
}
