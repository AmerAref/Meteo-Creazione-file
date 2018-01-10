using System;
using System.Linq;
using Meteo.Services.Models;
namespace Meteo.Services.Infrastructure
{
    public class QueryManager
    {
        public User UserExistance(ApplicationDbContext context, string forAnswerInsertUsername)
        {
            var userIfExist = context.Users.SingleOrDefault(x => x.Username.Equals(forAnswerInsertUsername));

            return userIfExist;
        }

        public Question QuestionExistance(ApplicationDbContext context, int reciveIdQuestion, User userIfExist)
        {
            reciveIdQuestion = Convert.ToInt32(userIfExist.Question);
            var printQuestionForAccessIfExist = context.Questions.SingleOrDefault(x => x.IdQuestion == reciveIdQuestion);

            return printQuestionForAccessIfExist;
        }

        public string QuestionControl(ApplicationDbContext context, int reciveIdQuestion)
        {
            var controller = context.Questions.SingleOrDefault(x => x.IdQuestion == reciveIdQuestion).DefaultQuestions;

            return controller;
        }
    }
}