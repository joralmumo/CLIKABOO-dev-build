using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPumpkins : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private TMPro.TextMeshProUGUI pumpkinCount;
    private int pmkCnt;
    void Start()
    {
        pumpkinCount = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        pmkCnt = levelManager.pumpkins;

        if (pmkCnt == 1) { 
            pumpkinCount.text = pmkCnt.ToString()+" pumpkin";
        }
        else
        {
            pumpkinCount.text = pmkCnt.ToString() + " pumpkins";
        }
        
    }
}
