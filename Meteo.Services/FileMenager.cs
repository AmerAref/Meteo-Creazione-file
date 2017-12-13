using System;
using System.Net.Mail;
using System.IO;
namespace Meteo.Services
{
    public class FileMenager
    {
        public string CreateNewFile(string fileName, string jsonStr)
        {
            System.IO.File.WriteAllText(fileName, jsonStr);
            return fileName;

        }
        public void SendFile(string fileName, string sender, string receiver, string body, string subject, string user, string password)
        {
            MailMessage message = new MailMessage(sender, receiver);


            Attachment data = new Attachment(fileName);

            message.Attachments.Add(data);
            message.Body = body;
            message.Subject = subject;
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(user, password);
                client.EnableSsl = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                            ex.ToString());
            }

            data.Dispose();
        }
        public void DeleteFile(string fileNameDelete)
        {
            File.Delete(fileNameDelete);
        }
    
    }
}
