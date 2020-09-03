using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace AzzamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string sameliststring = string.Empty;
            List<string> matching = new List<string>();

            //Get inputjson path from project folder
            //Acceptance Criteria:The json can be loaded externally.
            string jsonFilePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"AzzamTest\inputjson.json");

            using (StreamReader r = new StreamReader(jsonFilePath))
            {
                //Read inputjson file
                string json = r.ReadToEnd();

                //Deserialize json to object
                Root items = DeserializeObject<Root>(json);

               //Loop for each name
                foreach(var item in items.recipients)
                {
                    //Loop to compare with each name
                    foreach(var item2 in items.recipients)
                    {
                        //Requirement: produces a list of each pair of names which have 2 or more tags in common
                        matching = item.tags.Intersect(item2.tags).ToList();
                        if (matching.Count() >= 2 && item.name != item2.name)
                            //Acceptance Criteria: Each pair of names should only appear once in the list, the order does not matter
                            if (!sameliststring.Contains(item2.name + "|" + item.name))
                                sameliststring = string.IsNullOrEmpty(sameliststring) ? item.name + "|" + item2.name : sameliststring + "\n" + item.name + "|" + item2.name ;
                    }
                }
            }
            //Acceptance Criteria: The output should be printed to the console
            Console.WriteLine(sameliststring);
            Console.ReadLine();
        }


        public static T DeserializeObject<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }




    }
}
