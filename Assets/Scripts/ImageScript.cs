using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject container;    //�������
    private float fillValue; //�������������
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameControll.Health;
        fillValue = fillValue / GameControll.MaxHealth;
        container.GetComponent<Image>().fillAmount=fillValue;
    }
}
