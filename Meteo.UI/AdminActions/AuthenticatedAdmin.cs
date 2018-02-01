using System;
using Meteo.Services.Infrastructure;

namespace Meteo.UI.AdminActions
{
    public class FirstActions
    {
        public  Menu menu;
        public string _lang;
        public FirstActions(string menuLang)
        {
            _lang = menuLang;
        }
        public static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();

        public FirstActions()
        {
            var adminInterface = new AdminInterface(_lang);
            var print = new PrintData();
            // var secondActions = new SecondActions();
            menu.ShowFirtsMenuAdmin();
            var roleChoiceSelect = Console.ReadLine();
            switch (roleChoiceSelect)
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
                    // secondActions.
                    break;
                case "4":

                    adminInterface.Exit();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}