using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class IngameUIManager : MonoBehaviour
{
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI CombatScore;
    // Start is called before the first frame update
    void Start()
    {
        Lives.text = "Lives Remaining: " + GameMasterScript.GetInstance().LivesLeft;
        CombatScore.text = "Combat Score: " + GameMasterScript.GetInstance().CombatScore;
    }

    // Update is called once per frame
    void Update()
    {
        CombatScore.text = "Combat Score: " + GameMasterScript.GetInstance().CombatScore;
    }
}
