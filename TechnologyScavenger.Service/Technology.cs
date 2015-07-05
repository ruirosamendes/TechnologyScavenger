using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyScavenger.Service
{
    public interface ITechnology
    {
        string Name { get; }
        List<string> PatternsStrings { get;  }
    }

    public class Technology : ITechnology
    {
        string mName;
        List<string> mPatternsStrings;

        public Technology(string name, Stream patternsFile)
        {
            this.mName = name;
            mPatternsStrings = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(patternsFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mPatternsStrings.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }
        public List<string> PatternsStrings
        {
            get
            {
                return mPatternsStrings;
            }
        }

    }
}
