using System.Collections.Generic;
using CrazyMinnow.SALSA;
using UnityEngine;
using UnityEngine.Video;




public class AppManager : MonoBehaviour
{

    bool vidSync = true;

    FeedbackList feedbackListItems;


    //Resources and interpreters
    string audioClipPath = "Audio/ah_cest_horrible";
    private AudioSource audioSource;
    string videoClipPath = "Video/ah_cest_horrible";
    public VideoPlayer player;
    string openFacePath = "OpenFace/ah_cest_horrible";
    // A .csv file generated by OpenFace. Should include AU's, Pose and Gaze ONLY!!!
    public TextAsset actionUnitData;



    // Video player controls
    public KeyCode playKey = KeyCode.LeftControl;



    // Game Objects manipulated by the script
    public Transform Camera;
    public Transform activeAvatar;
    public GameObject[] avatars;
    Transform headBone;
    SkinnedMeshRenderer[] smRenderers = new SkinnedMeshRenderer[2];
    AudioSource voice;
    // Eyes object in SALSA
    Eyes eyes;

    public AnimParam parameters;

    List<FeedbackElement> items;

    [SerializeField]BlendshapeAnimator blendshapeAnimator;

    // Videoplayer related variables
    float time = 0f;
    bool play = false;
    float frameTime;
    int frameIndex = 0;

    // A list of structures containing data frame by frame
    List<Frame> frames = new List<Frame>();





    public FeedbackElement GetFeedback() { return items[frameIndex]; }
    public Transform GetCamera() { return Camera; }

    public SkinnedMeshRenderer[] GetSMRenderers() { return smRenderers; }

    public Transform GetHeadBone() { return headBone; }

    public Frame getFirstFrame() { return frames[0]; }

    public FeedbackList GetFeedbackList() { return feedbackListItems; }

    public static AppManager Instance { get; private set; }

    public bool GetEyeAnimsEnabled()
    {
        return eyes.blinkEnabled || eyes.eyeEnabled; // true if any is enabled
    }

    public void SetEyeAnimsEnabled(bool status)
    {
        eyes.EnableEyelidBlink(status);
        eyes.EnableEye(status);
    }

    public void ToggleVidSync(bool value)
    {
        vidSync = value;
    }

    public void LoadFeedbackRessources(int feedbackID)
    {

        audioClipPath = feedbackListItems.elements[feedbackID].path_Audio;
        audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);

        if (audioClip != null)
        {
            // Set the loaded audio clip to the AudioSource
            audioSource.clip = audioClip;

        }
        else
        {
            Debug.LogError("Failed to load audio clip from Resources folder: " + audioClipPath);
        }


        videoClipPath = feedbackListItems.elements[feedbackID].path_Video;
        VideoClip videoClip = Resources.Load<VideoClip>(videoClipPath);

        if (videoClip != null)
        {
            // Set the loaded video clip to the VideoPlayer
            player.clip = videoClip;

        }
        else
        {
            Debug.LogError("Failed to load video clip from Resources folder: " + videoClipPath);
        }

        openFacePath = feedbackListItems.elements[feedbackID].path_OpenFace;
        actionUnitData = Resources.Load<TextAsset>(openFacePath);
        frames = CsvImporter.ParseCSV(actionUnitData);
        blendshapeAnimator.SetFeedback();

