using Sorak.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sorak.Model.ClientModel
{
    public class SubjectSummary
    {
        public SubjectSummary(Subject subject)
        {
            this.Id = subject.RecordNum;
            this.Text = subject.Text;
            this.QuestionCount = subject.QuestionCount;
            this.Date = subject.EventDate;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionCount { get; set; }
        public string Date { get; set; }
    }
}
