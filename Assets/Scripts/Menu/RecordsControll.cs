using TMPro;
using UnityEngine;

public class RecordsControll : MonoBehaviour
{
    static int record;
    public  TMP_Text recordText;
    private void Awake()
    {
        record= 0;
    }
    private void Start()
    {
        record= PlayerPrefs.GetInt("RecordPlayerLevel");
        recordText.text =$"{record}";
    }
    private void Update()
    {
        recordText.text = $"{record}";
    }
    public static void RecordWrite()
    {
        if (LevelUp.Level == 1) 
        {
            if (LevelUp.Level > record)
            {
                record = LevelUp.Level;
                Debug.Log("New record!"+ record);
                //recordText.text = $"{record}";
            }
        }
        else
        {
            if ((LevelUp.Level - 1) > record)
            {
                record = LevelUp.Level - 1;
                Debug.Log("New record!" + record);
               // recordText.text = $"{record}";
            }
        }
        SaveRecord();

    }
    public static void SaveRecord()
    {
        PlayerPrefs.SetInt("RecordPlayerLevel", record);
        PlayerPrefs.Save();
    }

}
