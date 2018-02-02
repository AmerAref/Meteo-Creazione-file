using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Meteo.UI.Authentication;


namespace Meteo.UI
{
    public class LoginOrRegistration
    {
        public static IQueryBuilder _queryBuilder;
        public string _lang;
        private Menu _menu;
        private RegistrationUserUI _registationUserInterface;
        public Services.Models.User _authentication;

        public LoginOrRegistration(string lang, IQueryBuilder queryBuilderCostr)
        {
            _lang = lang;
            _queryBuilder = queryBuilderCostr;
            _menu = new Menu(_queryBuilder, _lang);
            _registationUserInterface = new RegistrationUserUI(_lang);
        }

        public string RegistrationNewAccount()
        {
            var nameNewAccount = "";
            var surnameNewAccount = "";
            var pswNewAccount = "";
            var newUsername = "";
            var idSelectedForQuestion = 0;
            var measureUnit = "";
            var languageNewAccunt = "";
            var encryptedAnswer = "";
            var encryptedPwd = "";

            nameNewAccount = _registationUserInterface.ReadName();
            surnameNewAccount = _registationUserInterface.ReadSurname();

            newUsername = ReadUserAndAuthenication();
            pswNewAccount = ReadNewPsw();
            encryptedPwd = ComparisonPswAndEcrypted(pswNewAccount);
            idSelectedForQuestion = _menu.SelectQuestion();
            var questionselect = _queryBuilder.GetQuestion(idSelectedForQuestion);
            encryptedAnswer = ReadAnswerAndEcrypted(questionselect);
            _lang = _menu.SelectLanguage();

            if (_lang == "1")
            {
                languageNewAccunt = "it";
                measureUnit = "metric";
            }
            else
            {
                languageNewAccunt = "en";
                measureUnit = "imperial";
            }
            var roleNewAccount = 2;
            _queryBuilder.InsertNewUser(encryptedPwd, newUsername, surnameNewAccount, nameNewAccount, idSelectedForQuestion, encryptedAnswer, languageNewAccunt, measureUnit, roleNewAccount);
            return newUsername;
        }


        private string ReadUserAndAuthenication()
        {

            var newUsername = "";
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                newUsername = _registationUserInterface.ReadUser();
                _authentication = _queryBuilder.GetUser(newUsername);
                if (_authentication != null)
                {
                    _registationUserInterface.IfUsernameExist();
                    if (countAttempts == 2)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    countAttempts = 3;
                }

            }
            return newUsername;
        }

        private string ReadNewPsw()
        {


            var pswNewAccount = "";
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                pswNewAccount = _registationUserInterface.ReadPsw();

                // Controlla se Accetta i criteri di sicurezza psw
                if (Helper.RegexForPsw(pswNewAccount) == false)
                {
                    _registationUserInterface.ReadPswSecondTime();
                    if (countAttempts == 2)
                    {
                        Environment.Exit(0);
                        return null;
                    }
                }
                else
                {
                    countAttempts = 3;
                }

            }
            return pswNewAccount;
        }


        private string ComparisonPswAndEcrypted(string pswNewAccount)
        {


            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                var pswComparison = _registationUserInterface.ComparisonPsw();

                // maschera reinserimento psw
                // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                if (pswNewAccount != pswComparison)
                {
                    _registationUserInterface.PswNotEquals();
                    if (countAttempts == 2)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    var encryptedPwd = Register.EncryptPwd(pswNewAccount);
                    countAttempts = 3;
                    return encryptedPwd;
                }
            }
            return null;
        }


        private string ReadAnswerAndEcrypted(Services.Models.Question questionselect)
        {




            for (var countAttempts = 0; countAttempts < 5; countAttempts++)
            {

                Console.WriteLine(questionselect.DefaultQuestion);
                // conferma rispost inserita 
                var readAnswer = Console.ReadLine();
                _registationUserInterface.ConfirmationAnswer(readAnswer);
                var confermation = _menu.Confirmation();
                if (confermation == "1")
                {
                    var encryptedAnswer = Register.EncryptPwd(readAnswer);
                    return encryptedAnswer;
                }
                else if (countAttempts == 4)
                {
                    Environment.Exit(0);
                    return null;
                }
            }
            return null;

        }











































        public string Login()
        {
            var loginInterface = new LoginUserUI(_lang);


            _authentication = AuthenicationWithUsernameAndPsw();
            if (_authentication != null)
            {
                loginInterface.WelcomeUser(_authentication.Username);
                return _authentication.Username;
            }
            _authentication = GetUserIfExist();
            var questionPrinted = _queryBuilder.GetQuestion(_authentication.IdQuestion).DefaultQuestion;
            _authentication = AuthenticationWithAnswer(_authentication.Username, questionPrinted);
            var pswNewAccount = ReadNewPsw();
            _authentication = AuthenicationWithUsernameAndPsw();
            if (_authentication != null)
            {
                loginInterface.WelcomeUser(_authentication.Username);
                return _authentication.Username;
            }
            Environment.Exit(0);
            return null; 
        }








        private Services.Models.User GetUserIfExist()

        {
            var loginInterface = new LoginUserUI(_lang);

            for (var c = 0; c < 3; c++)
            {
                var forAnswerReadUsername = loginInterface.SecureQuestion();
                var userIfExist = _queryBuilder.GetUser(forAnswerReadUsername);
                if (userIfExist != null)
                {
                    return userIfExist;
                }
            }
            Environment.Exit(0);
            return null;

        }






        private Services.Models.User AuthenicationWithUsernameAndPsw()
        {
            var loginInterface = new LoginUserUI(_lang);

            for (var c = 0; c < 3; c++)
            {
                // Inserimento User
                var usernameAuthentication = loginInterface.ReadUsername();
                var authPwd = loginInterface.ReadPassword();

                // confronto se esiste psw (Massimo 3 volte )
                _authentication = _queryBuilder.GetUserIfExist(usernameAuthentication, authPwd);
                if (_authentication!= null)
                {
                    return _authentication;
                }

            }
            return _authentication;


        }

        private Services.Models.User AuthenticationWithAnswer(string forAnswerInsertUsername,string questionPrinted)
        {
            for (var c = 0; c < 3; c++)
            {
                Console.WriteLine(questionPrinted);

                var answerToLogin = "";
                var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                // criptaggio della Risposta inserita 
                var insertAnswerForAccessEcrypted = Register.EncryptPwd(insertAnswerMaskered);
                _authentication = _queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, forAnswerInsertUsername);
                if (_authentication != null)
                {
                    return _authentication;

                }
            }
            Environment.Exit(0);
            return null;


        }
    }
}