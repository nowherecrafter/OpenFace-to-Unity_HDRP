using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlendshapeAnimator : MonoBehaviour
{
    AnimParam param;
    SkinnedMeshRenderer[] smRenderers;
    Transform headBone;
    Transform jawBone;
    Transform eyeLeftBone;
    Transform eyeRightBone;

    Frame firstFrame;


    private void Start()
    {


        SetFeedback();
        SetAvatarProps();

        firstFrame = AppManager.Instance.getFirstFrame();
        param = AppManager.Instance.parameters;

    }

    public void SetFeedback()
    {
        firstFrame = AppManager.Instance.getFirstFrame();
        param = AppManager.Instance.parameters;
    }

    public void SetAvatarProps()
    {
        smRenderers = AppManager.Instance.GetSMRenderers();
        headBone = AppManager.Instance.GetHeadBone();

        jawBone = headBone.GetChild(0).GetChild(0);
        eyeLeftBone = headBone.GetChild(0).GetChild(1);
        eyeRightBone = headBone.GetChild(0).GetChild(2);
    }

    public  void SetFExpression(Frame frame, Frame nextFrame, float frameTime)
    {
        int bsIndex;
        Vector3 vectorTemp;

        // Disable Salsa eyelid animation if respective action units are recognised
        if (AppManager.Instance.GetEyeAnimsEnabled() && (frame.AU05_c || frame.AU07_c || frame.AU45_c))
            AppManager.Instance.SetEyeAnimsEnabled(false);

        if (!AppManager.Instance.GetEyeAnimsEnabled() && !(frame.AU05_c || frame.AU07_c || frame.AU45_c))
            AppManager.Instance.SetEyeAnimsEnabled(true);

        // Disable Salsa eyelid animation if respective action units are recognised
        if (AppManager.Instance.GetEyeAnimsEnabled() && (frame.AU05_c || frame.AU07_c || frame.AU45_c))
            AppManager.Instance.SetEyeAnimsEnabled(false);

        if (!AppManager.Instance.GetEyeAnimsEnabled() && !(frame.AU05_c || frame.AU07_c || frame.AU45_c))
            AppManager.Instance.SetEyeAnimsEnabled(true);


        //Debug.LogWarning("frame nb : " + frame.frame.ToString() + " AU45_c : " + frame.AU45_c + " AU45_r : " + frame.AU45_r);

        // Blendshape update
        foreach (SkinnedMeshRenderer renderer in smRenderers)
        {
            


            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Inner_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU01_c ? frame.AU01_r : 0f, nextFrame.AU01_c ? nextFrame.AU01_r : 0f, frameTime) * param.AU01_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Inner_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU01_c ? frame.AU01_r : 0f, nextFrame.AU01_c ? nextFrame.AU01_r : 0f, frameTime) * param.AU01_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Outer_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU02_c ? frame.AU02_r : 0f, nextFrame.AU02_c ? nextFrame.AU02_r : 0f, frameTime) * param.AU02_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Outer_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU02_c ? frame.AU02_r : 0f, nextFrame.AU02_c ? nextFrame.AU02_r : 0f, frameTime) * param.AU02_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Drop_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU04_c ? frame.AU04_r : 0f, nextFrame.AU04_c ? nextFrame.AU04_r : 0f, frameTime) * param.AU04_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Drop_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU04_c ? frame.AU04_r : 0f, nextFrame.AU04_c ? nextFrame.AU04_r : 0f, frameTime) * param.AU04_r_coef);


            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Wide_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU05_c ? frame.AU05_r : 0f, nextFrame.AU05_c ? nextFrame.AU05_r : 0f, frameTime) * param.AU05_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Wide_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU05_c ? frame.AU05_r : 0f, nextFrame.AU05_c ? nextFrame.AU05_r : 0f, frameTime) * param.AU05_r_coef);

            



            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Cheek_Raise_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU06_c ? frame.AU06_r : 0f, nextFrame.AU06_c ? nextFrame.AU06_r : 0f, frameTime) * param.AU06_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Cheek_Raise_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU06_c ? frame.AU06_r : 0f, nextFrame.AU06_c ? nextFrame.AU06_r : 0f, frameTime) * param.AU06_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Squint_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU07_c ? frame.AU07_r : 0f, nextFrame.AU07_c ? nextFrame.AU07_r : 0f, frameTime) * param.AU07_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Squint_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU07_c ? frame.AU07_r : 0f, nextFrame.AU07_c ? nextFrame.AU07_r : 0f, frameTime) * param.AU07_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Nose_Sneer_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU09_c ? frame.AU09_r : 0f, nextFrame.AU09_c ? nextFrame.AU09_r : 0f, frameTime) * param.AU09_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Nose_Sneer_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU09_c ? frame.AU09_r : 0f, nextFrame.AU09_c ? nextFrame.AU09_r : 0f, frameTime) * param.AU09_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Up_Upper_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU10_c ? frame.AU10_r : 0f, nextFrame.AU10_c ? nextFrame.AU10_r : 0f, frameTime) * param.AU10_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Up_Upper_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU10_c ? frame.AU10_r : 0f, nextFrame.AU10_c ? nextFrame.AU10_r : 0f, frameTime) * param.AU10_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Smile_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU12_c ? frame.AU12_r : 0f, nextFrame.AU12_c ? nextFrame.AU12_r : 0f, frameTime) * param.AU12_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Smile_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU12_c ? frame.AU12_r : 0f, nextFrame.AU12_c ? nextFrame.AU12_r : 0f, frameTime) * param.AU12_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Dimple_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU14_c ? frame.AU14_r : 0f, nextFrame.AU14_c ? nextFrame.AU14_r : 0f, frameTime) * param.AU14_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Dimple_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU14_c ? frame.AU14_r : 0f, nextFrame.AU14_c ? nextFrame.AU14_r : 0f, frameTime) * param.AU14_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Frown_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU15_c ? frame.AU15_r : 0f, nextFrame.AU15_c ? nextFrame.AU15_r : 0f, frameTime) * param.AU15_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Frown_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU15_c ? frame.AU15_r : 0f, nextFrame.AU15_c ? nextFrame.AU15_r : 0f, frameTime) * param.AU15_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Chin_Up");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU17_c ? frame.AU17_r : 0f, nextFrame.AU17_c ? nextFrame.AU17_r : 0f, frameTime) * param.AU17_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Stretch_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU20_c ? frame.AU20_r : 0f, nextFrame.AU20_c ? nextFrame.AU20_r : 0f, frameTime) * param.AU20_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Stretch_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU20_c ? frame.AU20_r : 0f, nextFrame.AU20_c ? nextFrame.AU20_r : 0f, frameTime) * param.AU20_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Tighten_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU23_c ? frame.AU23_r : 0f, nextFrame.AU23_c ? nextFrame.AU23_r : 0f, frameTime) * param.AU23_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Tighten_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU23_c ? frame.AU23_r : 0f, nextFrame.AU23_c ? nextFrame.AU23_r : 0f, frameTime) * param.AU23_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("V_Lip_Open");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU25_c ? frame.AU25_r : 0f, nextFrame.AU25_c ? nextFrame.AU25_r : 0f, frameTime) * param.AU25_r_coef);




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Blink_L");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU45_c ? frame.AU45_r : 0f, nextFrame.AU45_c ? nextFrame.AU45_r : 0f, frameTime) * param.AU45_r_coef);

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Blink_R");

            if (bsIndex != -1)
                renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU45_c ? frame.AU45_r : 0f, nextFrame.AU45_c ? nextFrame.AU45_r : 0f, frameTime) * param.AU45_r_coef);


            


        }


        // Head mouvement
        vectorTemp = headBone.transform.rotation.eulerAngles;

        vectorTemp.x = (Mathf.Lerp(frame.pose_Rx * 57.2958f, nextFrame.pose_Rx * 57.2958f, frameTime) - firstFrame.pose_Rx * 57.2958f) * param.pose_Rx_coef;
        vectorTemp.y = 180 + (Mathf.Lerp(frame.pose_Ry * 57.2958f, nextFrame.pose_Ry * 57.2958f, frameTime) - firstFrame.pose_Ry * 57.2958f) * param.pose_Ry_coef ;
        vectorTemp.z = (Mathf.Lerp(frame.pose_Rz * 57.2958f, nextFrame.pose_Rz * 57.2958f, frameTime) - firstFrame.pose_Rz * 57.2958f) * param.pose_Rz_coef;
        headBone.transform.rotation = Quaternion.Euler(vectorTemp);

        //// Jaw mouvement. In conflict with Salsa 
        //vectorTemp = jawBone.transform.localRotation.eulerAngles;

        //jawBone.transform.localRotation = Quaternion.identity;

        //vectorTemp.z = -180;
        //vectorTemp.z += Mathf.Lerp(frame.AU25_c ? frame.AU25_r : 0f, nextFrame.AU25_c ? nextFrame.AU25_r : 0f, frameTime) * param.AU25_jaw_coef;
        //vectorTemp.z += Mathf.Lerp(frame.AU26_c ? frame.AU26_r : 0f, nextFrame.AU26_c ? nextFrame.AU26_r : 0f, frameTime) * param.AU26_jaw_coef;
     
        //jawBone.transform.localRotation = Quaternion.Euler(vectorTemp);

        
        //// Eyes mouvement. In conflict with Salsa 
        
        //vectorTemp = eyeLeftBone.transform.rotation.eulerAngles;

        //eyeLeftBone.transform.rotation = Quaternion.identity;

        //vectorTemp.z = 90 + (Mathf.Lerp(frame.gaze_angle_x * 57.2958f, nextFrame.gaze_angle_x * 57.2958f, frameTime) - firstFrame.gaze_angle_x * 57.2958f);
        //vectorTemp.y = 90 + (Mathf.Lerp(frame.gaze_angle_y * 57.2958f, nextFrame.gaze_angle_y * 57.2958f, frameTime) - firstFrame.gaze_angle_y * 57.2958f);

        //eyeLeftBone.transform.rotation = Quaternion.Euler(vectorTemp);
        //Debug.Log("Left: " + vectorTemp.y + " | " + vectorTemp.z);

        //vectorTemp = eyeRightBone.transform.localRotation.eulerAngles;

        //eyeRightBone.transform.localRotation = Quaternion.identity;

        //vectorTemp.z = 90 + (Mathf.Lerp(frame.gaze_angle_x * 57.2958f, nextFrame.gaze_angle_x * 57.2958f, frameTime) - firstFrame.gaze_angle_x * 57.2958f);
        //vectorTemp.y = (Mathf.Lerp(frame.gaze_angle_y * 57.2958f, nextFrame.gaze_angle_y * 57.2958f, frameTime) - firstFrame.gaze_angle_y * 57.2958f);

        //eyeRightBone.transform.localRotation = Quaternion.Euler(vectorTemp);
        
    }

}
