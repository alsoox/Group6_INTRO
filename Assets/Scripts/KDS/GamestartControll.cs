using System.Collections;
using UnityEngine;

public class GamestartControll : MonoBehaviour
{
    public RectTransform[] menuButtons; // �޴� ��ư��
    public RectTransform[] introText;     // INTRO �ؽ�Ʈ��
    public Camera mainCamera;        
    public Vector3[] introTextFinalPos;  // INTRO �ؽ�Ʈ�� ���� ��ġ
    public Vector3[] menuButtonFinalPos; // �޴� ��ư ���� ��ġ
    private Vector3 CameraFinalposition= new Vector3(0, 5, -0.5f); //ī�޶� ������ġ
    private Quaternion CameraFinalrotation= Quaternion.Euler(90, 0, 0);
    float CameraFinalfieldofView= 15f;

    //private Vector3 cameraInitialPos;
    private Vector3[] menuButtonsInitialPos;  // �� �޴� ��ư�� �ʱ� ��ġ
    private Vector3[] introTextInitialPos;     // �� INTRO �ؽ�Ʈ �ʱ� ��ġ
    private Vector3 Camerainitposition; //ī�޶� �ʱⰪ��
    private Quaternion Camerainitrotation;
    float mainCamerainitfieldofView;
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

    public IEnumerator GameStartAnimation()
    {
        float timeElapsed = 0f;
        float animationDuration = 1f;  // �ִϸ��̼� �ð�
        Vector3 initilaCameraPos=mainCamera.transform.position;
        Quaternion initialCameraLot=mainCamera.transform.rotation;
        float initialCamerafiledofView = mainCamera.fieldOfView;
        Debug.Log($"field:{mainCamera.fieldOfView}");
        // �޴� ��ư���� �Ʒ��� ������
        Vector3[] initialMenuPositions = new Vector3[menuButtons.Length];
        for (int i = 0; i < menuButtons.Length; i++)
        {
            initialMenuPositions[i] = menuButtons[i].localPosition;
        }
        // Intro�� �Ʒ��� ������
        Vector3[] initialIntroPositions = new Vector3[introText.Length];
        for (int i = 0; i < introText.Length; i++)
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

            // INTRO �ؽ�Ʈ�� ���� �ø���
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].localPosition = Vector3.Lerp(initialIntroPositions[i], introTextFinalPos[i], t);
            }

            // ī�޶� �̵� 
            mainCamera.transform.position = Vector3.Lerp(initilaCameraPos, CameraFinalposition, t); 
            mainCamera.transform.rotation = Quaternion.Lerp(initialCameraLot, CameraFinalrotation, t);
            mainCamera.fieldOfView = Mathf.Lerp(initialCamerafiledofView, 15f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
       
        // ���������� ��� ��ġ�� ��Ȯ�� ���߱�
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].localPosition = menuButtonFinalPos[i];
            Debug.Log(menuButtons[i].localPosition.y);
        }
        for (int i = 0; i < introText.Length; i++)
        {
            introText[i].localPosition = introTextFinalPos[i];
        }

        mainCamera.transform.position = CameraFinalposition;
        mainCamera.transform.rotation = CameraFinalrotation;
        mainCamera.fieldOfView = 15f ;
    }
}
