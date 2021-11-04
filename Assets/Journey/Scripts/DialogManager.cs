using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class DialogManager : MonoBehaviour
{
    //대화창 초상화
    [SerializeField] VideoPlayer portrait;
    [SerializeField] VideoClip[] videos;
    //0 = Norbal
    //1 = Exciting
    //2 = Worried
    //3 = Disappointment

    //타이핑 효과 속도
    [SerializeField] float TypingPerSecond = 0.05f;

    //json 파일명
    [SerializeField] string dialogFile = "dialog";

    //대사가 다 출력 된 상태인지
    bool isEndTyping;

    //대사 저장 큐
    [SerializeField] Queue<string> sentences;
    [SerializeField] Queue<int> videoIndex;

    //대사 임시 저장
    string sentenceBuffer;

    [SerializeField] Animator animator; //텍스트 애니메이션
    [SerializeField] GameObject nextIcon; //다음 대사 넘기기 아이콘 (세모 모양)
    [SerializeField] Dialogue dialog; //대화창
    [SerializeField] Text dialogueText; //대화 텍스트

    private void Awake()
    {
        sentences = new Queue<string>();
        videoIndex = new Queue<int>();
    }

    private void Start()
    {
        nextIcon.SetActive(false);

        LoadFromJson(dialogFile);

        StartDialogue(dialog);
    }

    private void Update()
    {
        nextIcon.SetActive(isEndTyping);

        //다음 문장 출력
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(isEndTyping)
            {
                //다음 대사 띄우기
                DisplayNextSentence();
                isEndTyping = false;
            }
            else
            {
                //남은 대사 출력
                StopAllCoroutines();
                dialogueText.text = sentenceBuffer;
                isEndTyping = true;
            }
        }
    }

    //Json 불러오기
    public void LoadFromJson(string fileName)
    {
        string path = Path.Combine(Application.dataPath, "Journey", "Dialog", fileName + ".json");

        string json = File.ReadAllText(path);
        dialog = JsonUtility.FromJson<Dialogue>(json);
    }

    //대화 시작
    public void StartDialogue(Dialogue dialogue)
    {
        StopAllCoroutines();
        dialogueText.text = "";

        sentences.Clear();
        videoIndex.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (int index in dialogue.videoIndex)
        {
            videoIndex.Enqueue(index);
        }

        DisplayNextSentence();
    }

    //다음 문장 출력
    public void DisplayNextSentence()
    {
        //대사가 끝났다면
        if (sentences.Count == 0)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Rhythm");
            //SceneController.Instance.ChangeScene("Rhythm");
            return;
        }

        string sentence = sentences.Dequeue();
        portrait.clip = videos[videoIndex.Dequeue()];

        //대사 임시 저장
        sentenceBuffer = sentence;

        StopAllCoroutines();
        StartCoroutine(TypingSentence(sentence));
    }

    //글자 타이핑 효과
    IEnumerator TypingSentence(string sentence)
    {
        //타이핑 시작
        isEndTyping = false;

        //초기화
        dialogueText.text = "";

        //타이핑 효과
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(TypingPerSecond);
        }

        //타이핑 종료
        isEndTyping = true;
    }
}
