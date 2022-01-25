using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{
    public class MementoQuestion
    {
        //czy zap. pop odp?
        private string word;
        private List<string> translations;
        private int userAnswer;
        private string textAnswer;

        public MementoQuestion(string word, List<string> translations, int userAnswer)
        {
            this.word = word;
            this.translations = translations;
            this.userAnswer = userAnswer;
        }

        public MementoQuestion(IQuestion question, int userAnswer)
        {
            this.word = question.GetWord();
            this.translations = question.GetTranslations();
            this.userAnswer = userAnswer;
        }
        public MementoQuestion(IQuestion question, string textAnswer)
        {
            this.word = question.GetWord();
            this.translations = question.GetTranslations();
            this.textAnswer = textAnswer;
        }

        public MementoQuestion(MementoQuestion memento)
        {
            this.word = memento.word;
            this.translations = memento.translations;
            this.userAnswer = memento.userAnswer;
        }

        public Question getStateQuestion()
        {
            Question question = new Question(word);
            foreach (var item in translations)
            {
                question.AddTranslation(item);
            }
            return question;
        }

        public int getStateUserAnswer()
        {
            return userAnswer;
        }

        public string getStateTextAnswer()
        {
            return textAnswer;
        }

    }
}
