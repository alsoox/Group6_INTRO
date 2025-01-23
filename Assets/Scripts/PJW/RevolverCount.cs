using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RevolverCount : MonoBehaviour
{
    public List<Text> m_textList;
    public Text m_bulletText;

    public void SetBullet(int _bulletCount){
        int totalchance = _bulletCount;
        m_bulletText.text = $"1 / {totalchance}";
        
        for(int i = 0; i < m_textList.Count; i++){
            if(i < 6 - totalchance){
                m_textList[i].text = "X";
            } else {
                m_textList[i].text = "?";
            }
        }
    }
    
    public void SetShoot(int _bulletCount){
        m_textList[5-_bulletCount].text = "!";
        m_bulletText.text = $"0 / {_bulletCount}";
    }
}
