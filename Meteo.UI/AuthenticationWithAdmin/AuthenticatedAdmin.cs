using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI.AdminActions
{

    public class AuthenticatedAdmin



    {
        public static AdminInterface adminInterface;
        public static string _lang;
        public static IQueryBuilder queryBuilder;
        public static Menu menu;

        public AuthenticatedAdmin(string menuLang, IQueryBuilder queryBuilderForCostr)
        {
            _lang = menuLang;
            adminInterface = new AdminInterface(_lang);
            queryBuilder = queryBuilderForCostr;
            menu = new Menu(queryBuilder);
        }

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
                    menu.ShowSecondMenuAdmin();
                    var secondAdminChoice = Console.ReadLine();
                    ModifyUserTable(secondAdminChoice);
                    break;
                case "4":

                    return;
                case "5":

                    adminInterface.Exit();
                    Environment.Exit(0);
                    break;
            }

        }

        public void ModifyUserTable(string secondAdminChoice)
        {
            menu.ChangeLangages(_lang);
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
            var pswModifyCount = 0;
            var usernameModify = adminInterface.InsertNameUserToModfy();
            for (pswModifyCount = 0; pswModifyCount != 3; pswModifyCount++)
            {
                var firstPsw = adminInterface.InsertFirstPsw();
                var secondPsw = adminInterface.InsertSecondPsw();

                if (secondPsw == firstPsw)
                {
                    if (Helper.RegexForPsw(firstPsw) == true)
                    {

                        var pswModifyCrypto = Register.EncryptPwd(secondPsw);
                        queryBuilder.QueryForUpdatePsw(pswModifyCrypto, usernameModify);
                        pswModifyCount = 3;
                        return;
                    }
                    else
                    {

                        adminInterface.AttemtsRegexPsw();

                    }
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
