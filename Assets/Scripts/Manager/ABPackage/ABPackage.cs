using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ABPkg
{
    public class ABPackage : MonobehaviourSingleton<ABPackage>
    {
        public Slider slider;
        public List<string> ablist = new();
        public List<AssetBundle> abPkg = new();
        public List<string> exclude = new();
        private List<int> excludeeI = new();
        public string version ;
        private Dictionary<int, string> tips = new();
        public string dataPath = Application.streamingAssetsPath;

        IEnumerator ReadList()
        {
            StreamReader st = new StreamReader(Application.streamingAssetsPath + "/ablist.txt");
            string[] a = st.ReadToEnd().Split("\n");
            st.Close();

            yield return new WaitForSeconds(0.1f);

            int iCount = 0;//计数，虽然可以用for的（（（）

            //load LoadAB config(ablist.txt)
            foreach (string i in a)
            {
                string[] temp = i.Split(" ");

                Debug.Log(temp[0]);

                ablist.Add(temp[0]);

                if (temp.Length == 3)
                {
                    if (temp[1] == "N")
                        exclude.Add(temp[0]);
                    tips[iCount] = temp[2];
                }
                else
                {
                    if (temp[1] != "N")
                        tips[iCount] = temp[1];
                    else
                        exclude.Add(temp[0]);
                }


                iCount++;
            }

            StartCoroutine("LoadAB");
        }
        IEnumerator LoadAB()
        {

            int iCount = 0; //in foreach

            foreach (string _index in ablist)
            {

                if (File.Exists(Application.streamingAssetsPath + "/AB/" + version + "/win/" + _index))
                {//AB存在就加载

                    AssetBundle ab;
                    int _iN = 0;

                    if (tips.ContainsKey(iCount))
                        Debug.Log(tips[iCount]);


                    yield return ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AB/" + version + "/win/" + _index);

                    if (abPkg.Contains(ab))
                    {
                        _iN = abPkg.IndexOf(ab);
                        abPkg[_iN] = ab;
                    }
                    else
                    {
                        abPkg.Add(ab);
                        _iN = abPkg.IndexOf(ab);
                    }

                    if (exclude.Contains(_index)) excludeeI.Add(_iN);
                }
                else
                {
                    Debug.Log("Error:Not Found Pivotal AB");
                }

                iCount++;
            }

            for (int i = 0; i < abPkg.Count; i++)
            {

                if (excludeeI.Contains(i)) goto b;

                abPkg[i].LoadAllAssets();
            b:
                slider.value = (i + 1) / abPkg.Count;
            }

            SceneManager.LoadScene("main");
        }
    }

}