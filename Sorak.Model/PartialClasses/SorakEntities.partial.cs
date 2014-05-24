using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sorak.Model.ClientModel;
using Sorak.Model;

namespace Sorak.Model
{
    public partial class SorakEntities
    {
        public void UpdateLastLoginTime(User user)
        {
            try
            {
                user.LastLoginTime = DateTime.Now;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public int InsertNewUser(SorakUser user)
        {
            try
            {
                var dbUser = this.Users.Where(i => i.EmailAddress == user.EmailAddress).FirstOrDefault();

                if (dbUser == null)
                {
                    var now = DateTime.Now;

                    dbUser = new User();
                    dbUser.WindowsUsername = user.WindowsUserName;
                    dbUser.EmailAddress = user.EmailAddress;
                    dbUser.EmployeeName = user.FullName;
                    dbUser.LastLoginTime = DateTime.Now;

                    this.Users.Add(dbUser);
                    this.SaveChanges();
                }
                else
                {
                    dbUser.LastLoginTime = DateTime.Now;
                    this.SaveChanges();
                }

                return dbUser.RecordNum;
            }
            catch (Exception e)
            {
                return -1;
            }

            return -1;
        }

        public User GetExistingUser(string emailAddress)
        {
            try
            {
                var dbUser = this.Users.Where(i => i.EmailAddress == emailAddress).FirstOrDefault();

                if (dbUser != null)
                {
                    return dbUser;
                }
            }
            catch (Exception e)
            {
                
            }

            return null;
        }

        public void Like(int userId, int questionId)
        {
            var question = this.Questions.FirstOrDefault(i => i.RecordNum == questionId);
            
            var user = question.VotingUsers.FirstOrDefault(i => i.RecordNum == userId);
            if (user != null)
                return;

            user = this.Users.FirstOrDefault(i => i.RecordNum == userId);

            question.VotingUsers.Add(user);
            question.LikeCount++;
            this.SaveChanges();
        }

        public void Unlike(int userId, int questionId)
        {
            var question = this.Questions.Include("VotingUsers").FirstOrDefault(i => i.RecordNum == questionId);
            var user = question.VotingUsers.FirstOrDefault(i => i.RecordNum == userId);
            if (user == null)
                return;

            question.VotingUsers.Remove(user);
            question.LikeCount--;
            this.SaveChanges();
        }

        public void Ask(int userId, int subjectId, string questionText)
        {
            Question question = new Question();
            question.OwnerUserId = userId;
            question.OwnerSubjectId = subjectId;
            question.Text = questionText;
            question.InstanceId= DateTime.Now;
            this.Questions.Add(question);
            this.SaveChanges();

            var subject = this.Subjects.Include("Questions").FirstOrDefault(i => i.RecordNum == subjectId);
            if (subject == null) return;
            var questionCount = subject.Questions.Count;
            subject.QuestionCount = questionCount;
            this.SaveChanges();
        }
    }
}
