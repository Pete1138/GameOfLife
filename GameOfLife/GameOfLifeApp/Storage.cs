using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GameOfLifeApp
{
    public class Storage
    {
        //private readonly JavaScriptSerializer _javaScriptSerializer;
        private const string FilePath = @"C:\Repos\GameOfLife\Boards";

        public void Store<T>(T objectToSerialize, string fileName)
        {
            using (var stream = new FileStream(Path.Combine(FilePath, fileName), FileMode.Create))
            {

                var serializer = new DataContractSerializer(typeof (T));
                serializer.WriteObject(stream, objectToSerialize);
                var writer = new StreamWriter(stream);
                writer.Close();
            }
        }

        public T Load<T>(string fileName)
        {
            T deserializedObject;

            using (var reader = new FileStream(Path.Combine(FilePath,fileName), FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractSerializer(typeof(T));
                deserializedObject = (T)serializer.ReadObject(reader);
            }

            return deserializedObject;
        }
    }
}
