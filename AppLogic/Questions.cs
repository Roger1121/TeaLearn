using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{
    public interface IQuestion
    {
        int GetPoints();
        string GetWord();
        void SetWord(string w);
        List<string> GetTranslations();
        void AddTranslation(string t);
        void SetTranslation(string t, int idx);
        void ClearTranslations();
        int GetCorrectAnswer();
        void SetCorrectAnswer(int c);
        MementoQuestion Save(int userAnswer);
        MementoQuestion Save(string userAnswer);
        void Restore(MementoQuestion memento);
    }

    public class Question : IQuestion
    {
        private string word;
        private List<string> translations;
        public Question(string w)
        {
            word = w;
            translations = new List<string>();
        }
        public string GetWord()
        {
            return word;
        }
        public void SetWord(string w)
        {
            word = w;
        }
        public List<string> GetTranslations()
        {
            return translations;
        }
        public void AddTranslation(string t)
        {
            translations.Add(t);
        }
        public void SetTranslation(string t, int idx)
        {
            translations[idx] = t;
        }
        public void ClearTranslations()
        {
            translations = null;
        }
        public int GetPoints()
        {
            return 1;
        }
        public virtual int GetCorrectAnswer()
        {
            return -1;
        }
        public virtual void SetCorrectAnswer(int c)
        {
            
        }
        public MementoQuestion Save(int userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(this.word, this.translations, userAnswer, GetCorrectAnswer());

            return memento;
        }

        public MementoQuestion Save(string userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(this.word, this.translations, userAnswer);

            return memento;
        }

        public void Restore(MementoQuestion memento)
        {
            IQuestion q = memento.getStateQuestion();
            this.word = q.GetWord();
            this.translations = q.GetTranslations();
        }
    }

    public class QuizQuestion : Question
    {
        public QuizQuestion(string word) : base(word) { }

        private int correctAnswer;
        public override int GetCorrectAnswer()
        {
            return correctAnswer;
        }
        public override void SetCorrectAnswer(int c)
        {
            correctAnswer = c;
        }

    }
    public abstract class QuestionPoints : IQuestion
    {
        public abstract string GetWord();
        public abstract void SetWord(string w);
        public abstract int GetPoints();
        public abstract List<string> GetTranslations();
        public abstract void AddTranslation(string t);
        public abstract void SetTranslation(string t, int idx);
        public abstract void ClearTranslations();
        public abstract int GetCorrectAnswer();
        public abstract void SetCorrectAnswer(int c);
        public abstract MementoQuestion Save(int userAnswer);
        public abstract MementoQuestion Save(string userAnswer);
        public abstract void Restore(MementoQuestion memento);
    }

    public class QuestionEasy : QuestionPoints
    {
        private IQuestion question;
        public QuestionEasy(IQuestion q)
        {
            question = q;
        }
        public override int GetPoints()
        {
            return question.GetPoints() * 1;
        }
        public override string GetWord()
        {
            return question.GetWord();
        }
        public override void SetWord(string w)
        {
            question.SetWord(w);
        }
        public override List<string> GetTranslations()
        {
            return question.GetTranslations();
        }
        public override void AddTranslation(string t)
        {
            question.AddTranslation(t);
        }
        public override void SetTranslation(string t, int idx)
        {
            question.SetTranslation(t, idx);
        }
        public override void ClearTranslations()
        {
            question.ClearTranslations();
        }
        public override int GetCorrectAnswer()
        {
            return question.GetCorrectAnswer();
        }
        public override void SetCorrectAnswer(int c)
        {
            question.SetCorrectAnswer(c);
        }
        public override MementoQuestion Save(int userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer, GetCorrectAnswer());

            return memento;
        }
        public override MementoQuestion Save(string userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer);

            return memento;
        }

        public override void Restore(MementoQuestion memento)
        {
            IQuestion q = memento.getStateQuestion();
            question.SetWord(q.GetWord());
            question.ClearTranslations();
            List<string> t = q.GetTranslations();
            foreach (var item in t)
            {
                question.AddTranslation(item);

            }
        }

    }

    public class QuestionMedium : QuestionPoints
    {
        private IQuestion question;
        public QuestionMedium(IQuestion q)
        {
            question = q;
        }
        public override int GetPoints()
        {
            return question.GetPoints() * 2;
        }
        public override string GetWord()
        {
            return question.GetWord();
        }
        public override void SetWord(string w)
        {
            question.SetWord(w);
        }
        public override List<string> GetTranslations()
        {
            return question.GetTranslations();
        }
        public override void AddTranslation(string t)
        {
            question.AddTranslation(t);
        }
        public override void SetTranslation(string t, int idx)
        {
            question.SetTranslation(t, idx);
        }
        public override void ClearTranslations()
        {
            question.ClearTranslations();
        }
        public override int GetCorrectAnswer()
        {
            return question.GetCorrectAnswer();
        }
        public override void SetCorrectAnswer(int c)
        {
            question.SetCorrectAnswer(c);
        }
        public override MementoQuestion Save(int userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer, GetCorrectAnswer());

            return memento;
        }
        public override MementoQuestion Save(string userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer);

            return memento;
        }
        public override void Restore(MementoQuestion memento)
        {
            IQuestion q = memento.getStateQuestion();
            question.SetWord(q.GetWord());
            question.ClearTranslations();
            List<string> t = q.GetTranslations();
            foreach (var item in t)
            {
                question.AddTranslation(item);

            }
        }
    }

    public class QuestionHard : QuestionPoints
    {
        private IQuestion question;
        public QuestionHard(IQuestion q)
        {
            question = q;
        }
        public override int GetPoints()
        {
            return question.GetPoints() * 3;
        }
        public override string GetWord()
        {
            return question.GetWord();
        }
        public override void SetWord(string w)
        {
            question.SetWord(w);
        }
        public override List<string> GetTranslations()
        {
            return question.GetTranslations();
        }
        public override void AddTranslation(string t)
        {
            question.AddTranslation(t);
        }
        public override void SetTranslation(string t, int idx)
        {
            question.SetTranslation(t, idx);
        }
        public override void ClearTranslations()
        {
            question.ClearTranslations();
        }
        public override int GetCorrectAnswer()
        {
            return question.GetCorrectAnswer();
        }
        public override void SetCorrectAnswer(int c)
        {
            question.SetCorrectAnswer(c);
        }
        public override MementoQuestion Save(int userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer, GetCorrectAnswer());

            return memento;
        }
        public override MementoQuestion Save(string userAnswer)
        {
            MementoQuestion memento = new MementoQuestion(GetWord(), GetTranslations(), userAnswer);

            return memento;
        }
        public override void Restore(MementoQuestion memento)
        {
            IQuestion q = memento.getStateQuestion();
            question.SetWord(q.GetWord());
            question.ClearTranslations();
            List<string> t = q.GetTranslations();
            foreach (var item in t)
            {
                question.AddTranslation(item);

            }
        }
    }
}
