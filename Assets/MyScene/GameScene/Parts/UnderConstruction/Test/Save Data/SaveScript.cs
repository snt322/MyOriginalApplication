using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class SaveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveGameData("");

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void SaveGameData(string path)
    {
        string pathDesk = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        pathDesk += "\\Data.txt";

        System.IO.StreamWriter swriter = new System.IO.StreamWriter(pathDesk, false);
        swriter.WriteLine("Is Data written?");
        swriter.Close();


        pathDesk = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        pathDesk += "\\binary.txt";


        var writer = new System.IO.BinaryWriter(System.IO.File.Open(pathDesk, FileMode.Append));
        byte[] buffer = { (byte)'A', (byte)'B' };
        writer.Write(buffer);
        writer.Close();



    }

}