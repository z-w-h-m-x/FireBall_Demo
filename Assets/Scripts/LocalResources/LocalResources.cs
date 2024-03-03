using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LocalResources
{
    public static class LocalResources
    {
        public static void Init()
        {
            //TextAsset configFile = AssetDatabase.LoadAssetAtPath<TextAsset>(LocalResourcesConfig.listPath);//it is not work

            //if (configFile == null) return;


        }
    }

    public static class LocalResourcesConfig
    {
        public static string listPath = "Assets/Config/LocalResourcesConfig.txt";//Asset Path(it is not work in android platform)

        public static Dictionary<string, string> filePath = new();

        public static void Analyze(string configFileContent)
        {
            string[] tmp = configFileContent.Split("\n");

            foreach (string t in tmp)
            {
                if (t == "") return;

                string[] tm = t.Split("==");

                filePath.Add(tm[0],tm[1]);
            }
        }
    }

}
