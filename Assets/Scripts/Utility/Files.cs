using System.Collections.Generic;
using System.IO;

namespace ColdCry.Utility
{
    public class Files
    {
        public static string[] GetLines(string path)
        {
            StreamReader stream = new StreamReader( path );
            LinkedList<string> lines = new LinkedList<string>();

            while (!stream.EndOfStream) {
                lines.AddLast( stream.ReadLine() );
            }
            return Utility.Collections.ToArray( lines );
        }
    }
}

