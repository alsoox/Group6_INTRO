using System.Collections;
using UnityEngine;

public class GamestartControll : MonoBehaviour
{
    public RectTransform[] menuButtons; // 메뉴 버튼들
    public RectTransform[] introText;     // INTRO 텍스트들
    public Camera mainCamera;        
    public Vector3[] introTextFinalPos;  // INTRO 텍스트의 최종 위치
    public Vector3[] menuButtonFinalPos; // 메뉴 버튼 최종 위치
    private Vector3 CameraFinalposition= new Vector3(0, 5, -0.5f); //카메라 최종위치
    private Quaternion CameraFinalrotation= Quaternion.Euler(90, 0, 0);
    float CameraFinalfieldofView= 15f;

    //private Vector3 cameraInitialPos;
    private Vector3[] menuButtonsInitialPos;  // 각 메뉴 버튼의 초기 위치
    private Vector3[] introTextInitialPos;     // 각 INTRO 텍스트 초기 위치
    private Vector3 Camerainitposition; //카메라 초기값들
    private Quaternion Camerainitrotation;
    float mainCamerainitfieldofView;
    void Start()
    {
        // 초기 카메라와 UI 요소들의 위치 저장
        // cameraInitialPos = mainCamera.transform.position; // 주석 처리
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
        float animationDuration = 1f;  // 애니메이션 시간
        Vector3 initilaCameraPos=mainCamera.transform.position;
        Quaternion initialCameraLot=mainCamera.transform.rotation;
        float initialCamerafiledofView = mainCamera.fieldOfView;
        Debug.Log($"field:{mainCamera.fieldOfView}");
        // 메뉴 버튼들을 아래로 내리기
        Vector3[] initialMenuPositions = new Vector3[menuButtons.Length];
        for (int i = 0; i < menuButtons.Length; i++)
        {
            initialMenuPositions[i] = menuButtons[i].localPosition;
        }
        // Intro를 아래로 내리기
        Vector3[] initialIntroPositions = new Vector3[introText.Length];
        for (int i = 0; i < introText.Length; i++)
        {
            initialIntroPositions[i] = introText[i].localPosition;
        }


        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;

            // 메뉴 버튼을 아래로 내리기
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].localPosition = Vector3.Lerp(initialMenuPositions[i], menuButtonFinalPos[i], t);
            }

            // INTRO 텍스트를 위로 올리기
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].localPosition = Vector3.Lerp(initialIntroPositions[i], introTextFinalPos[i], t);
            }

            // 카메라 이동 
            mainCamera.transform.position = Vector3.Lerp(initilaCameraPos, CameraFinalposition, t); 
            mainCamera.transform.rotation = Quaternion.Lerp(initialCameraLot, CameraFinalrotation, t);
            mainCamera.fieldOfView = Mathf.Lerp(initialCamerafiledofView, 15f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
       
        // 최종적으로 모든 위치를 정확히 맞추기
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
