using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Go
{
    public static class Serializer
    {
        public static void Serialize(object o, string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, o);
            stream.Close();
        }

        public static object Deserialize(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            object o = formatter.Deserialize(stream);
            stream.Close();
            return o;
        }
    }
}
