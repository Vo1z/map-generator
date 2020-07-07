// written by Voiz
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace config
{
    class Config
    {
        public float[] variables; //array that collects variables in float
        private string[] strVariables; //array that collects variables but in string (for runtime optimization)
        private int lengthOfArray; //variable that collects number of avaliable elements in config
        public string address; //path to config.txt
        private File fileReader;

        public Config(string address, int numberOfVariables) //constructor
        {
            this.address = address;
            this.fileReader = new File(address, numberOfVariables);
            this.lengthOfArray = fileReader.GetContent().Length;
            this.strVariables = fileReader.GetContent();
            this.variables = getConvertedVariables(); // number of variables that are stored in config
        }

        private float[] getConvertedVariables() //method that converts string varibales,
        {                                        //taken from class FileReader, to float
            float[] convertedVariables = new float[lengthOfArray];

            for (int i = 0; i < lengthOfArray; i++)
            {
                convertedVariables[i] = float.Parse(strVariables[i]);
            }

            return convertedVariables;
        }        
    }

    class File
    {
        private string address; //path to config.txt
        private int numberOfVariables;
        private string[] content;

        internal File(string address, int numberOfVariables) //constructor
        {
            this.address = address;
            this.numberOfVariables = numberOfVariables;
            this.content = replaceOdd();
        }

        private string readWords() //method that copies content of config.txt file
        {
            string text = ""; // string that collects copy of the text file
            StreamReader sr = new StreamReader(this.address);
            int charCode;

            while ((charCode = sr.Read()) != -1)
            {
                text += (char)charCode;
            }

            return text;
        }

        private string[] findCommonWords()//method that finds and takes variables from
        {                                            //confic which was converted in string with readWords()        

            Regex regex = new Regex("((= .?\\d+\\.\\d+)(?m))|((= .?\\d+)(?m))"); //THE MAIN REGULAR EXPRESSION
            MatchCollection matches = regex.Matches(readWords());
            string[] commonText = new string[this.numberOfVariables]; //NUMBER OF VARIABLES of elements in config
            int count = 0;

            foreach (Match match in matches)
            {
                if (match.Value == null)
                {
                    commonText[count] = "0";
                }
                commonText[count] = match.Value;
                count++;
            }

            return commonText;
        }

        internal string[] replaceOdd() // method that clears all odd symbols from variables 
        {                                        // that were found in findCommonWords() method

            string[] replacedOdd = findCommonWords(); //string which is to become with replaced odd symbols
            for (int i = 0; i < findCommonWords().Length; i++)
            {
                if (replacedOdd[i] == null)
                {
                    replacedOdd[i] = "0";
                }
                else
                {
                    replacedOdd[i] = replacedOdd[i].Remove(0, 2);
                }
            }

            return replacedOdd;
        }

        public string[] GetContent() //retuns content of the file
        {
            return this.content;
        }
    }
}