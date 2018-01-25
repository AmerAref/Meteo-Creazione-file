using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

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



        public  string AuthenticationOrRegistration()
        {
            var lang = "";
            var usernameAuthentication = "";
            var choseCreateNewAccuoutOrLogin = "";


            queryBuilder = QueryBuilderServices.QueryBuilder();


            menu = new Menu( queryBuilder,  _lang,  null);





            var registationUserInterface = new RegistrationUserInterface(_lang);

            choseCreateNewAccuoutOrLogin = Console.ReadLine();
            var controlInsertValueInSwitch = true;  
            while (controlInsertValueInSwitch)
            {
                switch (choseCreateNewAccuoutOrLogin)
                {
                    case "1":

                        //Caso scelta Login con utente già registrato su DB


                        var accessOrNot = loginServices.LoginWithUserAndPsw();
                        if (accessOrNot == false)
                        {
                            loginServices.GetQuestion();
                            loginServices.AutenticationWithAnswer();
                            usernameAuthentication = loginServices.InsertNewPsw();
                            return usernameAuthentication;


                        }



                        break;

                    case "2":
                        usernameAuthentication = Registrartion(registationUserInterface);
                        return usernameAuthentication;

                }

            }
            return null;

           
        }
        private string Registrartion(RegistrationUserInterface registationUserInterface)
        {
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
    }
}







