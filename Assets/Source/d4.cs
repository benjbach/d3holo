using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using DataLoader;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class d4: MonoBehaviour {

    public TextAsset myFile;

    public void Start()
    {
        // DataLoader.DataObject do_ = new DataLoader.DataObject();
        // Debug.Log(do_.loadCSV(myFile.text));
   
    }

    //void loadCSV(string data)
    //{
    //    string[] lines = data.Split('\n');

    //    //1: read types
    //    string[] identifiers = lines[0].Split(',');
    //    string[] typesToRead = lines[1].Split(',');
    //    string[] theTypes = new string[typesToRead.Length];
    //    for(int i=0; i<typesToRead.Length;i++)
    //    {
    //        if (isBool(typesToRead[i]))
    //            theTypes[i] = "bool";
    //        else if (isFloat(typesToRead[i]))
    //            theTypes[i] = "float";
    //        else theTypes[i] = "string";
    //    }

    //    string DataObject = createClass(identifiers, theTypes);
    //    //compile the class
    //    CodeDomProvider oCodeDomProvider = CodeDomProvider.CreateProvider("CSharp");
    //    // Add what referenced assemblies
    //    CompilerParameters oCompilerParameters = new CompilerParameters();
    //    oCompilerParameters.ReferencedAssemblies.Add("system.dll");
    //    // set the compiler to create a DLL
    //    oCompilerParameters.GenerateExecutable = false;
    //    // set the dll to be created in memory and not on the hard drive
    //    oCompilerParameters.GenerateInMemory = true;
    //    var oCompilerResults =
    //      oCodeDomProvider.CompileAssemblyFromSource(oCompilerParameters, DataObject);
    //    if (oCompilerResults.Errors.Count != 0)
    //        Debug.Log("There were errors while compiling the Data ");

    //    Debug.Log(DataObject);

    //}

    internal static Selection selectAll()
    {
        return new Selection();
    }

    public bool isBool(string value)
    {
        bool res = false;
        return bool.TryParse(value, out res);
    }

    public bool isFloat(string value)
    {
        float res = 0f;
        return float.TryParse(value, out res);
    }

    string createClass(string[] ids, string[] typeFields)
    {
        string classCompile =
            @"using System;
              
              class DataObject
              {
              "+"\n";
            
        string fields = "";
            for(int i=0; i<typeFields.Length;i++)
             {
                 fields+= "public static " + typeFields[i] + " " + ids[i]+";\n";
             }
        classCompile+= fields;
        classCompile+="}\n";

        return classCompile;
    }


    
    
    
    
    
    /////////////////
    // DATA IMPORT //
    /////////////////



    public static List<Dictionary<string,object>> loadCSV(string filePath){
        
        List<Dictionary<string,object>> data = CSVReader.Read (filePath);
 
        for(var i=0; i < data.Count; i++) {

            // print ("Country:  " + data[i]["Country"]);
            // foreach(KeyValuePair<string, object> field in data[i])
            // {
            //     //Now you can access the key and value both separately from this attachStat as:
            //     Debug.Log(field.Key);
            //     Debug.Log(field.Value);
            // }
        }
 
        return data;
    }


    
    public class CSVReader
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };
    
        public static List<Dictionary<string, object>> Read(string file)
        {
            var list = new List<Dictionary<string, object>>();

            TextAsset data = Resources.Load (file) as TextAsset;

            var lines = Regex.Split (data.text, LINE_SPLIT_RE);
    
            if(lines.Length <= 1) return list;
    
            var header = Regex.Split(lines[0], SPLIT_RE);
            for(var i=1; i < lines.Length; i++) {
    
                var values = Regex.Split(lines[i], SPLIT_RE);
                if(values.Length == 0 ||values[0] == "") continue;
    
                var entry = new Dictionary<string, object>();
                for(var j=0; j < header.Length && j < values.Length; j++ ) {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    object finalvalue = value;
                    int n;
                    float f;
                    if(int.TryParse(value, out n)) {
                        finalvalue = n;
                    } else if (float.TryParse(value, out f)) {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }
                list.Add (entry);
            }
            return list;
        }
    }
              
    

    // API WRAPPERS

    public static float lerp(float start, float end, float interpolation){
        return Mathf.Lerp (10, 100, Mathf.InverseLerp (1, 5, 3));
    }

    public static float map (float startDomain, float endDomain, float startRange, float endRange, float v) {
        // if (v <= startDomain)
        //     return startRange;
        // else if (v >= endDomain)
        //     return endRange;
        return (endRange - startRange) * ((v - startDomain) / (endDomain - startDomain)) + startRange;
    }

    public static float max(List<float> values)
    {
        float m = 3786597845f;
        foreach(float v in values)
        {
            if(m == 3786597845f)
            {
                m = v;
            }else{
                m = Mathf.Max(m,v);
            }
        }
        return m;
     }

    public static float min(List<float> values)
    {
        float m = 3786597845f;
        foreach(float v in values){
            if( m == 3786597845f)
            {
                m = v;
            }else{
                m = Mathf.Min(m,v);
            }
        }
        return m;
     }

 
    
}
