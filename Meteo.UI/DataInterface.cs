using System.IO;

namespace Meteo.UI
{
    public static class DataInterface
    {
        public static string insertNameFileIT = "Inserisci nome file";
        public static string insertNameFileEN = "Enter the name of the file";
        public static string successIT = "Richiesta elaborata con successo", successEN = "request processed successfully";
        public static string choiceDoFileIT = "Vuoi creare un file JSON con i dati precedentemente richiesti?";
        public static string choiceDoFileEN = "Do you want to create a JSON file with the data previously requested? ";
        public static string choiceSendEmailIT = "Vuoi inviare tramite email il file appena creato? ";
        public static string choiceSendEmailEN = "Do you want to send an email with the newly created file? ";
        public static string insertNamePlaceIT = "Inserisci località richiesta", insertNamePlaceEN = "Enter requested location";
        public static string insertLonIT = "Insersci longitudine", insertLonEN = "Enter longitude";
        public static string insertLatIT = "Inserisci latitudine", insertLatEN = "Enter latitute";
        public static string successCreateFileIT = "File creato con successo", successCreateFileEN = "File successfully created";
        public static string choiceCreateXlsFileIT = "Vuoi creare un file XLS con i dati precedenti? ";
        public static string choiceCreateXlsFileEN = "Do you want to create an XLS file with the previous data? ";
        public static string insertUserIT = "\nInserisci Username", insertUserEN = "\nEnter Username";
        public static string insertPswIT = "Inserisci Password", insertPswEN = "Enter Password";
        public static string newPswIT = "Inserisci la nuova password", newPswEN = "Enter the new password";
        public static string reinsertUserPswIT = "\nReinsersci Username e password", reinsertUserPswEN = "\nReenter Username and Password";
        public static string remainingAttemptsIT = "\nTentativi rimasi: ", remainingAttemptsEN = "\nRemaining attempts: ";
        public static string secureQuestionIT = "Inserisci Username per la domanda di sicurezza", secureQuestionEN = "Enter Username for the security question";
        public static string insertNameFileXlsIT = "Inserisci nome file";
        public static string insertNameFileXlsEN = "Insert file name";
        public static string filePath = Directory.GetCurrentDirectory();
    }
}