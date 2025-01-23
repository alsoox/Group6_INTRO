using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RussianRoulette : MonoBehaviour
{
    public RevolverCount m_revolverCount;
    public List<Animator> m_personList;
    public Light m_light;
    public Camera m_camera;
    public GameObject m_revolver;
    public CanvasGroup m_blood;
    public Transform m_originCameraTransform;
    public Transform m_gameInRouletteCameraPos;
    public Transform m_originRevolverTransform;
    public Transform m_revolverCameraTransform;
    public Transform m_tempTransform;
    // ������ ���̶�� �˸�
    public bool m_isAction = false;

    // �� �� �����̴� ��
    public bool m_isHesitate = true;

    public void Start(){
        m_revolverCount.gameObject.SetActive(false);
        GameManager.Instance.RussianRouletteAction = this;
    }
    public void OnClick1PlayerKill(bool _isShoot){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, 0));
    }
    public void OnClick2PlayerKill(bool _isShoot){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, 1));
    }
    public void OnClick3PlayerKill(bool _isShoot){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, 2));
    }
    public void OnClick4PlayerKill(bool _isShoot){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, 3));
    }
    public void OnClick5PlayerKill(bool _isShoot){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, 4));
    }

    public void ShootAction(bool _isShoot, int _sitPosition){
        StartCoroutine(CoroutineRussianRoulette(_isShoot, _sitPosition));
    }


    private IEnumerator GunMoveToPerson(int _sitPosition)
    {
        // Position ����
        Vector3 newPosition = m_personList[_sitPosition].transform.position;
        newPosition.z -= 0.3f; // z ���� -0.3 ����
        m_tempTransform.position = newPosition;

        // Rotation ����
        m_tempTransform.rotation = m_personList[_sitPosition].transform.rotation; // ȸ���� �״�� ����
        

        yield return StartCoroutine(CoroutineObjectMove(m_revolver, m_originRevolverTransform, m_tempTransform));
    }

    private IEnumerator GunReturnToTable()
    {
        yield return StartCoroutine(CoroutineObjectMove(m_revolver, m_tempTransform, m_originRevolverTransform));
    }

    private IEnumerator GunAction(bool _isShoot, int _sitPosition)
    {
        // ������ ���� ���� ���� ����
        Animator person = m_personList[_sitPosition];

        person.SetBool("Scared", true);
        if (m_isHesitate){
            m_revolver.GetComponent<Animator>().SetTrigger("PrepareForShooting");
            yield return new WaitForSeconds(2f);
            m_revolver.GetComponent<Animator>().SetTrigger("Shot2");
            yield return new WaitForSeconds(1f);
        } else {
            m_revolver.GetComponent<Animator>().SetTrigger("Shot1");
            yield return new WaitForSeconds(1f);
        }

        // �Ѿ��� �������� �ȳ�������
        if(_isShoot){
            SoundManager.instance.PlaySFX("fire");
            m_revolverCount.SetShoot(GameManager.Instance.currentChance-1);
            
            m_light.color = Color.red * 2f;

            m_blood.alpha = 1;
            m_blood.gameObject.SetActive(true);
            person.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            float fadeDuration = 1f; // ������� �ð�
            float timeElapsed = 0f;
            while (timeElapsed < fadeDuration)
            {
                m_blood.alpha = 1f - (timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            m_blood.gameObject.SetActive(false);
            m_blood.alpha = 1;
        } else {
            SoundManager.instance.PlaySFX("fall");
            m_revolverCount.SetBullet(GameManager.Instance.currentChance-1);
            
        }

        yield return new WaitForSeconds(1f);
        person.SetBool("Scared", false);
    }
    
    private IEnumerator CoroutineRussianRoulette(bool _isShoot, int _sitPosition)
    {
        // �ൿ ������ ���� �ڷ�ƾ ���� �ȵ�
        if(m_isAction){
            yield break;
        }
        // �ൿ ������ ���� �ش� �ڷ�ƾ �ٽ� ���� �ȵǰ� �ϱ� ���� bool�� ��������� ����
        m_isAction = true;

        m_revolverCount.gameObject.SetActive(true);
        
        m_revolverCount.SetBullet(GameManager.Instance.currentChance);

        m_light.gameObject.SetActive(true);
        m_light.color = Color.red * 0.35f;
        // �� �̵�
        yield return StartCoroutine(GunMoveToPerson(_sitPosition));
        // �� ���� ī�޶� �̵�
        var fovMove = StartCoroutine(CoroutineFovMove(Camera.main, 15f, 60f));
        var camPosMove = StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_originCameraTransform, m_gameInRouletteCameraPos));
        yield return camPosMove;
        yield return fovMove;

        Animator person = m_personList[_sitPosition];
        person.GetComponent<CharacterText>().Alert();
        yield return new WaitForSeconds(1f); // ���ϰ� 1�� ��ٸ�

        yield return StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_gameInRouletteCameraPos, m_revolverCameraTransform));
        yield return StartCoroutine(GunAction(_isShoot, _sitPosition));


        person.GetComponent<CharacterText>().Alert();

        yield return StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_revolverCameraTransform, m_gameInRouletteCameraPos));
        yield return new WaitForSeconds(0.5f); // ���ϰ� 1�� ��ٸ�

        // ī�޶� ���̺�� ����ġ
        fovMove = StartCoroutine(CoroutineFovMove(Camera.main, 60f, 15f));
        camPosMove = StartCoroutine(CoroutineObjectMove(m_camera.gameObject, m_gameInRouletteCameraPos, m_originCameraTransform));
        var gun = StartCoroutine(GunReturnToTable());
        var light = StartCoroutine(CoroutineLightColorFade(m_light, m_light.color.r, 0f));
        yield return fovMove;
        yield return camPosMove;

        // �� ���̺�� ����ġ
        yield return gun;
        

        // ����Ʈ ������ ����
        yield return light;
        m_light.gameObject.SetActive(false);
        
        m_revolverCount.gameObject.SetActive(false);
        
        // �ൿ ������ ���� �ش� �ڷ�ƾ �ٽ� ���� �ȵǰ� �ϱ� ���� bool�� ��������� ����
        m_isAction = false;
    }

    //ī�� �ִϸ��̼�
    private IEnumerator CoroutineObjectMove(GameObject _object, Transform _startPos, Transform _endPos)
    {
        float moveDuration = 1f; // �̵� �ð�
        float timeElapsed = 0f;
        while (timeElapsed < moveDuration+0.1f)
        {
            _object.transform.position = Vector3.Lerp(_startPos.position, _endPos.position, timeElapsed / moveDuration);   // ��������
            _object.transform.rotation = Quaternion.Lerp(_startPos.rotation, _endPos.rotation, timeElapsed / moveDuration);   // ��������
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _object.transform.position = _endPos.position;
        _object.transform.rotation = _endPos.rotation;
    }

    //ī�� �ִϸ��̼�
    private IEnumerator CoroutineFovMove(Camera _camera, float _start, float _end)
    {
        float moveDuration = 1f; // �̵� �ð�
        float timeElapsed = 0f;
        while (timeElapsed < moveDuration+0.1f)
        {
            _camera.fieldOfView = Mathf.Lerp(_start, _end, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _camera.fieldOfView = _end;
    }


    
    //����Ʈ �ִϸ��̼�
    private IEnumerator CoroutineLightColorFade(Light _light, float _start, float _end)
    {
        float duration = 1f; // ��ȯ �ð�
        float timeElapsed = 0f;

        float between = _start - _end;
        
        while (timeElapsed < duration)
        {
            _light.color = Color.red * (between * ( 1 - (timeElapsed / duration)));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _light.color = Color.red * _end;
    }
}
