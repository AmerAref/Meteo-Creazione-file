using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI.AdminManager
{
    public class AdminActions
    {
        public static AdminManagerUI _adminInteractions;
        public static string _lang;
        public static IQueryBuilder _queryBuilder;
        public static Menu _menu;
        public IService _exit;


        public AdminActions(string menuLang, IQueryBuilder queryBuilderForCostr, IService exit)
        {
            _exit = exit;
            _lang = menuLang;
            _adminInteractions = new AdminManagerUI(_lang, _exit);
            _queryBuilder = queryBuilderForCostr;
            _menu = new Menu(_queryBuilder, _lang, _exit);
        }

        public void AdminLogic(string choiceSelect)
        {
            var updateCity = new UpdateCity();

            var print = new PrintData();


            switch (choiceSelect)
            {
                case "1":
                    var allUsers = _queryBuilder.GetAllUsers();
                    print.PrintAllUsers(allUsers);
                    break;
                case "2":
                    var allMasterRecords = _queryBuilder.GetAllMasterRecords();
                    print.PrintAllMasterRecords(allMasterRecords);
                    break;
                case "3":
                    var secondAdminChoice = _menu.ShowSecondMenuAdmin();
                    ModifyUserTable(secondAdminChoice);
                    break;
                case "4":
                    return;
                case "5":
                    updateCity.DownloadJsonCity();
                    var allCity = updateCity.DataReadyToInsert();
                    _queryBuilder.UpdateCities(allCity);

                    break;
                case "6":
                    _adminInteractions.Exit();
                    Environment.Exit(0);
                    break;
            }
        }

        public void ModifyUserTable(string secondAdminChoice)
        {
            {
                switch (secondAdminChoice)
                {
                    case "1":
                        _adminInteractions.InsertUsernameToDelete();
                        var usernameDelete = Console.ReadLine();
                        _queryBuilder.DeleteUser(usernameDelete);
                        break;
                    case "2":
                        ModifyPsw();
                        break;
                    case "3":
                        _adminInteractions.InsertNameUserToModfy();
                        var usernameRoleModify = Console.ReadLine();
                        var roleModify = Convert.ToInt32(_menu.SelectRole());
                        _queryBuilder.QueryForUpdateRole(usernameRoleModify, roleModify);
                        break;
                    case "4":
                        break;
                    case "5":
                        _adminInteractions.Exit();
                        Environment.Exit(0);
                        break;
                }
            }
            return;
        }
        private void ModifyPsw()
        {
            var pswModifyCount = 0;
            var usernameModify = _adminInteractions.InsertNameUserToModfy();
            for (pswModifyCount = 0; pswModifyCount != 3; pswModifyCount++)
            {
                var firstPsw = _adminInteractions.InsertFirstPsw();
                var secondPsw = _adminInteractions.InsertSecondPsw();

                if (secondPsw == firstPsw)
                {
                    if (Helper.RegexForPsw(firstPsw) == true)
                    {
                        var pswModifyCrypto = Register.EncryptPwd(secondPsw);
                        _queryBuilder.QueryForUpdatePsw(pswModifyCrypto, usernameModify);
                        pswModifyCount = 3;
                        return;
                    }
                    else
                    {
                        _adminInteractions.AttemtsRegexPsw();
                    }
                }
                else
                {
                    pswModifyCount++;
                    _adminInteractions.AttemptsPsw(pswModifyCount);

                    if (pswModifyCount == 3)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}