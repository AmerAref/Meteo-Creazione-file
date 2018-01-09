using System;
using System.Linq;

namespace Meteo.Services
{
    public class QueryManager
    {
        public User UserExistance(Context context, string forAnswerInsertUsername)
        {
            var userIfExist = context.Users.SingleOrDefault(x => x.Username.Equals(forAnswerInsertUsername));

            return userIfExist;
        }

        public Question QuestionExistance(Context context, int reciveIDQuestion, User userIfExist)
        {
            reciveIDQuestion = Convert.ToInt32(userIfExist.Question);
            var printQuestionForAccessIfExist = context.QuestionForUsers.SingleOrDefault(x => x.IdDomanda == (reciveIDQuestion));

            return printQuestionForAccessIfExist;
        }

        public string QuestionControl(Context context, int reciveIDQuestion)
        {
            var controller = context.QuestionForUsers.SingleOrDefault(x => x.IdDomanda == (reciveIDQuestion)).Domande;

            return controller;
        }
    }
}