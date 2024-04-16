using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class CsvImporter 
{

    public static List<Frame> ParseCSV(TextAsset csv)
    {
        Transform camera = AppManager.Instance.GetCamera();

        List<Frame> frames = new List<Frame>();

        string csvData = csv.text;
        string[] lines = csvData.Split("\r\n");
        string[] values = new string[lines[0].Length];
        

        // Going though lines array ignoring the 1st line with keys
        for (int i = 1; i < lines.Length - 1; i++)
        {

            values = lines[i].Split(", ");

            Frame newFrame = new Frame();

            newFrame.frame = int.Parse(values[0]);
            newFrame.face_id = int.Parse(values[1]);
            newFrame.timestamp = float.Parse(values[2], CultureInfo.InvariantCulture);
            newFrame.confidence = float.Parse(values[3], CultureInfo.InvariantCulture);
            newFrame.success = values[4] == "1";

            //newFrame.gaze_0 =  camera.transform.position + new Vector3(float.Parse(values[5], CultureInfo.InvariantCulture), float.Parse(values[6], CultureInfo.InvariantCulture), float.Parse(values[7], CultureInfo.InvariantCulture));
            //newFrame.gaze_1 = camera.transform.position + new Vector3(float.Parse(values[8], CultureInfo.InvariantCulture), float.Parse(values[9], CultureInfo.InvariantCulture), float.Parse(values[10], CultureInfo.InvariantCulture));

            newFrame.gaze_angle_x = float.Parse(values[11], CultureInfo.InvariantCulture); 
            newFrame.gaze_angle_y = float.Parse(values[12], CultureInfo.InvariantCulture); ;

            newFrame.pose_Rx = float.Parse(values[296], CultureInfo.InvariantCulture);
            newFrame.pose_Ry = float.Parse(values[297], CultureInfo.InvariantCulture);
            newFrame.pose_Rz = float.Parse(values[298], CultureInfo.InvariantCulture);

            newFrame.AU01_r = float.Parse(values[299], CultureInfo.InvariantCulture);
            newFrame.AU02_r = float.Parse(values[300], CultureInfo.InvariantCulture);
            newFrame.AU04_r = float.Parse(values[301], CultureInfo.InvariantCulture);
            newFrame.AU05_r = float.Parse(values[302], CultureInfo.InvariantCulture);
            newFrame.AU06_r = float.Parse(values[303], CultureInfo.InvariantCulture);
            newFrame.AU07_r = float.Parse(values[304], CultureInfo.InvariantCulture);
            newFrame.AU09_r = float.Parse(values[305], CultureInfo.InvariantCulture);
            newFrame.AU10_r = float.Parse(values[306], CultureInfo.InvariantCulture);
            newFrame.AU12_r = float.Parse(values[307], CultureInfo.InvariantCulture);
            newFrame.AU14_r = float.Parse(values[308], CultureInfo.InvariantCulture);
            newFrame.AU15_r = float.Parse(values[309], CultureInfo.InvariantCulture);
            newFrame.AU17_r = float.Parse(values[310], CultureInfo.InvariantCulture);
            newFrame.AU20_r = float.Parse(values[311], CultureInfo.InvariantCulture);
            newFrame.AU23_r = float.Parse(values[312], CultureInfo.InvariantCulture);
            newFrame.AU25_r = float.Parse(values[313], CultureInfo.InvariantCulture);
            newFrame.AU26_r = float.Parse(values[314], CultureInfo.InvariantCulture);
            newFrame.AU45_r = float.Parse(values[315], CultureInfo.InvariantCulture);

            newFrame.AU01_c = values[316] == "1.00";
            newFrame.AU02_c = values[317] == "1.00";
            newFrame.AU04_c = values[318] == "1.00";
            newFrame.AU05_c = values[319] == "1.00";
            newFrame.AU06_c = values[320] == "1.00";
            newFrame.AU07_c = values[321] == "1.00";
            newFrame.AU09_c = values[322] == "1.00";
            newFrame.AU10_c = values[323] == "1.00";
            newFrame.AU12_c = values[324] == "1.00";
            newFrame.AU14_c = values[325] == "1.00";
            newFrame.AU15_c = values[326] == "1.00";
            newFrame.AU17_c = values[327] == "1.00";
            newFrame.AU20_c = values[328] == "1.00";
            newFrame.AU23_c = values[329] == "1.00";
            newFrame.AU25_c = values[330] == "1.00";
            newFrame.AU26_c = values[331] == "1.00";
            newFrame.AU28_c = values[332] == "1.00";
            newFrame.AU45_c = values[333] == "1.00";
                //Debug.Log(newFrame.frame.ToString() + " | " + values[333] + "| " + newFrame.AU45_c.ToString());
            frames.Add(newFrame);

        }

        return frames;
    }
}
