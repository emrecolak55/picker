using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Player player;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.finishLineStop();
        }

  
    }
}
