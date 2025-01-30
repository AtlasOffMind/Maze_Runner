using UnityEngine;


public class CharacterKeepping : MonoBehaviour
{
    public static CharacterKeepping instance;


    public string blueTeam1;
    public string blueTeam2;
    public string redTeam1;
    public string redTeam2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
             DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
