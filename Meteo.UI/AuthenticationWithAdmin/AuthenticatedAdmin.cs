using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI.AdminActions
{
    public class AuthenticatedAdmin



    {
        public  Menu menu;
        public string _lang;
        public AuthenticatedAdmin(string menuLang)
        {
            _lang = menuLang;
        }
        public static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        public static AdminInterface adminInterface;


        public void LoginAdmin()
        {
            var print = new PrintData();
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
                    ModifyUserTable();
                    break;
                case "4":

                    adminInterface.Exit();
                    Environment.Exit(0);
                    break;


            }
            return;
        }

        private void ModifyUserTable()
        {
            {
                menu.ShowSecondMenuAdmin();

                var secondAdminChoice = Console.ReadLine();
                switch (secondAdminChoice)
                {
                    case "1":
                        adminInterface.InsertUsernameToDelete();
                        var usernameDelete = Console.ReadLine();
                        queryBuilder.DeleteUser(usernameDelete);
                        break;
                    case "2":
                        var pswModify = "";
                        var pswModifyCompare = "";
                        var pswModifyCount = 0;
                        adminInterface.InsertNmeUserToModfy();
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
                                    break;
                                }
                            }
                        }
                        break;
                    case "3":
                        adminInterface.InsertNmeUserToModfy();
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
    }
}
