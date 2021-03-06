﻿using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NetherWars.Parsing
{
    class Interpertor
    {
        private Tokenizer _tokenizer;

        private Processor _processor;

        private const string GRAMMER_FILE_PATH = "Assets/Resources/grammer.txt";

        public Interpertor()
        {
            _tokenizer = new Tokenizer();
            _processor = new Processor();

            LoadGrammer(GRAMMER_FILE_PATH);
        }

        public void Execute(string inputString)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            _tokenizer.InputString = inputString;

            _processor.Run(inputString, _tokenizer);

            UnityEngine.Debug.Log("Finished executing in " + stopWatch.ElapsedMilliseconds + "ms");

        }

        private bool LoadGrammer(string fileName)
        {
            try
            {
                string line;
                StreamReader reader = new StreamReader(fileName, Encoding.Default);
                using (reader)
                {
                    do
                    {
                        line = reader.ReadLine();

                        if (line != null)
                        {
                            if ((line.Trim().Equals(string.Empty)) || (line.Trim().StartsWith("#")))
                                continue;
                            string regex = string.Empty;
                            char prevChar = '\0';
                            int quoteNum = 0;

                            int ndx = 0;

                            foreach (char c in line)
                            {
                                if (c == '\"')
                                {
                                    quoteNum++;
                                    if (quoteNum > 1 && prevChar != '\\')
                                    {
                                        regex += c;
                                        ndx++;
                                        break;
                                    }
                                }

                                regex += c;
                                ndx++;
                                prevChar = c;
                            }
                            string ident = line.Remove(0, ndx).Trim();

                            int ndxComment = ident.Length - 1;

                            while (ndxComment > 0)
                            {
                                if (ident[ndxComment] == '#')
                                    break;
                                ndxComment--;
                            }
                            if (ndxComment > 0)
                            {
                                ident = ident.Substring(0, ndxComment);
                                ident = ident.Trim();
                            }

                            regex = regex.Substring(1, regex.Length - 1);
                            regex = regex.Substring(0, regex.Length - 1);
                            regex = regex.Replace("\\\"", "\"");

                            if (regex.Equals(string.Empty))
                                continue;

                            string retval = _tokenizer.AddRegExToken(regex, ident);
                            if (retval != string.Empty)
                            {
                                UnityEngine.Debug.LogError("ERROR - " + retval);
                                return false;
                            }
                        }
                    }
                    while (line != null);
                    
                    reader.Close();
                    return true;
                }
            }

            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("{0}\n" +  e.Message);
                return false;
            }
        }
    }

}
