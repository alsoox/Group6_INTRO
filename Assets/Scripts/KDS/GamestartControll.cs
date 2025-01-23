using System.Collections;
using UnityEngine;

public class GamestartControll : MonoBehaviour
{
    public RectTransform[] menuButtons; // �޴� ��ư��
    public RectTransform[] introText;     // INTRO �ؽ�Ʈ��
    public TextIntroEffect introTextEffect;
    public Camera mainCamera;
    public Vector3[] introTextFinalPos;  // INTRO �ؽ�Ʈ�� ���� ��ġ
    public Vector3[] menuButtonFinalPos; // �޴� ��ư ���� ��ġ
    float CameraFinalfieldofView = 15f;

    //private Vector3 cameraInitialPos;
    private Vector3[] menuButtonsInitialPos;  // �� �޴� ��ư�� �ʱ� ��ġ
    private Vector3[] introTextInitialPos;     // �� INTRO �ؽ�Ʈ �ʱ� ��ġ
    private Vector3 Camerainitposition; //ī�޶� �ʱⰪ��
    private Quaternion Camerainitrotation;
    float mainCamerainitfieldofView;

    public Transform m_gameOutCameraPos;
    public Transform m_gameInCameraPos;




    void Start()
    {
        // �ʱ� ī�޶�� UI ��ҵ��� ��ġ ����
        // cameraInitialPos = mainCamera.transform.position; // �ּ� ó��
        menuButtonsInitialPos = new Vector3[menuButtons.Length];
        introTextInitialPos = new Vector3[introText.Length];
        menuButtonFinalPos = new Vector3[menuButtons.Length];
        introTextFinalPos = new Vector3[introText.Length];
        CameraFinalfieldofView = mainCamera.fieldOfView;
        Camerainitposition = mainCamera.transform.position;
        Camerainitrotation = mainCamera.transform.rotation;


        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtonsInitialPos[i] = menuButtons[i].localPosition;
            menuButtonFinalPos[i] = new Vector3(menuButtons[i].localPosition.x, -800, 0);
        }
        for (int i = 0; i < introText.Length; i++)
        {
            introTextInitialPos[i] = introText[i].localPosition;
            introTextFinalPos[i] = new Vector3(introText[i].localPosition.x, 800, 0);
        }

    }

    public void StartGame()
    {
        StartCoroutine(GameStartAnimation());
    }

    public void GoingToMenu()
    {
        StartCoroutine(GoingToMenuAnimation());
    }

    public IEnumerator GameStartAnimation()
    {
        float timeElapsed = 0f;
        float animationDuration = 1f;  // �ִϸ��̼� �ð�
        float initialCamerafiledofView = mainCamera.fieldOfView;
        Debug.Log($"field:{mainCamera.fieldOfView}");
        // �޴� ��ư���� �Ʒ��� ������
        Vector3[] initialMenuPositions = new Vector3[menuButtons.Length];
        for (int i = 0; i < menuButtons.Length; i++)
        {
            initialMenuPositions[i] = menuButtons[i].localPosition;
        }
        // Intro�� �Ʒ��� ������
        Vector3[] initialIntroPositions = new Vector3[introText.Length]; for (int i = 0; i < introText.Length; i++)
        {
            initialIntroPositions[i] = introText[i].localPosition;
        }


        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;

            // �޴� ��ư�� �Ʒ��� ������
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].localPosition = Vector3.Lerp(initialMenuPositions[i], menuButtonFinalPos[i], t);
            }

            introTextEffect.StopCorutine();
            // INTRO �ؽ�Ʈ�� ���� �ø���
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].localPosition = Vector3.Lerp(initialIntroPositions[i], introTextFinalPos[i], t);
            }

            // ī�޶� �̵� 
            mainCamera.transform.position = Vector3.Lerp(m_gameOutCameraPos.position, m_gameInCameraPos.position, t);
            mainCamera.transform.rotation = Quaternion.Lerp(m_gameOutCameraPos.rotation, m_gameInCameraPos.rotation, t);
            mainCamera.fieldOfView = Mathf.Lerp(initialCamerafiledofView, 15f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ���������� ��� ��ġ�� ��Ȯ�� ���߱�
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].localPosition = menuButtonFinalPos[i];
        }
        for (int i = 0; i < introText.Length; i++)
        {
            introText[i].localPosition = introTextFinalPos[i];
        }

        mainCamera.transform.position = m_gameInCameraPos.position;
        mainCamera.transform.rotation = m_gameInCameraPos.rotation;
        mainCamera.fieldOfView = 15f;
    }

    public IEnumerator GoingToMenuAnimation()
    {
        float timeElapsed = 0f;
        float animationDuration = 1f; // �ִϸ��̼� �ð�
        float initialCameraFieldOfView = mainCamera.fieldOfView;

        // �޴� ��ư �ʱ� ��ġ�� INTRO �ؽ�Ʈ �ʱ� ��ġ
        Vector3[] initialMenuPositions = new Vector3[menuButtons.Length];
        for (int i = 0; i < menuButtons.Length; i++)
        {
            initialMenuPositions[i] = menuButtons[i].localPosition;
        }

        Vector3[] initialIntroPositions = new Vector3[introText.Length];
        for (int i = 0; i < introText.Length; i++)
        {
            initialIntroPositions[i] = introText[i].localPosition;
        }

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;

            // �޴� ��ư�� ���� �ø���
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].localPosition = Vector3.Lerp(initialMenuPositions[i], menuButtonsInitialPos[i], t);
            }

            // INTRO �ؽ�Ʈ�� �Ʒ��� ������
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].localPosition = Vector3.Lerp(initialIntroPositions[i], introTextInitialPos[i], t);
            }

            // ī�޶� �̵�
            mainCamera.transform.position = Vector3.Lerp(m_gameInCameraPos.position, m_gameOutCameraPos.position, t);
            mainCamera.transform.rotation = Quaternion.Lerp(m_gameInCameraPos.rotation, m_gameOutCameraPos.rotation, t);
            mainCamera.fieldOfView = Mathf.Lerp(initialCameraFieldOfView, CameraFinalfieldofView, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ���������� ��� ��ġ�� ��Ȯ�� ���߱�
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].localPosition = menuButtonsInitialPos[i];
        }
        for (int i = 0; i < introText.Length; i++)
        {
            introText[i].localPosition = introTextInitialPos[i];
        }

        mainCamera.transform.position = m_gameOutCameraPos.position;
        mainCamera.transform.rotation = m_gameOutCameraPos.rotation;
        mainCamera.fieldOfView = CameraFinalfieldofView;
    }

}
