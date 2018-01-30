using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI.AdminActions
{




    //Prendo le credenziali e faccio la validazione sul db
    // se errore lancio eccezione
    // se risposta corretta ritorno valore admin


    public class AuthenticatedAdmin



    {
        public Menu menu;
        public string _lang;
        public AuthenticatedAdmin(string menuLang)
        {
            _lang = menuLang;
        }
        public static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        public static AdminInterface adminInterface;



        public void LoginAdmin(string choiceSelect)
        {
            var print = new PrintData();

            
            switch (choiceSelect)
            {
                case "1":
                    var allUsers = queryBuilder.GetAllUsers();
                    print.PrintAllUsers(allUsers);
                    break;

                case "2":
                    var allMasterRecords = queryBuilder.GetAllMasterRecords();
                    print.PrintAllMasterRecords(allMasterRecords);
                    break;

                case "3":
                    break;
                case "4":

                    break;
                
                case "5":

                    adminInterface.Exit();
                    Environment.Exit(0);
                    break;


            }
            return;
        }

        public void ModifyUserTable(string secondAdminChoice)
        {
            {
               
                switch (secondAdminChoice)
                {
                    case "1":
                        adminInterface.InsertUsernameToDelete();
                        var usernameDelete = Console.ReadLine();
                        queryBuilder.DeleteUser(usernameDelete);
                        break;
                    case "2":
                        ModifyPsw();
                        break;
                    case "3":
                        adminInterface.InsertNameUserToModfy();
                        var usernameRoleModify = Console.ReadLine();
                        menu.SelectRole();
                        var roleModify = Convert.ToInt32(Console.ReadLine());
                        queryBuilder.QueryForUpdateRole(usernameRoleModify, roleModify);
                        break;
                    case "4":

                        break;
                    case "5":

                        adminInterface.Exit();
                        Environment.Exit(0);

                        break;
                }

            }
            return;
        }


        private void ModifyPsw()
        {
            var pswModify = "";
            var pswModifyCompare = "";
            var pswModifyCount = 0;
            adminInterface.InsertNameUserToModfy();
            var usernameModify = Console.ReadLine();
            for (pswModifyCount = 0; pswModifyCount != 3; pswModifyCount++)
            {
                var pswModifyRegex = "";
                adminInterface.InsertSecondPsw();
                pswModifyRegex = DataMaskManager.MaskData(pswModify);
                adminInterface.InsertSecondPsw();
                var pswModifyCompareRegex = DataMaskManager.MaskData(pswModifyCompare);
                if (pswModifyRegex == pswModifyCompareRegex)
                {
                    var pswModifyCrypto = Register.EncryptPwd(pswModifyRegex);
                    queryBuilder.QueryForUpdatePsw(pswModifyCrypto, usernameModify);
                    pswModifyCount = 3;
                }
                else
                {
                    pswModifyCount++;
                    adminInterface.AttemptsPsw(pswModifyCount);

                    if (pswModifyCount == 3)
                    {
                        Environment.Exit(0);

                    }
                }
            }
            
        }
    }
}
