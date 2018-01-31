using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Meteo.UI.Authentication;


namespace Meteo.UI
{
    public class LoginOrRegistration
    {
        public static IQueryBuilder queryBuilder;
        public string _lang;
        private Menu menu;

        public LoginOrRegistration(string lang, IQueryBuilder queryBuilderCostr)
        {
            _lang = lang;
            queryBuilder = queryBuilderCostr;
            menu = new Menu(queryBuilder);


        }

        public Services.Models.User _authentication;
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
            var registationUserInterface = new RegistrationUserFrontEnd(_lang);
            menu.ChangeLangages(_lang);

            nameNewAccount = registationUserInterface.ReadName();
            surnameNewAccount = registationUserInterface.ReadSurname();

            newUsername = ReadUserAndAuthenication();
            pswNewAccount = RegexControl();
            encryptedPwd = ComparisonPswAndEcrypted(pswNewAccount);
            idSelectedForQuestion = menu.SelectQuestion();
            var questionselect = queryBuilder.GetQuestion(idSelectedForQuestion);
            encryptedAnswer = ReadAnswerAndEcrypted(questionselect);
            _lang = menu.SelectLanguage();

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
            queryBuilder.InsertNewUser(encryptedPwd, newUsername, surnameNewAccount, nameNewAccount, idSelectedForQuestion, encryptedAnswer, languageNewAccunt, measureUnit, roleNewAccount);
            return newUsername;
        }


        private string ReadUserAndAuthenication()
        {
            var registationUserInterface = new RegistrationUserFrontEnd(_lang);
            var newUsername = "";
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                newUsername = registationUserInterface.ReadUser();
                _authentication = queryBuilder.GetUser(newUsername);
                if (_authentication != null)
                {
                    registationUserInterface.IfUsernameExist();
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

        private string RegexControl()
        {
            var registationUserInterface = new RegistrationUserFrontEnd(_lang);

            var pswNewAccount = "";
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                pswNewAccount = registationUserInterface.ReadPsw();

                // Controlla se Accetta i criteri di sicurezza psw
                if (Helper.RegexForPsw(pswNewAccount) == false)
                {
                    registationUserInterface.ReadPswSecondTime();
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
            var registationUserInterface = new RegistrationUserFrontEnd(_lang);

            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                var pswComparison = registationUserInterface.ComparisonPsw();

                // maschera reinserimento psw
                // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                if (pswNewAccount != pswComparison)
                {
                    registationUserInterface.PswNotEquals();
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

            var registationUserInterface = new RegistrationUserFrontEnd(_lang);



            for (var countAttempts = 0; countAttempts < 5; countAttempts++)
            {

                Console.WriteLine(questionselect.DefaultQuestion);
                // conferma rispost inserita 
                var readAnswer = Console.ReadLine();
                registationUserInterface.ConfirmationAnswer(readAnswer);
                var confermation =  menu.Confirmation();
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

            var countAttempts = 2;
            var forAnswerInsertUsername = "";
            var attemptsAnswerForQuestion = 0;
            var newPswClear = "";

            var loginInterface = new LoginUserFrotEnd(_lang);





            for (var countPsw = 0; countPsw < 5; countPsw++)
            {
                _authentication = AuthenicationWithUsernameAndPsw();

                if (_authentication != null)
                {
                    loginInterface.WelcomeUser(_authentication.Username);
                    countPsw = 5;
                    return _authentication.Username;
                }
                else
                {
                    // Reinserimento Psw (massimo altri 2 tentativi)
                    if (countPsw < 2)
                    {
                        loginInterface.WrongPassword(countAttempts);
                    }
                    countAttempts--;

                    // al terzo tentativo da la possibilà di recuperare e modificare psw tramite domanda sicurezza precedentemente impostata
                    if (countPsw == 2)
                    {
                        for (var countUserExists = 0; countUserExists < 4; countUserExists++)
                        {
                            if (countUserExists == 3)
                            {
                                //termina sessione in caso in cui sbaglia tre volte a digitare psw e/o username
                                Environment.Exit(0);
                                return null;
                            }
                            forAnswerInsertUsername = loginInterface.SecureQuestion();
                            var userIfExist = queryBuilder.GetUser(forAnswerInsertUsername);
                            if (userIfExist != null)
                            {
                                var IdQuestionInTableUser = queryBuilder.GetUser(forAnswerInsertUsername).IdQuestion;
                                var question = queryBuilder.GetQuestion(IdQuestionInTableUser).DefaultQuestion;
                                if (question != null)
                                {
                                    countUserExists = 4;
                                    for (var countAnswerWrong = 0; countAnswerWrong < 4; countAnswerWrong++, attemptsAnswerForQuestion++)
                                    {
                                        _authentication = AuthenticationWithAnswer(question, forAnswerInsertUsername);

                                        if (_authentication != null)
                                        {
                                            loginInterface.InsertNewPassword();
                                            // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
                                            countAnswerWrong = 4;
                                        }
                                        if (attemptsAnswerForQuestion == 2)
                                        {
                                            Environment.Exit(0);
                                            return null;
                                        }
                                    }

                                    for (var countAttemptsPswRegister = 0; countAttemptsPswRegister < 3; countAttemptsPswRegister++)
                                    {
                                        // maschera nuova psw 
                                        var newPswMask = DataMaskManager.MaskData(newPswClear);
                                        // controlo su vincoli di sicurezza psw
                                        if (Helper.RegexForPsw(newPswMask) == false)
                                        {
                                            loginInterface.WrongRegexNewPassowrd();
                                            // uscita in caso di 3 errori
                                            if (countAttemptsPswRegister == 3)
                                            {
                                                loginInterface.FinishedAttempts();
                                                Environment.Exit(0);
                                                return null;
                                            }
                                        }
                                        else
                                        {
                                            // criptaggio nuova psw 
                                            var newPswEncrypted = Register.EncryptPwd(newPswMask);
                                            // Aggiornamento psw in DB
                                            queryBuilder.QueryForUpdatePsw(newPswEncrypted, forAnswerInsertUsername);
                                            countPsw = 5;
                                            _authentication.Username = forAnswerInsertUsername;
                                            return _authentication.Username;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private Services.Models.User AuthenicationWithUsernameAndPsw()
        {
            var passwordLogin = "";
            var loginInterface = new LoginUserFrotEnd(_lang);


            // Inserimento User
            loginInterface.InsertUsername();
            var usernameAuthentication = Console.ReadLine();
            loginInterface.InsertPassword();
            var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
            var authPwd = Register.EncryptPwd(passwordAuthentication);
            // confronto se esiste psw (Massimo 3 volte )
            _authentication = queryBuilder.GetUserIfExist(usernameAuthentication, authPwd);
            return _authentication;
            // se esiste utente ritorna diverso da null 
        }

        private Services.Models.User AuthenticationWithAnswer(string question, string forAnswerInsertUsername)
        {
            var answerToLogin = "";
            Console.WriteLine(question);
            var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
            Console.WriteLine();
            // criptaggio della Risposta inserita 
            var insertAnswerForAccessEcrypted = Register.EncryptPwd(insertAnswerMaskered);
            _authentication = queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, forAnswerInsertUsername);
            return _authentication;

        }
    }
}