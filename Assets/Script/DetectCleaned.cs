using UnityEngine;

public class DetectCleaned : MonoBehaviour
{
    [Header("Objetos a chequear")]
    public CleanAction[] objetosAChequear; 

    [Header("Manager")]
    public MessageManager messageManager; 

    private bool[] previousStates; 

    void Start()
    {
        if (objetosAChequear == null || objetosAChequear.Length == 0)
        {
            Debug.LogError("No hay objetos a chequear en DetectCleaned.");
            return;
        }

        
        previousStates = new bool[objetosAChequear.Length];
        for (int i = 0; i < objetosAChequear.Length; i++)
        {
            if (objetosAChequear[i] != null)
                previousStates[i] = objetosAChequear[i].isClean;
        }
    }

    void Update()
    {
        for (int i = 0; i < objetosAChequear.Length; i++)
        {
            var obj = objetosAChequear[i];
            if (obj == null) continue;

            
            if (!previousStates[i] && obj.isClean)
            {
                Debug.Log($"Objeto {obj.name} ha sido limpiado.");

                if (messageManager != null)
                {
                    messageManager.ForzarNotificacion();
                }
            }

            
            previousStates[i] = obj.isClean;
        }
    }
}
