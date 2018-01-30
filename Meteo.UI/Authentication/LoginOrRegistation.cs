using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Meteo.UI.Authentication;


namespace Meteo.UI
{
    public class LoginOrRegistration
    {
        private Menu menu;
        public string _lang;
        public LoginOrRegistration(string lang)
        {
            _lang = lang;
        }
        static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        private int _countAttemptsPswRegister = 0;
        public Services.Models.User _authentication;
        public LoginUserFrotEnd loginInterface;
        public string RegistrationNewAccount()
        {
            var nameNewAccount = "";
            var surnameNewAccount = "";
            var pswNewAccount = "";
            var newUsername = "";
            var idSelectedForQuestion = 0;
            var measureUnit = "";
            var languageNewAccunt = "";
            var passwordRegistration = "";
            var passwordComparisonNotEcrypted = "";
            var encryptedAnswer = "";
            var encryptedPwd = "";
            var registationUserInterface = new RegistrationUserFrontEnd(_lang);

            registationUserInterface.InsertName();
            nameNewAccount = Console.ReadLine();
            registationUserInterface.InsertSurname();
            surnameNewAccount = Console.ReadLine();
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                registationUserInterface.InsertUser();
                newUsername = Console.ReadLine();
                _authentication = queryBuilder.GetUser(newUsername);
                if (_authentication != null)
                {
                    registationUserInterface.IfUsernameExist();
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
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                registationUserInterface.InserPsw();
                pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
                // Controlla se Accetta i criteri di sicurezza psw
                if (Helper.RegexForPsw(pswNewAccount) == false)
                {
                    registationUserInterface.ReinsertPsw();
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
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                registationUserInterface.ComparisonReinsertPsw();

                // maschera reinserimento psw
                var pswComparison = DataMaskManager.MaskData(passwordComparisonNotEcrypted);
                // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                if (pswNewAccount != pswComparison)
                {
                    registationUserInterface.PswNotEquals();
                    if (countAttempts == 2)
                    {
                        Environment.Exit(0);
                        return null;
                    }
                }
                else
                {
                    encryptedPwd = Register.EncryptPwd(pswNewAccount);
                    countAttempts = 3;
                }
            }

            menu.SelectQuestion();
            idSelectedForQuestion = Convert.ToInt32(Console.ReadLine());

            var questionselect = queryBuilder.GetQuestion(idSelectedForQuestion);


            for (var countAttempts = 0; countAttempts < 5; countAttempts++)
            {

                Console.WriteLine(questionselect.DefaultQuestion);
                // conferma rispost inserita 
                var insertAnswer = Console.ReadLine();
                registationUserInterface.ConfirmationAnswer(insertAnswer);
                menu.Confirmation();
                var confermation = Console.ReadLine();
                if (confermation == "1")
                {
                    encryptedAnswer = Register.EncryptPwd(insertAnswer);
                    countAttempts = 5;
                }
                else if (countAttempts == 4)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            menu.SelectLanguage();

            _lang = Console.ReadLine();


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

        public string Login()
        {

            var countAttempts = 2;
            var forAnswerInsertUsername = "";
            var attemptsAnswerForQuestion = 0;
            var newPswClear = "";



            for (var countPsw = 0; countPsw < 5; countPsw++)
            {
                _authentication = AuthenicationUserAndPsw();

                if (_authentication != null)
                {
                    loginInterface.WelcomeUser(_authentication.Username);
                    countPsw = 5;
                    return null;
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

        private Services.Models.User AuthenicationUserAndPsw()
        {
            var passwordLogin = "";

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