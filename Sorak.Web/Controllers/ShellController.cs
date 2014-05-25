using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Breeze.WebApi;
using Sorak.Model.ClientModel;
using Sorak.Web.Utils;
using Breeze.WebApi.EF;
using Sorak.Model;

namespace Sorak.Web.Controllers
{
    [BreezeController]
    public class ShellController : ApiController
    {
        readonly EFContextProvider<SorakEntities> _contextProvider =  new EFContextProvider<SorakEntities>();

        [HttpGet]
        public string Metadata()
        {
            var metadata = _contextProvider.Metadata();

            return metadata;
        }

        [HttpGet]
        public SorakUser UserInformation()
        {
            var user = GetUser();

            return user;
        }

        public SorakUser GetUser()
        {
            SorakUser videoUser;

            try
            {
                videoUser = new SorakUser(this.User, WebUtilities.ClientIP, WebUtilities.ClientMachineName);

                var dbUser = _contextProvider.Context.GetExistingUser(videoUser.EmailAddress);

                if (dbUser != null)
                {
                    videoUser.RecordNum = dbUser.RecordNum;
                    _contextProvider.Context.UpdateLastLoginTime(dbUser);
                }
                else
                {
                    videoUser.RecordNum = _contextProvider.Context.InsertNewUser(videoUser);
                }

                videoUser.UpdatePhotoUri();

                //Logger.WriteLog("Kullanıcı bilgileri başarıyla alındı:" + videoUser.WindowsUserName);

                return videoUser;
            }
            catch (Exception e)
            {/*
                Logger.WriteLog("Kullanıcı bilgileri alırken hata oluştu");
                Logger.WriteLog(e.ToString());s
                Logger.WriteError(e);
            */
                return null;
            }
            return null;
        }

        [HttpGet]
        public IQueryable<SubjectSummary> GetSubjects()
        {
            var subjectsArr =  _contextProvider.Context.Subjects.Where(i=>i.StatusCode =="A").ToArray();

            return subjectsArr.Select(i => new SubjectSummary(i)).AsQueryable();
        }

        [HttpGet]
        public IQueryable<QuestionSummary> GetQuestions(int subjectId)
        {
            SorakUser user = GetUser();
            
            if (user == null)
                return null;

            var questionsArr = _contextProvider.Context.Questions.Include("OwnerUser").Where(i => i.OwnerSubjectId == subjectId).OrderBy(i => i.RecordNum).OrderByDescending(i => i.LikeCount).ToArray();

            var questions = questionsArr.Select(i => new QuestionSummary(i, IsLiked(i.RecordNum, user.RecordNum), GetPhotoUrl(i.OwnerUser.WindowsUsername), i.OwnerUser.EmployeeName)).AsQueryable();
            
            return questions;
        }

        private bool IsLiked(int questionId, int userId)
        {
            var votingUser = this._contextProvider.Context.Questions.Include("VotingUsers").FirstOrDefault(i => i.RecordNum == questionId).VotingUsers.FirstOrDefault(i => i.RecordNum == userId);

            return votingUser != null;
        }

        private string GetPhotoUrl(string windowsUsername)
        {
            var url = PhotoUtilities.GetPhotoUriByWindowsName(windowsUsername); ;

            return url;
        }

        [HttpGet]
        public int Like(int questionId)
        {
            SorakUser user = GetUser();
            
            if (user == null)
                return -1;

            try
            {
                _contextProvider.Context.Like(user.RecordNum, questionId);
                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }
            
            return -1;
        }

        [HttpGet]
        public int Unlike(int questionId)
        {
            SorakUser user = GetUser();

            if (user == null)
                return -1;

            try
            {
                _contextProvider.Context.Unlike(user.RecordNum, questionId);
                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }

            return -1;
        }

        [HttpGet]
        public int Ask(int subjectId, string questionText)
        {
            SorakUser user = GetUser();

            if (user == null)
                return -1;

            try
            {
                _contextProvider.Context.Ask(user.RecordNum, subjectId, questionText);
                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }

            return 0;
        }
    }

}