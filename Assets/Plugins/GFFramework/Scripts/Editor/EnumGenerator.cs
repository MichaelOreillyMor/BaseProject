using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace GFF.Editor
{
    public class EnumGenerator
    {
        public void CreateEnum(string enumTitle, string namespaceTitle, List<string> enumNames)
        {
            string path = Application.dataPath + "/Plugins/GFFramework/Scripts/Generated/" + enumTitle + ".cs";
            Debug.Log(path);

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            else
            {
                File.WriteAllText(path, "");
            }

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("namespace " + namespaceTitle);
            writer.WriteLine("{");
            writer.WriteLine("\t public enum " + enumTitle);
            writer.WriteLine("\t {");

            foreach (string enumName in enumNames)
            {
                writer.WriteLine("\t \t " + enumName + ",");
            }

            writer.WriteLine("\t }");
            writer.WriteLine("}");
            writer.Close();

            AssetDatabase.Refresh();
        }
    }
}