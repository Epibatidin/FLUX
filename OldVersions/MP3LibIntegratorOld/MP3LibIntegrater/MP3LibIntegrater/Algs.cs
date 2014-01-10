using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MP3LibIntegrater
{
    public static class Algs
    {       
        private static Dictionary<string, Regex> RegexDict = new Dictionary<string, Regex>();
        private static char[] splitBy = new char[] { ' ', '_', '-', ',', '.' };

        readonly public static HashSet<string> WhiteList = initWhiteList();

        public static string badMP3BasicPath = "G:\\TESTBENCH";
        public static string goodMP3BasicPath = "G:\\TARGET";

        public static bool DoIt = true;

        public static string extension = ".mp3";

        public static  HashSet<string> initWhiteList()
        {
            HashSet<string> _WhiteList = new HashSet<string>();

            _WhiteList.Add("the");
            _WhiteList.Add("a");
            _WhiteList.Add("an");
            _WhiteList.Add("to");
            _WhiteList.Add("in");
            _WhiteList.Add("of");

            return _WhiteList;
        }

        public static DirectoryInfo AddDirectory(DirectoryInfo currentDir, string FolderName)
        {
            if (currentDir == null)
            {
                currentDir = new DirectoryInfo(goodMP3BasicPath);
            }
            if (DoIt)
            {
                if (currentDir.Exists)
                {
                    return currentDir.CreateSubdirectory(FolderName);
                }
            }
            return null;
        }


        //static List<string> source = new List<string>();
        //static List<string> target = new List<string>();

        public static bool compareSourceAndTarget(DirectoryInfo[] source, DirectoryInfo[] target)
        {
            if(source == null) source = new DirectoryInfo(badMP3BasicPath).GetDirectories();
            if(target == null) source = new DirectoryInfo(goodMP3BasicPath).GetDirectories();



            if ((source.Count() != target.Count())) return false;
            int dirs = target.Count();

            for (int i = 0; i < dirs; i++)
            {
                if (source[i].GetDirectories().Count() > 0 &&
                    target[i].GetDirectories().Count() > 0)
                {
                    return compareSourceAndTarget(source[i].GetDirectories(), target[i].GetDirectories());
                }
                else
                {
                    return compareSourceAndTarget(source[i].GetFiles(), target[i].GetFiles());
                }


                if (source[i].Name != target[i].Name) 
                    return false;
            }
            return true;
        }

        private static bool compareSourceAndTarget(FileInfo[] source, FileInfo[] target)
        {
            if (source == null && target == null) return true;
            if (source.Count() != target.Count()) return false;
            int files = source.Count();




            return true;
        }


        public static void CopyFile(DirectoryInfo TargetDir , string FileName, string sourcePath)
        {
            if(DoIt)
                if (TargetDir.Exists)
                {
                    FileInfo fi = new FileInfo(sourcePath);
                    string targetPath = TargetDir.FullName + "\\" + FileName + extension;
                    if (!File.Exists(targetPath))
                        fi.CopyTo(targetPath);                    
                }
        }

        public static string joinString(string[] input)
        {
            string output = String.Empty;
            //if (input == null) return String.Empty;
            foreach (var s in input)
            {
                if (s != String.Empty)
                {
                    output += UppercaseFirstLetter(s) + " ";
                }
            }
            return output.Trim();
        }

        static private string UppercaseFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word)) return string.Empty;
            char[] a = word.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

     

        public static bool executeRegex(string regexName, string wordToCheck)
        {
            if (RegexDict.Keys.Count == 0) initRegex();

            if (RegexDict.ContainsKey(regexName))
            {
                return RegexDict[regexName].IsMatch(wordToCheck);
            }
            return false;
        }

        private static void initRegex()
        {
            RegexDict.Add("year", new Regex(@"([^0-9])*(19|20)[0-9][0-9]([^0-9])*"));
            RegexDict.Add("tracknum", new Regex("\\b([^0-9]?[0-4]?[0-9]?[0-9][^0-9]?)\\b"));
        }

        internal static string[] Split(string p)
        {
            return p.Split(Algs.splitBy, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