        if (actionUnitData == null)
        {
            Debug.LogError("Failed to load OpenFace csv from Resources folder: " + openFacePath);
        }
        
    }

    public void SetAvatarProps()
    {
        headBone = activeAvatar.Find("CC_Base_BoneRoot").GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0);


        smRenderers[0] = activeAvatar.Find("Brows").GetComponent<SkinnedMeshRenderer>();
        smRenderers[1] = activeAvatar.Find("CC_Base_Body").GetComponent<SkinnedMeshRenderer>();
        //smRenderers[2] = activeAvatar.GetChild(0).Find("Brows_Extracted0").GetComponent<SkinnedMeshRenderer>();

        eyes = activeAvatar.GetComponent<Eyes>();
    }
    public void ChangeActiveAvatar(int value)
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            if (i == value)
            {
                avatars[i].SetActive(true);
            }
            else
            {
                avatars[i].SetActive(false);
            }
        }

        activeAvatar = avatars[value].transform;
        SetAvatarProps();
        blendshapeAnimator.SetAvatarProps();
    }

    public void GlobalPlay()
    {
        // Check if the VideoPlayer component is attributed
        if (vidSync && player != null)
        {
            // Prepare and play the video assigned to the VideoPlayer component only then play the avatar's animation
            player.Prepare();
            if (player.isPrepared)
            {

                player.Play();

                play = true;

                voice.Play();
            }

        }
        else
        {
            //if there's no VideoPlayer assigned, the animation is good on its own
            play = true;

            voice.Play();
        }
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        feedbackListItems = JsonImporter.ParseJSON();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        SetAvatarProps();

        voice = GetComponent<AudioSource>();

        frames = CsvImporter.ParseCSV(actionUnitData);

        //player.Play();

        LoadFeedbackRessources(1);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the assigned key is pressed
        if (Input.GetKeyDown(playKey))
        {

            GlobalPlay();
            
        }
        


        if (play)
        {
            if (vidSync && player != null) 
            {
                
                if ((int)player.frame > -1)
                {
                    if ((int)player.frame < frames.Count -1)
                    {
                        // calculating the time passed on the current frame
                        frameTime = (time - frames[(int)player.frame].timestamp) / (frames[(int)player.frame + 1].timestamp - frames[(int)player.frame].timestamp);

                        // setting expression between the current frame and the one that follows
                        blendshapeAnimator.SetFExpression(frames[(int)player.frame], frames[(int)player.frame + 1], frameTime);

                        // set back animation time if VideoPlayer lags behind
                        if (time > player.time + 0.04 || time < player.time)
                            time = frames[(int)player.frame].timestamp;

                    }
                    else if ((int)player.frame == frames.Count - 1)
                    {
                        // calculating the time passed on the current frame
                        frameTime = (time - frames[(int)player.frame].timestamp) / (frames[(int)player.frame].timestamp - frames[(int)player.frame - 1].timestamp);

                        // instead of 2 different frames we prolong the final one
                        blendshapeAnimator.SetFExpression(frames[(int)player.frame], frames[(int)player.frame], frameTime);

                        // reset values after the video ends
                        play = false;
                        Instance.SetEyeAnimsEnabled(true);
                        time = 0f;
                    }
                    
                }

            }
            else
            {

                if (frameIndex < frames.Count - 1)
                {
                    // calculating the time passed between frames
                    frameTime = (time - frames[frameIndex].timestamp) / (frames[frameIndex + 1].timestamp - frames[frameIndex].timestamp);

                    // setting expression between the current frame and the one that follows
                    blendshapeAnimator.SetFExpression(frames[frameIndex], frames[frameIndex + 1], frameTime);


                }
                // case for the last frame
                else if (frameIndex == frames.Count - 1)
                {
                    // calculating the time passed between frames
                    frameTime = (time - frames[frameIndex].timestamp) / (frames[frameIndex].timestamp - frames[frameIndex - 1].timestamp);

                    // instead of 2 different frames we prolong the final one
                    blendshapeAnimator.SetFExpression(frames[frameIndex], frames[frameIndex], frameTime);

                    // reset values after the video ends
                    play = false;
                    Instance.SetEyeAnimsEnabled(true);
                    frameIndex = 0;
                    time = 0f;
                }

                if (time > frames[frameIndex].timestamp)
                {
                    

                    frameIndex++;
                }



            }

            // time is unstoppable
            time += Time.deltaTime;

            //bsIndex = smRenderers[1].sharedMesh.GetBlendShapeIndex("Mouth_Smile_L");
            //Debug.Log("frame " + frames[frameIndex].frame + " | time " + time + " | timestamp " + frames[frameIndex].timestamp + " | AU " + frames[frameIndex].AU12_r + " | BS " + smRenderers[1].GetBlendShapeWeight(bsIndex));



        }
    }


}
