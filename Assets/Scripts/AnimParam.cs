using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "AmimParam", order = 1)]
public class AnimParam : ScriptableObject 
{
    [Header("Bone Angle Coefficients")]
    public float pose_Rx_coef = 0.9f;
    public float pose_Ry_coef = 0.9f;
    public float pose_Rz_coef = 0.9f;
    public float AU25_jaw_coef = -1;
    public float AU26_jaw_coef = -4;
    [Header("Action Unit Coefficients")]
    public float AU01_r_coef = 100 / 5;
    public float AU02_r_coef = 100 / 5;
    public float AU04_r_coef = 100 / 5;
    public float AU05_r_coef = 100 / 5;
    public float AU06_r_coef = 100 / 5;
    public float AU07_r_coef = 100 / 5;
    public float AU09_r_coef = 100 / 5;
    public float AU10_r_coef = 50 / 5;
    public float AU12_r_coef = 100 / 5;
    public float AU14_r_coef = 100 / 5;
    public float AU15_r_coef = 100 / 5;
    public float AU17_r_coef = 100 / 5;
    public float AU20_r_coef = 100 / 5;
    public float AU23_r_coef = 100 / 5;
    public float AU25_r_coef = 100 / 5;
    public float AU28_max = 30;
    public float AU45_r_coef = 100 / 5;
}
