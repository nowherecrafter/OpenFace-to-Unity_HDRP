using System;
using System.Collections;
using System.Collections.Generic;
using CrazyMinnow.SALSA;
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
    float frameTime;

    List<Frame> ogFrames;
    int firstFrameIndex;
    
    List<Frame> diffs;
    Frame means;
    List<Frame> deviations;
    Frame stdDeviations;

    List<Frame> corFrames;
   
    
    public void ResetStdDeviation()
    {
        diffs = new List<Frame>();
        means = new Frame();
        deviations = new List<Frame>();
        stdDeviations = new Frame();

        corFrames = new List<Frame>();
    }
    public void SetFeedback()
    {
        ogFrames = AppManager.Instance.GetOgFrames();
        firstFrameIndex = AppManager.Instance.GetFirstFrameIndex();
        if (AppManager.Instance.noiseReduced)
        {
            ResetStdDeviation();
            CalcStdDeviations(ogFrames);
            CorrectFrames();
        }
        else
            corFrames = ogFrames;

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

    public void CalcStdDeviations(List<Frame> frames)
    {

        // Calculating differences between frames
        for (int i = firstFrameIndex; i < frames.Count - 1; i++)
        {
            Frame newFrame = new Frame();

            newFrame.frame = i;
            newFrame.face_id = 0;
            newFrame.timestamp = frames[i].timestamp;
            newFrame.confidence = 0.97f;
            newFrame.success = true;

            newFrame.gaze_angle_x = (float)Math.Abs(frames[i].gaze_angle_x - frames[i + 1].gaze_angle_x);
            newFrame.gaze_angle_y = (float)Math.Abs(frames[i].gaze_angle_y - frames[i + 1].gaze_angle_y);

            newFrame.pose_Rx = (float)Math.Abs(frames[i].pose_Rx - frames[i + 1].pose_Rx);
            newFrame.pose_Ry = (float)Math.Abs(frames[i].pose_Ry - frames[i + 1].pose_Ry);
            newFrame.pose_Rz = (float)Math.Abs(frames[i].pose_Rz - frames[i + 1].pose_Rz);

            newFrame.AU01_r = (float)Math.Abs(frames[i].AU01_r - frames[i + 1].AU01_r);
            newFrame.AU02_r = (float)Math.Abs(frames[i].AU02_r - frames[i + 1].AU02_r);
            newFrame.AU04_r = (float)Math.Abs(frames[i].AU04_r - frames[i + 1].AU04_r);
            newFrame.AU05_r = (float)Math.Abs(frames[i].AU05_r - frames[i + 1].AU05_r);
            newFrame.AU06_r = (float)Math.Abs(frames[i].AU06_r - frames[i + 1].AU06_r);
            newFrame.AU07_r = (float)Math.Abs(frames[i].AU07_r - frames[i + 1].AU07_r);
            newFrame.AU09_r = (float)Math.Abs(frames[i].AU09_r - frames[i + 1].AU09_r);
            newFrame.AU10_r = (float)Math.Abs(frames[i].AU10_r - frames[i + 1].AU10_r);
            newFrame.AU12_r = (float)Math.Abs(frames[i].AU12_r - frames[i + 1].AU12_r);
            newFrame.AU14_r = (float)Math.Abs(frames[i].AU14_r - frames[i + 1].AU14_r);
            newFrame.AU15_r = (float)Math.Abs(frames[i].AU15_r - frames[i + 1].AU15_r);
            newFrame.AU17_r = (float)Math.Abs(frames[i].AU17_r - frames[i + 1].AU17_r);
            newFrame.AU20_r = (float)Math.Abs(frames[i].AU20_r - frames[i + 1].AU20_r);
            newFrame.AU23_r = (float)Math.Abs(frames[i].AU23_r - frames[i + 1].AU23_r);
            newFrame.AU25_r = (float)Math.Abs(frames[i].AU25_r - frames[i + 1].AU25_r);
            newFrame.AU26_r = (float)Math.Abs(frames[i].AU26_r - frames[i + 1].AU26_r);
            newFrame.AU45_r = (float)Math.Abs(frames[i].AU45_r - frames[i + 1].AU45_r);

            newFrame.AU01_c = false;
            newFrame.AU02_c = false;
            newFrame.AU04_c = false;
            newFrame.AU05_c = false;
            newFrame.AU06_c = false;
            newFrame.AU07_c = false;
            newFrame.AU09_c = false;
            newFrame.AU10_c = false;
            newFrame.AU12_c = false;
            newFrame.AU14_c = false;
            newFrame.AU15_c = false;
            newFrame.AU17_c = false;
            newFrame.AU20_c = false;
            newFrame.AU23_c = false;
            newFrame.AU25_c = false;
            newFrame.AU26_c = false;
            newFrame.AU28_c = false;
            newFrame.AU45_c = false;

            diffs.Add(newFrame);
        }

        // Sums of properties initially equal to 0
        float gaze_angle_x_sum = 0;
        float gaze_angle_y_sum = 0;

        float pose_Rx_sum = 0;
        float pose_Ry_sum = 0;
        float pose_Rz_sum = 0;

        float AU01_r_sum = 0;
        float AU02_r_sum = 0;
        float AU04_r_sum = 0;
        float AU05_r_sum = 0;
        float AU06_r_sum = 0;
        float AU07_r_sum = 0;
        float AU09_r_sum = 0;
        float AU10_r_sum = 0;
        float AU12_r_sum = 0;
        float AU14_r_sum = 0;
        float AU15_r_sum = 0;
        float AU17_r_sum = 0;
        float AU20_r_sum = 0;
        float AU23_r_sum = 0;
        float AU25_r_sum = 0;
        float AU26_r_sum = 0;
        float AU45_r_sum = 0;

        // Calculating summs of differences
        foreach (Frame diff in diffs)
        {
            gaze_angle_x_sum += diff.gaze_angle_x;
            gaze_angle_y_sum += diff.gaze_angle_y;

            pose_Rx_sum += diff.pose_Rx;
            pose_Ry_sum += diff.pose_Ry;
            pose_Rz_sum += diff.pose_Rz;

            AU01_r_sum += diff.AU01_r;
            AU02_r_sum += diff.AU02_r;
            AU04_r_sum += diff.AU04_r;
            AU05_r_sum += diff.AU05_r;
            AU06_r_sum += diff.AU06_r;
            AU07_r_sum += diff.AU07_r;
            AU09_r_sum += diff.AU09_r;
            AU10_r_sum += diff.AU10_r;
            AU12_r_sum += diff.AU12_r;
            AU14_r_sum += diff.AU14_r;
            AU15_r_sum += diff.AU15_r;
            AU17_r_sum += diff.AU17_r;
            AU20_r_sum += diff.AU20_r;
            AU23_r_sum += diff.AU23_r;
            AU25_r_sum += diff.AU25_r;
            AU26_r_sum += diff.AU26_r;
            AU45_r_sum += diff.AU45_r;
        }

        // Calculating means
        {
            means.frame = 0;
            means.face_id = 0;
            means.timestamp = 0f;
            means.confidence = 0.97f;
            means.success = true;

            means.gaze_angle_x = gaze_angle_x_sum / diffs.Count;
            means.gaze_angle_y = gaze_angle_y_sum / diffs.Count;

            means.pose_Rx = pose_Rx_sum / diffs.Count;
            means.pose_Ry = pose_Ry_sum / diffs.Count;
            means.pose_Rz = pose_Rz_sum / diffs.Count;

            means.AU01_r = AU01_r_sum / diffs.Count;
            means.AU02_r = AU02_r_sum / diffs.Count;
            means.AU04_r = AU04_r_sum / diffs.Count;
            means.AU05_r = AU05_r_sum / diffs.Count;
            means.AU06_r = AU06_r_sum / diffs.Count;
            means.AU07_r = AU07_r_sum / diffs.Count;
            means.AU09_r = AU09_r_sum / diffs.Count;
            means.AU10_r = AU10_r_sum / diffs.Count;
            means.AU12_r = AU12_r_sum / diffs.Count;
            means.AU14_r = AU14_r_sum / diffs.Count;
            means.AU15_r = AU15_r_sum / diffs.Count;
            means.AU17_r = AU17_r_sum / diffs.Count;
            means.AU20_r = AU20_r_sum / diffs.Count;
            means.AU23_r = AU23_r_sum / diffs.Count;
            means.AU25_r = AU25_r_sum / diffs.Count;
            means.AU26_r = AU26_r_sum / diffs.Count;
            means.AU45_r = AU45_r_sum / diffs.Count;

            means.AU01_c = false;
            means.AU02_c = false;
            means.AU04_c = false;
            means.AU05_c = false;
            means.AU06_c = false;
            means.AU07_c = false;
            means.AU09_c = false;
            means.AU10_c = false;
            means.AU12_c = false;
            means.AU14_c = false;
            means.AU15_c = false;
            means.AU17_c = false;
            means.AU20_c = false;
            means.AU23_c = false;
            means.AU25_c = false;
            means.AU26_c = false;
            means.AU28_c = false;
            means.AU45_c = false;
        }
        // Calculating deviations
        for (int i = firstFrameIndex; i < diffs.Count; i++)
        {
            Frame newFrame = new Frame();

            newFrame.frame = i;
            newFrame.face_id = 0;
            newFrame.timestamp = diffs[i].timestamp;
            newFrame.confidence = 0.97f;
            newFrame.success = true;

            newFrame.gaze_angle_x = (float)Math.Pow(diffs[i].gaze_angle_x - means.gaze_angle_x, 2f);
            newFrame.gaze_angle_y = (float)Math.Pow(diffs[i].gaze_angle_y - means.gaze_angle_y, 2f);

            newFrame.pose_Rx = (float)Math.Pow(diffs[i].pose_Rx - means.pose_Rx, 2f);
            newFrame.pose_Ry = (float)Math.Pow(diffs[i].pose_Ry - means.pose_Ry, 2f);
            newFrame.pose_Rz = (float)Math.Pow(diffs[i].pose_Rz - means.pose_Rz, 2f);

            newFrame.AU01_r = (float)Math.Pow(diffs[i].AU01_r - means.AU01_r, 2f);
            newFrame.AU02_r = (float)Math.Pow(diffs[i].AU02_r - means.AU02_r, 2f);
            newFrame.AU04_r = (float)Math.Pow(diffs[i].AU04_r - means.AU04_r, 2f);
            newFrame.AU05_r = (float)Math.Pow(diffs[i].AU05_r - means.AU05_r, 2f);
            newFrame.AU06_r = (float)Math.Pow(diffs[i].AU06_r - means.AU06_r, 2f);
            newFrame.AU07_r = (float)Math.Pow(diffs[i].AU07_r - means.AU07_r, 2f);
            newFrame.AU09_r = (float)Math.Pow(diffs[i].AU09_r - means.AU09_r, 2f);
            newFrame.AU10_r = (float)Math.Pow(diffs[i].AU10_r - means.AU10_r, 2f);
            newFrame.AU12_r = (float)Math.Pow(diffs[i].AU12_r - means.AU12_r, 2f);
            newFrame.AU14_r = (float)Math.Pow(diffs[i].AU14_r - means.AU14_r, 2f);
            newFrame.AU15_r = (float)Math.Pow(diffs[i].AU15_r - means.AU15_r, 2f);
            newFrame.AU17_r = (float)Math.Pow(diffs[i].AU17_r - means.AU17_r, 2f);
            newFrame.AU20_r = (float)Math.Pow(diffs[i].AU20_r - means.AU20_r, 2f);
            newFrame.AU23_r = (float)Math.Pow(diffs[i].AU23_r - means.AU23_r, 2f);
            newFrame.AU25_r = (float)Math.Pow(diffs[i].AU25_r - means.AU25_r, 2f);
            newFrame.AU26_r = (float)Math.Pow(diffs[i].AU26_r - means.AU26_r, 2f);
            newFrame.AU45_r = (float)Math.Pow(diffs[i].AU45_r - means.AU45_r, 2f);

            newFrame.AU01_c = false;
            newFrame.AU02_c = false;
            newFrame.AU04_c = false;
            newFrame.AU05_c = false;
            newFrame.AU06_c = false;
            newFrame.AU07_c = false;
            newFrame.AU09_c = false;
            newFrame.AU10_c = false;
            newFrame.AU12_c = false;
            newFrame.AU14_c = false;
            newFrame.AU15_c = false;
            newFrame.AU17_c = false;
            newFrame.AU20_c = false;
            newFrame.AU23_c = false;
            newFrame.AU25_c = false;
            newFrame.AU26_c = false;
            newFrame.AU28_c = false;
            newFrame.AU45_c = false;

            deviations.Add(newFrame);
        }

        // Reinitializing summs
        {
            gaze_angle_x_sum = 0;
            gaze_angle_y_sum = 0;

            pose_Rx_sum = 0;
            pose_Ry_sum = 0;
            pose_Rz_sum = 0;

            AU01_r_sum = 0;
            AU02_r_sum = 0;
            AU04_r_sum = 0;
            AU05_r_sum = 0;
            AU06_r_sum = 0;
            AU07_r_sum = 0;
            AU09_r_sum = 0;
            AU10_r_sum = 0;
            AU12_r_sum = 0;
            AU14_r_sum = 0;
            AU15_r_sum = 0;
            AU17_r_sum = 0;
            AU20_r_sum = 0;
            AU23_r_sum = 0;
            AU25_r_sum = 0;
            AU26_r_sum = 0;
            AU45_r_sum = 0;
        }

        // Calculating variance
        foreach (Frame deviation in deviations)
        {
            gaze_angle_x_sum += deviation.gaze_angle_x;
            gaze_angle_y_sum += deviation.gaze_angle_y;

            pose_Rx_sum += deviation.pose_Rx;
            pose_Ry_sum += deviation.pose_Ry;
            pose_Rz_sum += deviation.pose_Rz;

            AU01_r_sum += deviation.AU01_r;
            AU02_r_sum += deviation.AU02_r;
            AU04_r_sum += deviation.AU04_r;
            AU05_r_sum += deviation.AU05_r;
            AU06_r_sum += deviation.AU06_r;
            AU07_r_sum += deviation.AU07_r;
            AU09_r_sum += deviation.AU09_r;
            AU10_r_sum += deviation.AU10_r;
            AU12_r_sum += deviation.AU12_r;
            AU14_r_sum += deviation.AU14_r;
            AU15_r_sum += deviation.AU15_r;
            AU17_r_sum += deviation.AU17_r;
            AU20_r_sum += deviation.AU20_r;
            AU23_r_sum += deviation.AU23_r;
            AU25_r_sum += deviation.AU25_r;
            AU26_r_sum += deviation.AU26_r;
            AU45_r_sum += deviation.AU45_r;
        }

        // Calculating standart deviation
        {
            stdDeviations.frame = 0;
            stdDeviations.face_id = 0;
            stdDeviations.timestamp = 0f;
            stdDeviations.confidence = 0.97f;
            stdDeviations.success = true;

            stdDeviations.gaze_angle_x = (float)Math.Sqrt(gaze_angle_x_sum / deviations.Count);
            stdDeviations.gaze_angle_y = (float)Math.Sqrt(gaze_angle_y_sum / deviations.Count);

            stdDeviations.pose_Rx = (float)Math.Sqrt(pose_Rx_sum / deviations.Count);
            stdDeviations.pose_Ry = (float)Math.Sqrt(pose_Ry_sum / deviations.Count);
            stdDeviations.pose_Rz = (float)Math.Sqrt(pose_Rz_sum / deviations.Count);

            stdDeviations.AU01_r = (float)Math.Sqrt(AU01_r_sum / deviations.Count);
            stdDeviations.AU02_r = (float)Math.Sqrt(AU02_r_sum / deviations.Count);
            stdDeviations.AU04_r = (float)Math.Sqrt(AU04_r_sum / deviations.Count);
            stdDeviations.AU05_r = (float)Math.Sqrt(AU05_r_sum / deviations.Count);
            stdDeviations.AU06_r = (float)Math.Sqrt(AU06_r_sum / deviations.Count);
            stdDeviations.AU07_r = (float)Math.Sqrt(AU07_r_sum / deviations.Count);
            stdDeviations.AU09_r = (float)Math.Sqrt(AU09_r_sum / deviations.Count);
            stdDeviations.AU10_r = (float)Math.Sqrt(AU10_r_sum / deviations.Count);
            stdDeviations.AU12_r = (float)Math.Sqrt(AU12_r_sum / deviations.Count);
            stdDeviations.AU14_r = (float)Math.Sqrt(AU14_r_sum / deviations.Count);
            stdDeviations.AU15_r = (float)Math.Sqrt(AU15_r_sum / deviations.Count);
            stdDeviations.AU17_r = (float)Math.Sqrt(AU17_r_sum / deviations.Count);
            stdDeviations.AU20_r = (float)Math.Sqrt(AU20_r_sum / deviations.Count);
            stdDeviations.AU23_r = (float)Math.Sqrt(AU23_r_sum / deviations.Count);
            stdDeviations.AU25_r = (float)Math.Sqrt(AU25_r_sum / deviations.Count);
            stdDeviations.AU26_r = (float)Math.Sqrt(AU26_r_sum / deviations.Count);
            stdDeviations.AU45_r = (float)Math.Sqrt(AU45_r_sum / deviations.Count);

            stdDeviations.AU01_c = false;
            stdDeviations.AU02_c = false;
            stdDeviations.AU04_c = false;
            stdDeviations.AU05_c = false;
            stdDeviations.AU06_c = false;
            stdDeviations.AU07_c = false;
            stdDeviations.AU09_c = false;
            stdDeviations.AU10_c = false;
            stdDeviations.AU12_c = false;
            stdDeviations.AU14_c = false;
            stdDeviations.AU15_c = false;
            stdDeviations.AU17_c = false;
            stdDeviations.AU20_c = false;
            stdDeviations.AU23_c = false;
            stdDeviations.AU25_c = false;
            stdDeviations.AU26_c = false;
            stdDeviations.AU28_c = false;
            stdDeviations.AU45_c = false;
        }

        
    }

    public void CorrectFrames()
    {
        double stdDevThreshhold = 2.5;

        //Repacking the frame with reduced noise
        for (int i = 0 + 1; i <= firstFrameIndex; i++)
            corFrames.Add(ogFrames[0]);

        for (int i = firstFrameIndex + 1; i < ogFrames.Count; i++)
        {
            //Debug.Log("corFrames length: " + corFrames.Count + " | ogFrames length: " + ogFrames.Count + " | index: " + i);
            Frame newFrame = new Frame();

            newFrame.frame = i;
            newFrame.face_id = ogFrames[i].face_id;
            newFrame.timestamp = ogFrames[i].timestamp;
            newFrame.confidence = ogFrames[i].confidence;
            newFrame.success = ogFrames[i].success;

            if (stdDeviations.gaze_angle_x < stdDevThreshhold)
                newFrame.gaze_angle_x = ogFrames[i].gaze_angle_x;
            else
                newFrame.gaze_angle_x = (ogFrames[i - 1].gaze_angle_x + ogFrames[i].gaze_angle_x) / 2;


            if (stdDeviations.gaze_angle_y < stdDevThreshhold)
                newFrame.gaze_angle_y = ogFrames[i].gaze_angle_y;
            else
                newFrame.gaze_angle_y = (ogFrames[i - 1].gaze_angle_y + ogFrames[i].gaze_angle_y) / 2;


            if (stdDeviations.pose_Rx < stdDevThreshhold)
                newFrame.pose_Rx = ogFrames[i].pose_Rx;
            else
                newFrame.pose_Rx = (ogFrames[i - 1].pose_Rx + ogFrames[i].pose_Rx) / 2;


            if (stdDeviations.pose_Ry < stdDevThreshhold)
                newFrame.pose_Ry = ogFrames[i].pose_Ry;
            else
                newFrame.pose_Ry = (ogFrames[i - 1].pose_Ry + ogFrames[i].pose_Ry) / 2;


            if (stdDeviations.pose_Rz < stdDevThreshhold)
                newFrame.pose_Rz = ogFrames[i].pose_Rz;
            else
                newFrame.pose_Rz = (ogFrames[i - 1].pose_Rz + ogFrames[i].pose_Rz) / 2;


            if (stdDeviations.AU01_r < stdDevThreshhold)
                newFrame.AU01_r = ogFrames[i].AU01_r;
            else
                newFrame.AU01_r = (ogFrames[i - 1].AU01_r + ogFrames[i].AU01_r) / 2;


            if (stdDeviations.AU02_r < stdDevThreshhold)
                newFrame.AU02_r = ogFrames[i].AU02_r;
            else
                newFrame.AU02_r = (ogFrames[i - 1].AU02_r + ogFrames[i].AU02_r) / 2;


            if (stdDeviations.AU04_r < stdDevThreshhold)
                newFrame.AU04_r = ogFrames[i].AU04_r;
            else
                newFrame.AU04_r = (ogFrames[i - 1].AU04_r + ogFrames[i].AU04_r) / 2;


            if (stdDeviations.AU05_r < stdDevThreshhold)
                newFrame.AU05_r = ogFrames[i].AU05_r;
            else
                newFrame.AU05_r = (ogFrames[i - 1].AU05_r + ogFrames[i].AU05_r) / 2;


            if (stdDeviations.AU06_r < stdDevThreshhold)
                newFrame.AU06_r = ogFrames[i].AU06_r;
            else
                newFrame.AU06_r = (ogFrames[i - 1].AU06_r + ogFrames[i].AU06_r) / 2;


            if (stdDeviations.AU07_r < stdDevThreshhold)
                newFrame.AU07_r = ogFrames[i].AU07_r;
            else
                newFrame.AU07_r = (ogFrames[i - 1].AU07_r + ogFrames[i].AU07_r) / 2;


            if (stdDeviations.AU09_r < stdDevThreshhold)
                newFrame.AU09_r = ogFrames[i].AU09_r;
            else
                newFrame.AU09_r = (ogFrames[i - 1].AU09_r + ogFrames[i].AU09_r) / 2;


            if (stdDeviations.AU10_r < stdDevThreshhold)
                newFrame.AU10_r = ogFrames[i].AU10_r;
            else
                newFrame.AU10_r = (ogFrames[i - 1].AU10_r + ogFrames[i].AU10_r) / 2;


            if (stdDeviations.AU12_r < stdDevThreshhold)
                newFrame.AU12_r = ogFrames[i].AU12_r;
            else
                newFrame.AU12_r = (ogFrames[i - 1].AU12_r + ogFrames[i].AU12_r) / 2;


            if (stdDeviations.AU14_r < stdDevThreshhold)
                newFrame.AU14_r = ogFrames[i].AU14_r;
            else
                newFrame.AU14_r = (ogFrames[i - 1].AU14_r + ogFrames[i].AU14_r) / 2;


            if (stdDeviations.AU15_r < stdDevThreshhold)
                newFrame.AU15_r = ogFrames[i].AU15_r;
            else
                newFrame.AU15_r = (ogFrames[i - 1].AU15_r + ogFrames[i].AU15_r) / 2;


            if (stdDeviations.AU17_r < stdDevThreshhold)
                newFrame.AU17_r = ogFrames[i].AU17_r;
            else
                newFrame.AU17_r = (ogFrames[i - 1].AU17_r + ogFrames[i].AU17_r) / 2;


            if (stdDeviations.AU20_r < stdDevThreshhold)
                newFrame.AU20_r = ogFrames[i].AU20_r;
            else
                newFrame.AU20_r = (ogFrames[i - 1].AU20_r + ogFrames[i].AU20_r) / 2;


            if (stdDeviations.AU23_r < stdDevThreshhold)
                newFrame.AU23_r = ogFrames[i].AU23_r;
            else
                newFrame.AU23_r = (ogFrames[i - 1].AU23_r + ogFrames[i].AU23_r) / 2;


            if (stdDeviations.AU25_r < stdDevThreshhold)
                newFrame.AU25_r = ogFrames[i].AU25_r;
            else
                newFrame.AU25_r = (ogFrames[i - 1].AU25_r + ogFrames[i].AU25_r) / 2;


            if (stdDeviations.AU26_r < stdDevThreshhold)
                newFrame.AU26_r = ogFrames[i].AU26_r;
            else
                newFrame.AU26_r = (ogFrames[i - 1].AU26_r + ogFrames[i].AU26_r) / 2;


            if (stdDeviations.AU45_r < stdDevThreshhold)
                newFrame.AU45_r = ogFrames[i].AU45_r;
            else
                newFrame.AU45_r = (ogFrames[i - 1].AU45_r + ogFrames[i].AU45_r) / 2;


            newFrame.AU01_c = ogFrames[i].AU01_c;
            newFrame.AU02_c = ogFrames[i].AU02_c;
            newFrame.AU04_c = ogFrames[i].AU04_c;
            newFrame.AU05_c = ogFrames[i].AU05_c;
            newFrame.AU06_c = ogFrames[i].AU06_c;
            newFrame.AU07_c = ogFrames[i].AU07_c;
            newFrame.AU09_c = ogFrames[i].AU09_c;
            newFrame.AU10_c = ogFrames[i].AU10_c;
            newFrame.AU12_c = ogFrames[i].AU12_c;
            newFrame.AU14_c = ogFrames[i].AU14_c;
            newFrame.AU15_c = ogFrames[i].AU15_c;
            newFrame.AU17_c = ogFrames[i].AU17_c;
            newFrame.AU20_c = ogFrames[i].AU20_c;
            newFrame.AU23_c = ogFrames[i].AU23_c;
            newFrame.AU25_c = ogFrames[i].AU25_c;
            newFrame.AU26_c = ogFrames[i].AU26_c;
            newFrame.AU28_c = ogFrames[i].AU28_c;
            newFrame.AU45_c = ogFrames[i].AU45_c;

            corFrames.Add(newFrame);
        }
    }

    public void PrepareFExpression(float time, int index)
    {
        Frame curFrame;
        Frame nextFrame;
        if (index < corFrames.Count) {
            //Debug.Log(index + " | " + corFrames.Count);

            // determining if there are frames to follow. If not, the current frame taken
            curFrame = corFrames[index];
            if (index < corFrames.Count - 1)
                nextFrame = corFrames[index + 1];
            else
                nextFrame = curFrame;

            if (curFrame.timestamp != nextFrame.timestamp) {
                // calculating the time passed on the current frame
                frameTime = (time - curFrame.timestamp) / (nextFrame.timestamp - curFrame.timestamp);
            }
            else
            {
                frameTime = 1;
            }


            // setting expression between the current frame and the one that follows

            SetFExpression(curFrame, nextFrame, frameTime);
        }
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
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU01_c ? frame.AU01_r : 0f, nextFrame.AU01_c ? nextFrame.AU01_r : 0f, frameTime) * param.AU01_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU01_c ? frame.AU01_r : 0f * param.AU01_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Inner_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU01_c ? frame.AU01_r : 0f, nextFrame.AU01_c ? nextFrame.AU01_r : 0f, frameTime) * param.AU01_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU01_c ? frame.AU01_r : 0f * param.AU01_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Outer_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU02_c ? frame.AU02_r : 0f, nextFrame.AU02_c ? nextFrame.AU02_r : 0f, frameTime) * param.AU02_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU02_c ? frame.AU02_r : 0f * param.AU02_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Raise_Outer_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU02_c ? frame.AU02_r : 0f, nextFrame.AU02_c ? nextFrame.AU02_r : 0f, frameTime) * param.AU02_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU02_c ? frame.AU02_r : 0f * param.AU02_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Drop_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU04_c ? frame.AU04_r : 0f, nextFrame.AU04_c ? nextFrame.AU04_r : 0f, frameTime) * param.AU04_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU04_c ? frame.AU04_r : 0f * param.AU04_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Brow_Drop_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU04_c ? frame.AU04_r : 0f, nextFrame.AU04_c ? nextFrame.AU04_r : 0f, frameTime) * param.AU04_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU04_c ? frame.AU04_r : 0f * param.AU04_r_coef);

                }
            }


            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Wide_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU05_c ? frame.AU05_r : 0f, nextFrame.AU05_c ? nextFrame.AU05_r : 0f, frameTime) * param.AU05_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU05_c ? frame.AU05_r : 0f * param.AU05_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Wide_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU05_c ? frame.AU05_r : 0f, nextFrame.AU05_c ? nextFrame.AU05_r : 0f, frameTime) * param.AU05_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU05_c ? frame.AU05_r : 0f * param.AU05_r_coef);

                }
            }





            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Cheek_Raise_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU06_c ? frame.AU06_r : 0f, nextFrame.AU06_c ? nextFrame.AU06_r : 0f, frameTime) * param.AU06_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU06_c ? frame.AU06_r : 0f * param.AU06_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Cheek_Raise_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU06_c ? frame.AU06_r : 0f, nextFrame.AU06_c ? nextFrame.AU06_r : 0f, frameTime) * param.AU06_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU06_c ? frame.AU06_r : 0f * param.AU06_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Squint_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU07_c ? frame.AU07_r : 0f, nextFrame.AU07_c ? nextFrame.AU07_r : 0f, frameTime) * param.AU07_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU07_c ? frame.AU07_r : 0f * param.AU07_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Squint_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU07_c ? frame.AU07_r : 0f, nextFrame.AU07_c ? nextFrame.AU07_r : 0f, frameTime) * param.AU07_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU07_c ? frame.AU07_r : 0f * param.AU07_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Nose_Sneer_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU09_c ? frame.AU09_r : 0f, nextFrame.AU09_c ? nextFrame.AU09_r : 0f, frameTime) * param.AU09_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU09_c ? frame.AU09_r : 0f * param.AU09_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Nose_Sneer_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU09_c ? frame.AU09_r : 0f, nextFrame.AU09_c ? nextFrame.AU09_r : 0f, frameTime) * param.AU09_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU09_c ? frame.AU09_r : 0f * param.AU09_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Up_Upper_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU10_c ? frame.AU10_r : 0f, nextFrame.AU10_c ? nextFrame.AU10_r : 0f, frameTime) * param.AU10_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU10_c ? frame.AU10_r : 0f * param.AU10_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Up_Upper_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU10_c ? frame.AU10_r : 0f, nextFrame.AU10_c ? nextFrame.AU10_r : 0f, frameTime) * param.AU10_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU10_c ? frame.AU10_r : 0f * param.AU10_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Smile_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU12_c ? frame.AU12_r : 0f, nextFrame.AU12_c ? nextFrame.AU12_r : 0f, frameTime) * param.AU12_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU12_c ? frame.AU12_r : 0f * param.AU12_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Smile_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU12_c ? frame.AU12_r : 0f, nextFrame.AU12_c ? nextFrame.AU12_r : 0f, frameTime) * param.AU12_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU12_c ? frame.AU12_r : 0f * param.AU12_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Dimple_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU14_c ? frame.AU14_r : 0f, nextFrame.AU14_c ? nextFrame.AU14_r : 0f, frameTime) * param.AU14_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU14_c ? frame.AU14_r : 0f * param.AU14_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Dimple_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU14_c ? frame.AU14_r : 0f, nextFrame.AU14_c ? nextFrame.AU14_r : 0f, frameTime) * param.AU14_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU14_c ? frame.AU14_r : 0f * param.AU14_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Frown_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU15_c ? frame.AU15_r : 0f, nextFrame.AU15_c ? nextFrame.AU15_r : 0f, frameTime) * param.AU15_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU15_c ? frame.AU15_r : 0f * param.AU15_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Frown_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU15_c ? frame.AU15_r : 0f, nextFrame.AU15_c ? nextFrame.AU15_r : 0f, frameTime) * param.AU15_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU15_c ? frame.AU15_r : 0f * param.AU15_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Chin_Up");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU17_c ? frame.AU17_r : 0f, nextFrame.AU17_c ? nextFrame.AU17_r : 0f, frameTime) * param.AU17_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU17_c ? frame.AU17_r : 0f * param.AU17_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Stretch_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU20_c ? frame.AU20_r : 0f, nextFrame.AU20_c ? nextFrame.AU20_r : 0f, frameTime) * param.AU20_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU20_c ? frame.AU20_r : 0f * param.AU20_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Stretch_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU20_c ? frame.AU20_r : 0f, nextFrame.AU20_c ? nextFrame.AU20_r : 0f, frameTime) * param.AU20_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU20_c ? frame.AU20_r : 0f * param.AU20_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Tighten_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU23_c ? frame.AU23_r : 0f, nextFrame.AU23_c ? nextFrame.AU23_r : 0f, frameTime) * param.AU23_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU23_c ? frame.AU23_r : 0f * param.AU23_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Tighten_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU23_c ? frame.AU23_r : 0f, nextFrame.AU23_c ? nextFrame.AU23_r : 0f, frameTime) * param.AU23_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU23_c ? frame.AU23_r : 0f * param.AU23_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("V_Lip_Open");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU25_c ? frame.AU25_r : 0f, nextFrame.AU25_c ? nextFrame.AU25_r : 0f, frameTime) * param.AU25_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU25_c ? frame.AU25_r : 0f * param.AU25_r_coef);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU28_c ? param.AU28_max : 0f);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU28_c ? param.AU28_max : 0f);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU28_c ? param.AU28_max : 0f);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Mouth_Roll_In_Upper_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU28_c ? param.AU28_max : 0f, nextFrame.AU28_c ? param.AU28_max : 0f, frameTime));
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU28_c ? param.AU28_max : 0f);

                }
            }




            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Blink_L");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU45_c ? frame.AU45_r : 0f, nextFrame.AU45_c ? nextFrame.AU45_r : 0f, frameTime) * param.AU45_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU45_c ? frame.AU45_r : 0f * param.AU45_r_coef);

                }
            }

            bsIndex = renderer.sharedMesh.GetBlendShapeIndex("Eye_Blink_R");

            if (bsIndex != -1)
            {
                if (AppManager.Instance.frameInterpol)
                {
                    renderer.SetBlendShapeWeight(bsIndex, Mathf.Lerp(frame.AU45_c ? frame.AU45_r : 0f, nextFrame.AU45_c ? nextFrame.AU45_r : 0f, frameTime) * param.AU45_r_coef);
                }
                else
                {
                    renderer.SetBlendShapeWeight(bsIndex, frame.AU45_c ? frame.AU45_r : 0f * param.AU45_r_coef);

                }
            }



        }


        // Head mouvement
        vectorTemp = headBone.transform.rotation.eulerAngles;

        float x = (Mathf.Lerp(frame.pose_Rx * 57.2958f, nextFrame.pose_Rx * 57.2958f, frameTime) - corFrames[firstFrameIndex].pose_Rx * 57.2958f) * param.pose_Rx_coef;
        float y = 180 + (Mathf.Lerp(frame.pose_Ry * 57.2958f, nextFrame.pose_Ry * 57.2958f, frameTime) - corFrames[firstFrameIndex].pose_Ry * 57.2958f) * param.pose_Ry_coef;
        float z = (Mathf.Lerp(frame.pose_Rz * 57.2958f, nextFrame.pose_Rz * 57.2958f, frameTime) - corFrames[firstFrameIndex].pose_Rz * 57.2958f) * param.pose_Rz_coef;

        

        vectorTemp.x = x;
        vectorTemp.y = y;
        vectorTemp.z = z;
        headBone.transform.rotation = Quaternion.Euler(vectorTemp);

        // Jaw mouvement. In conflict with Salsa 
        if (!AppManager.Instance.lipSynched)
        { 
            vectorTemp = jawBone.transform.localRotation.eulerAngles;

            jawBone.transform.localRotation = Quaternion.identity;

            vectorTemp.z = -90;
            vectorTemp.z += Mathf.Lerp(frame.AU25_c ? frame.AU25_r : 0f, nextFrame.AU25_c ? nextFrame.AU25_r : 0f, frameTime) * param.AU25_jaw_coef;
            vectorTemp.z += Mathf.Lerp(frame.AU26_c ? frame.AU26_r : 0f, nextFrame.AU26_c ? nextFrame.AU26_r : 0f, frameTime) * param.AU26_jaw_coef;

            jawBone.transform.localRotation = Quaternion.Euler(vectorTemp);
        }

        //eyeLeftBone.transform.LookAt(GameObject.Find("Main Camera").transform);
        //eyeLeftBone.transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);

        //eyeRightBone.transform.LookAt(GameObject.Find("Main Camera").transform);
        //eyeRightBone.transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);

        // Eyes mouvement. In conflict with Salsa 

        vectorTemp = eyeLeftBone.transform.rotation.eulerAngles;

        eyeLeftBone.transform.rotation = Quaternion.identity;

        vectorTemp.z = 90 + (Mathf.Lerp(frame.gaze_angle_x * 57.2958f, nextFrame.gaze_angle_x * 57.2958f, frameTime) - corFrames[firstFrameIndex].gaze_angle_x * 57.2958f);
        vectorTemp.y = 90 + (Mathf.Lerp(frame.gaze_angle_y * 57.2958f, nextFrame.gaze_angle_y * 57.2958f, frameTime) - corFrames[firstFrameIndex].gaze_angle_y * 57.2958f);

        eyeLeftBone.transform.rotation = Quaternion.Euler(vectorTemp);
        

        vectorTemp = eyeRightBone.transform.localRotation.eulerAngles;

        eyeRightBone.transform.localRotation = Quaternion.identity;

        vectorTemp.z = 180+(Mathf.Lerp(frame.gaze_angle_x * 57.2958f, nextFrame.gaze_angle_x * 57.2958f, frameTime) - corFrames[firstFrameIndex].gaze_angle_x * 57.2958f);
        vectorTemp.y = (Mathf.Lerp(frame.gaze_angle_y * 57.2958f, nextFrame.gaze_angle_y * 57.2958f, frameTime) - corFrames[firstFrameIndex].gaze_angle_y * 57.2958f);

        eyeRightBone.transform.localRotation = Quaternion.Euler(vectorTemp);

    }

    private void Start()
    {

        SetFeedback();
        SetAvatarProps();

    }
}
