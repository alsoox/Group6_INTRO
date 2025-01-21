using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class User : MonoBehaviour
{
    public int index; //User ��
    public GameObject Live; // ������ �ѿ� �±� �� 
    public GameObject Death;// ������ �ѿ� ���� ��


    void Awake()
    {
        
    }

    void Start()
    {
        if(gameObject.name.StartsWith("User")) // User �ε����� �ο�
        {
            string name = gameObject.name;
            if (name.Length > 4 && int.TryParse(name.Substring(4), out int result))
            {
                index = result;
            }
        }       
    }

    public void UserDie()
    {
        
    }
}
