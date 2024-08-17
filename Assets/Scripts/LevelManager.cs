using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int pumpkins = 0;
    [SerializeField]
    private float clickerMultiplier = 1f;
    [SerializeField]
    private GameDataController gameDataController;
    [SerializeField]
    private string urlname;

    private bool isAutoClicking = false;
    private float autoClickInterval = 1f; // Variable para controlar el tiempo de espera

    private readonly List<(int threshold, float multiplier)> multipliers = new List<(int, float)>
    {
        (100, 1f),
        (1000, 20f),
        (10000, 50f),
        (100000, 100f),
        (1000000, 500f),
        (10000000, 1000f),
        (100000000, 5000f)
    };

    private readonly List<System.Action> randomEvents = new List<System.Action>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Agregar métodos de eventos a la lista
        randomEvents.Add(Event1);
        randomEvents.Add(Event2);
        randomEvents.Add(Event3);
        randomEvents.Add(Event4);
    }

    // Update is called once per frame
    void Update()
    {
        if (pumpkins > 99 && !isAutoClicking)
        {
            StartCoroutine(BasicAutoClick());
            isAutoClicking = true;
        }

        UpdateClickerMultiplier();
    }

    private void UpdateClickerMultiplier()
    {
        foreach (var (threshold, multiplier) in multipliers)
        {
            if (pumpkins >= threshold)
            {
                clickerMultiplier = multiplier;
                autoClickInterval = 1f / clickerMultiplier; // Ajusta el intervalo de espera basado en el multiplicador
            }
        }
    }

    public void AddPumpkin()
    {
        pumpkins += Mathf.RoundToInt(1 * clickerMultiplier);
        Debug.Log("Añadida calabaza");

        
        if (UnityEngine.Random.Range(0f, 1f) <= 0.01f) 
        {
            ExecuteRandomEvent();
        }
    }

    public void AddPumpkinByClick()
    {
        pumpkins++;
        Debug.Log("Añadida calabaza por clic");

        if (UnityEngine.Random.Range(0f, 1f) <= 0.01f) 
        {
            ExecuteRandomEvent();
        }
    }

    private void ExecuteRandomEvent()
    {
        if (randomEvents.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, randomEvents.Count);
            randomEvents[index]?.Invoke();
        }
    }

    private void Event1()
    {
        Debug.Log("Evento 1 ocurrió!");
        pumpkins *= -1;
        gameDataController.SaveData();
    }

    private void Event2()
    {
        Debug.Log("Evento 2 ocurrió!");
        pumpkins = 0;
        gameDataController.SaveData();
    }

    private void Event3()
    {
        Debug.Log("Evento 4 ocurrió!");
        Application.Quit();
    }

    private void Event4()
    {
        Application.OpenURL(urlname);
    }

    IEnumerator BasicAutoClick()
    {
        while (true)
        {
            AddPumpkin();
            gameDataController.SaveData();
            yield return new WaitForSeconds(autoClickInterval); // Usa el intervalo ajustado
        }
    }
}
