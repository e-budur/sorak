using Sorak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sorak.Model.ClientModel
{
    public class QuestionSummary
    {

        public QuestionSummary(Question question, bool isLiked, string ownerPhotoUrl, string ownerFullname)
        {
            this.Id = question.RecordNum;
            this.Text = question.Text;
            this.LikeCount = question.LikeCount;
            this.IsLiked = isLiked;
            this.OwnerPhotoUrl = ownerPhotoUrl;
            this.OwnerFullname = ownerFullname;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public string OwnerPhotoUrl { get; set; }
        public string OwnerFullname { get; set; }
    }
}
