using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianRoulette : MonoBehaviour
{
    public List<Animator> m_personList;
    public Light m_actionLight;
    public Camera m_camera;
    public GameObject m_revolver;
    public Transform m_originCameraTransform;
    public Transform m_originRevolverTransform;
    public Transform m_revolverCameraTransform;
    public Transform m_tempTransform;
    // 뒤집는 중이라고 알림
    public bool m_isAction = false;

    // 촐 쏠때 망설이는 것
    public bool m_isHesitate = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineRussianRoulette(true, 1));
    }

    public IEnumerator GunMoveToPerson(int _sitPosition)
    {
        Animator person = m_personList[_sitPosition];

        person.SetBool("Scared", true);

        // Position 복사
        Vector3 newPosition = m_personList[_sitPosition].transform.position;
        newPosition.z -= 0.3f; // z 값을 -0.3 조정
        m_tempTransform.position = newPosition;

        // Rotation 복사
        m_tempTransform.rotation = m_personList[_sitPosition].transform.rotation; // 회전값 그대로 복사
        

        yield return StartCoroutine(CoroutineObjectMove(m_revolver, m_originRevolverTransform, m_tempTransform));
    }

    public IEnumerator GunReturnToTable()
    {
        yield return StartCoroutine(CoroutineObjectMove(m_revolver, m_tempTransform, m_originRevolverTransform));
    }

    public void Gun(bool _isShoot)
    {
        
    }
    
    private IEnumerator CoroutineRussianRoulette(bool _isShoot, int _sitPosition)
    {
        // 총 이동
        yield return StartCoroutine(GunMoveToPerson(_sitPosition));
        // 총 따라 카메라 이동
        yield return StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_originCameraTransform, m_revolverCameraTransform));
        



        // 카메라 테이블로 원위치
        yield return StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_revolverCameraTransform, m_originCameraTransform));
        // 총 테이블로 원위치
        yield return StartCoroutine(GunReturnToTable());
    }

    //카드 애니메이션
    private IEnumerator CoroutineObjectMove(GameObject _object, Transform _startPos, Transform _endPos)
    {
        float moveDuration = 1f; // 이동 시간
        float timeElapsed = 0f;
        while (timeElapsed < moveDuration+0.5f)
        {
            Debug.Log(timeElapsed / moveDuration);
            _object.transform.position = Vector3.Slerp(_startPos.position, _endPos.position, timeElapsed / moveDuration);   // 선형보간
            _object.transform.rotation = Quaternion.Slerp(_startPos.rotation, _endPos.rotation, timeElapsed / moveDuration);   // 선형보간
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _object.transform.position = _endPos.position;
        _object.transform.rotation = _endPos.rotation;
        Debug.Log("aa");
    }
}
