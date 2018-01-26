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
        private string _pswNewAccount;
        private string _encryptedPwd;
        private string _encryptedAnswer;
        private string _languageNewAccount;
        private string _measureUnit;
        private string _surnameNewAccount;
        private string _nameNewAccuont;
        private string _newUsername;
        private int _idSelectedForQuestion;


        public string Registrartion()
        {
            var registationUserInterface = new RegistrationUserInterface(_lang);

            registationUserInterface.InsertName();
            _nameNewAccuont = Console.ReadLine();
            registationUserInterface.InsertSurname();
            _surnameNewAccount = Console.ReadLine();
            for (var countAttempts = 0; countAttempts < 3; countAttempts++)
            {
                registationUserInterface.InsertUser();
                _newUsername = Console.ReadLine();
                var autentication = queryBuilder.GetUser(_newUsername);
                if (autentication != null)
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
                var passwordRegistration = "";
                registationUserInterface.InserPsw();
                _pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
                // Controlla se Accetta i criteri di sicurezza psw
                if (Helper.RegexForPsw(_pswNewAccount) == false)
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

                var passwordComparisonNotEcrypted = "";
                // maschera reinserimento psw
                var pswComparison = DataMaskManager.MaskData(passwordComparisonNotEcrypted);
                // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                if (_pswNewAccount != pswComparison)
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
                    _encryptedPwd = Register.EncryptPwd(_pswNewAccount);
                    countAttempts = 3;
                }
            }

            menu.SelectQuestion();
            _idSelectedForQuestion = Convert.ToInt32(Console.ReadLine());

            var questionselect = queryBuilder.GetQuestion(_idSelectedForQuestion);


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
                    _encryptedAnswer = Register.EncryptPwd(insertAnswer);
                    countAttempts = 5;
                }
                else if (countAttempts == 4)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            menu.SelectLanguage();

            var lenguageSelectedForUnitMeasure = Console.ReadLine();


            if (_lang == "1")
            {
                lenguageSelectedForUnitMeasure = "it";
                _measureUnit = "metric";
            }
            else
            {
                lenguageSelectedForUnitMeasure = "en";
                _measureUnit = "imperial";
            }
            var roleNewAccount = 2;
            queryBuilder.InsertNewUser(_encryptedPwd, _newUsername, _surnameNewAccount, _nameNewAccuont, _idSelectedForQuestion, _encryptedAnswer, _languageNewAccount, _measureUnit, roleNewAccount);
            return _newUsername;
        }










































        public string Login()
        {
            var countAttempts = 2;
            var passwordLogin = "";

            for (var countPsw = 0; countPsw < 5; countPsw++)
            {
                var loginInterface = new LoginUserInterface(_lang);
                // Inserimento User

                loginInterface.InsertUsername();
                var _usernameAuthentication = Console.ReadLine();

                loginInterface.InsertPassword();
                var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);




                // Criptaggio Psw
                var authPwd = Register.EncryptPwd(passwordAuthentication);
                // confronto se esiste psw (Massimo 3 volte )
                var autentication = queryBuilder.GetUserIfExist(_usernameAuthentication, authPwd);
                if (autentication != null)
                {
                    loginInterface.WelcomeUser(_usernameAuthentication);
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
                            var _forAnswerInsertUsername = loginInterface.SecureQuestion();
                            var userIfExist = queryBuilder.GetUser(_forAnswerInsertUsername);
                            if (userIfExist != null)
                            {
                                var IdQuestionInTableUser = queryBuilder.GetUser(_forAnswerInsertUsername).IdQuestion;
                                var question = queryBuilder.GetQuestion(IdQuestionInTableUser).DefaultQuestion;
                                if (question != null)
                                {
                                    countUserExists = 4;
                                    var attemptsAnswerForQuestion = 0;
                                    for (var countAnswerWrong = 0; countAnswerWrong < 4; countAnswerWrong++, attemptsAnswerForQuestion++)
                                    {
                                        var answerToLogin = "";
                                        Console.WriteLine(question);
                                        var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                                        Console.WriteLine();
                                        // criptaggio della Risposta inserita 
                                        var insertAnswerForAccessEcrypted = Register.EncryptPwd(insertAnswerMaskered);
                                        autentication = queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, _forAnswerInsertUsername);
                                        if (autentication != null)
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
                                        var newPswClear = "";
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
                                            queryBuilder.QueryForUpdatePsw(newPswEncrypted, _forAnswerInsertUsername);
                                            countPsw = 5;
                                            _usernameAuthentication = _forAnswerInsertUsername;
                                            return _usernameAuthentication;
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
        public bool LoginWithUserAndPsw()
        {
            var loginInterface = new LoginUserInterface(_lang);

            var countAttempts = 2;
            var passwordLogin = "";

            for (var m = 0; m < 2; m++)
            {

                loginInterface.InsertUsername();
                var _usernameAuthentication = Console.ReadLine();

                loginInterface.InsertPassword();
                var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);

                // Criptaggio Psw
                var authPwd = Register.EncryptPwd(passwordAuthentication);
                // confronto se esiste psw (Massimo 3 volte )
                var autentication = queryBuilder.GetUserIfExist(_usernameAuthentication, authPwd);
                if (autentication != null)
                {
                    // Da il benvenuto e accede al menu Meteo
                    loginInterface.WelcomeUser(_usernameAuthentication);
                    return true;


                }
                else
                {
                    // Reinserimento Psw (massimo altri 2 tentativi)

                    loginInterface.WrongPassword(countAttempts);



                }


                countAttempts--;

            }
            return false;
        }


        public void GetQuestion()
        {
            for (var m = 0; m < 2; m++)
            {


                if (_lang == "1")
                {
                    Console.WriteLine($"{DataInterface.secureQuestionIT}");

                }
                else
                {

                    Console.WriteLine($"{DataInterface.secureQuestionEN}");
                }

                _usernameAuthentication = Console.ReadLine();
                var userIfExist = queryBuilder.GetUser(_usernameAuthentication);
                if (userIfExist != null)
                {
                    var IdQuestionInTableUser = queryBuilder.GetUser(_usernameAuthentication).IdQuestion;
                    _questionPrintedForAccess = queryBuilder.GetQuestion(IdQuestionInTableUser).DefaultQuestion;
                    return;
                }

            }
            Environment.Exit(0);
            return;

        }



        public void AutenticationWithAnswer()
        {
            for (var m = 0; m < 2; m++)
            {

                Console.WriteLine(_questionPrintedForAccess);
                var answerToLogin = "";
                var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                Console.WriteLine();
                // criptaggio della Risposta inserita 
                var insertAnswerForAccessEcrypted = Register.EncryptPwd(insertAnswerMaskered);
                var autentication = queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, _usernameAuthentication);
                if (autentication != null)
                {
                    return;
                }


            }
            Environment.Exit(0);
            return;

        }


        public string InsertNewPsw()
        {
            for (var m = 0; m < 2; m++)
            {
                if (_lang == "1")
                {
                    Console.WriteLine($"\n{DataInterface.newPswIT}");
                }
                else
                {
                    Console.WriteLine($"\n{DataInterface.newPswEN}");
                }

                var newPswClear = "";
                // maschera nuova psw 
                var newPswMask = DataMaskManager.MaskData(newPswClear);
                // controlo su vincoli di sicurezza psw
                if (Helper.RegexForPsw(newPswMask) == false)
                {
                    if (_lang == "1")
                    {
                        Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                        Console.WriteLine("\nReinserisci Password!");
                    }
                    else
                    {
                        Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
                        Console.WriteLine("\nReenter Password!");
                    }


                }
                else
                {
                    // criptaggio nuova psw 
                    var newPswEncrypted = Register.EncryptPwd(newPswMask);

                    // Aggiornamento psw in DB
                    queryBuilder.QueryForUpdatePsw(newPswEncrypted, _usernameAuthentication);
                    // Uscita dal while 
                    return _usernameAuthentication;

                }
            }
            Environment.Exit(0);
            return null;



        }

    }
}








