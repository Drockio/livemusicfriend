using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;

namespace LiveMusicFriend.Models
{
    public class MyException : Exception
    {
        public MyException() : base("My Exception")
        {
            this.HelpLink = "booya!";
        }
    }

    public class Derived : Sandbox, IInterface
    {
        public string myString { get; set; }

        public override void abstractMethod()
        {
            throw new NotImplementedException();
        }
    }

    interface IInterface
    {
        string myString {get; set;}
    }

    public abstract class Sandbox
    {
        string mystring { get; set; }

        public void virtualMethod()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "examplefile.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "This is a file");
            }

            File.AppendAllText(filePath, "more text yo!");

            string results = File.ReadAllText(filePath);
        }

        public abstract void abstractMethod();

        public void Arrays()
        {
            int[] myArray = { 100, 200, 300, 400 };
            int first = myArray.FirstOrDefault();

            int[,] multiDim = new int[,]{
                {0,1,2},
                {3,4,5},
                {6,7,8}
                             };
            int[] reverse = myArray.Reverse().ToArray();
            IEnumerable<int> reverseList = myArray.Reverse();
            int multiArray = multiDim.Rank;

            ArrayList myAl = new ArrayList();
            myAl.Add(new Event("whatevs"));
            myAl.Add(new Search());

            foreach (object obj in myAl)
            {
                if (obj is Event)
                {
                    string smack = "";
                }

                else if (obj is Search)
                {
                    string booya = "";
                }
            }

            Queue myQueue = new Queue();
            myQueue.Enqueue(new Event("whatevs"));

            Hashtable myHT = new Hashtable();
            myHT.Add("sea", "seattle tacoma");

            string airport = myHT["sea"].ToString();

            string whatevs = "";
        }
    }
}