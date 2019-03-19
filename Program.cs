using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using Newtonsoft.Json;

namespace SerializationPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();
            using (FileStream reader = new FileStream("people.csv", FileMode.Open))
            {
                byte[] buffer = new byte[reader.Length];
                reader.Read(buffer, 0, buffer.Length);
                string result = Encoding.UTF8.GetString(buffer);
                string[] peopleString = result.Split(new string[] { ",","\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < peopleString.Length - 1; i += 4)
                {
                    Person tmpPerson = new Person{
                        Name = peopleString[i],
                        Surname = peopleString[i + 1],
                        Phone = peopleString[i + 2],
                        BirthYear = peopleString[i + 3],
                    };
                    people.Add(tmpPerson);
                }
            }
            SoapFormatter sp = new SoapFormatter();
            using (FileStream writer = new FileStream("seiralized_people.soap", FileMode.Create))
            {
                foreach (var person in people)
                {
                    sp.Serialize(writer, person);
                }
            }
            string json = JsonConvert.SerializeObject(people);
        }
    }
}
