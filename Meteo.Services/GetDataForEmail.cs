using System;
using System.Collections.Generic;

namespace Meteo.Services
{
    public class GetDataForEmail
    {
        public static Dictionary<string, string> InsertDataForEmail(int lang)
        {
            string insertSender = "", insertReciver = "", insertBody = "", insertSubject = "";
            var dictionaryForEmail = new Dictionary<string, string>();
            if (lang == 1)
            {
                insertSender = "Inserisci email del mittente";
                insertReciver = "Inserisci email del destinatario";
                insertBody = "Iserisci il testo all'interno dell'email";
                insertSubject = "Inserisci l'oggetto";
            }
            else
            {
                insertSender = "Insert sender's email";
                insertReciver = "Insert recipient'semail";
                insertBody = "Isert the body text of the email";
                insertSubject = "Insert the object";
            }
            Console.WriteLine(insertSender);
            var sender = Console.ReadLine();
            dictionaryForEmail.Add("senderKey", sender);

            Console.WriteLine();

            Console.WriteLine(insertReciver);
            var receiver = Console.ReadLine();
            Console.WriteLine(insertBody);
            var body = Console.ReadLine();
            Console.WriteLine(insertSubject);
            var subject = Console.ReadLine();

            var user = sender.Split('@')[0];

            dictionaryForEmail.Add("receiverKey", receiver);
            dictionaryForEmail.Add("bodyKey", body);
            dictionaryForEmail.Add("subjectKey", subject);
            dictionaryForEmail.Add("userKey", user);
            if (lang == 1)
            { Console.WriteLine("Inserisci password"); }
            else
            { Console.WriteLine("Insert password"); }

            return dictionaryForEmail;
        }
    }
}