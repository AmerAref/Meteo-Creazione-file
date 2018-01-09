using System;
using System.Net.Mail;

namespace Meteo.Services
{
    public class EmailManager
    {
        public void SendFile(string fileName, string sender, string receiver, string body, string subject, string user, string password)
        {
            var data = new Attachment(fileName);

            var message = new MailMessage(sender, receiver);
            message.Attachments.Add(data);
            message.Body = body;
            message.Subject = subject;

            var client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(user, password);
            client.EnableSsl = true;
            client.Send(message);
            data.Dispose();
        }
        public void AttempsPasswordAndSendEmail(string fileName, string sender, string receiver, string body, string subject, string user, string password)
        {
            var countAttempts = 2;
            for (var i = 0; i <= 3; i++)
            {
                try
                {
                    password = DataMaskManager.MaskData("");
                    //autenticazione mail
                    SendFile(fileName, sender, receiver, body, subject, user, password);
                    i = 3;
                    Console.WriteLine("\nEmail inviata con successo!");
                }
                catch
                {
                    if (i == 2)
                    {
                        Console.WriteLine("\nTentativi finiti");
                        i = 3;
                    }
                    Console.WriteLine($"\nTentavi rimasti {countAttempts}");
                    countAttempts--;
                    if (i != 3)
                    {
                        Console.WriteLine("Autenticazione non riuscita");
                        Console.WriteLine("Inserisci password");
                    }
                }
            }
        }
    }
}