using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{
    public class MementoQuestion
    {
        private string word;
        private List<string> translations;
        private int userAnswer;
        private string textAnswer;
        private int correctAnswer;

        public MementoQuestion(string word, List<string> translations, int answer, int correctAnswer)
        {
            this.word = word;
            this.translations = translations;
            this.userAnswer = answer;
            this.correctAnswer = correctAnswer;
        }
        public MementoQuestion(string word, List<string> translations, string answer)
        {
            this.word = word;
            this.translations = translations;
            this.textAnswer = answer;
        }

        public MementoQuestion(IQuestion question, int userAnswer)
        {
            this.word = question.GetWord();
            this.translations = question.GetTranslations();
            this.userAnswer = userAnswer;
            this.correctAnswer = question.GetCorrectAnswer();
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
            this.textAnswer = memento.textAnswer;
            this.correctAnswer = memento.correctAnswer;
        }

        public IQuestion getStateQuestion()
        {
            IQuestion question;
            if (textAnswer == null)
            {
                question = new Question(word);
            }
            else
            {
                question = new QuizQuestion(word);
                question.SetCorrectAnswer(correctAnswer);
            }
            foreach (var item in translations)
            {
                question.AddTranslation(item);
            }
            return question;
        }

        public void SetUserAnswer(int answer)
        {
            userAnswer = answer;
        }
        public void SetUserAnswer(string answer)
        {
            textAnswer = answer;
        }

        public int getStateUserAnswer()
        {
            return userAnswer;
        }

        public string getStateTextAnswer()
        {
            return textAnswer;
        }
        public int GetCorrectAnswer()
        {
            return correctAnswer;
        }

    }
}
