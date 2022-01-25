using System;
using System.Collections;
using System.Collections.Generic;
using WMPLib;

namespace AppLogic
{
    public class Settings
    {
        private static Settings instance;
        public static Settings GetSettings()
        {
            if (instance == null)
                instance = new Settings();
            return instance;
        }

        private Settings()
        {
            Music = "Off";
            TrainingMode = "Quiz";
            ExerciseType = "Trening";
            QuestionsInTest = 10;
        }

        public string Music { get; set; }
        public string TrainingMode { get; set; }
        public string ExerciseType { get; set; }
        public int QuestionsInTest { get; set; }
    }
    public static class Logic
    {
        private static int points;
        private static int maxPoints;
        public static List<MementoQuestion> history = new List<MementoQuestion>();

        public static void ClearHistory()
        {
            history = null;
        }

        private static QuestionIterator questionIterator = new QuestionIterator();

        public static void OnStart(List<QuizQuestion> quiz)
        {
            List<QuizQuestion> questions = quiz;
            questions.Shuffle();
            questionIterator = new QuestionIterator(questions);
        }
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public class MyIterator : IEnumerable<QuizQuestion>
        {
            private List<QuizQuestion> questionsQuiz;

            public MyIterator(List<QuizQuestion> quiz)
            {
                this.questionsQuiz = quiz;
            }

            public IEnumerator<QuizQuestion> GetEnumerator()
            {
                foreach (var item in questionsQuiz)
                {
                    yield return item;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public static void AddPoints(int p)
        {
            points += p;
        }

        public static void AddMaxPoints(int p)
        {
            maxPoints += p;
        }

        public static int GetPoints()
        {
            return points;
        }

        public static int GetMaxPoints()
        {
            return maxPoints;
        }

        public static void ClearPoints()
        {
            points = 0;
        }

        public static void ClearMaxPoints()
        {
            maxPoints = 0;
        }

        private static readonly WindowsMediaPlayer player = new WindowsMediaPlayer()
        {
            URL = @".\Assets\Music\WithoutYou.mp3"
        };
        public static void PlayMusic()
        {
            player.controls.play();
            player.settings.setMode("loop", true);
        }
        public static void SaveSystemState(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            Files.SaveQuestionsToXML(polishToEnglish, "PolishToEnglish.xml");
            Files.SaveQuestionsToXML(englishToPolish, "EnglishToPolish.xml");
            Files.SaveQuizQuestionsToXML(quiz, "Quiz.xml");
            Files.SaveSettingsToXML(settings);
        }

        public static string GetInfo()
        {
            return Files.GetInfo();
        }

        public static void LoadSystemState(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            Files.GetSettingsFromXML(settings);
            Files.GetQuestionsFromXML(polishToEnglish, "PolishToEnglish.xml");
            Files.GetQuestionsFromXML(englishToPolish, "EnglishToPolish.xml");
            Files.GetQuizQuestionsFromXML(quiz, "Quiz.xml");
            if (settings.Music == "On")
            {
                PlayMusic();
            }
        }

        public static void StopMusic()
        {
            player.controls.stop();
        }

        public static void SetExerciseType(string v, Settings settings)
        {
            settings.ExerciseType = v;
        }

        public static void SetTrainingMode(string mode, Settings settings)
        {
            settings.TrainingMode = mode;
        }

        public static void SetNumberOfQuestions(int number, Settings settings)
        {
            settings.QuestionsInTest = number;
        }

        public static void AddTranslation(List<Question> translations, string word, List<string> answers)
        {
            var question = new Question(word);
            foreach(var a in answers)
            {
                question.AddTranslation(a);
            }
            translations.Add(question);
        }

        public static void AddQuizQuestion(List<QuizQuestion> quiz, string word, List<string> answers, int number)
        {
            var quizQuestion = new QuizQuestion(word);
            foreach (var a in answers)
            {
                quizQuestion.AddTranslation(a);
            }
            quizQuestion.SetCorrectAnswer(number);
            quiz.Add(quizQuestion);
        }

        public static bool IsAnswerCorrect(int correctAnswer, int choice)
        {
            return (choice == correctAnswer);
        }

        public static bool IsAnswerCorrect(List<string> correctAnswers, string choice)
        {
            return (correctAnswers.Contains(choice));
        }

        public static QuizQuestion GetRandomQuizQuestion(List<QuizQuestion> quiz, int numberOfAnswers = 0)
        {
            if (questionIterator.hasNext())
            {
                Random rnd = new Random();
                QuizQuestion temp = questionIterator.next();
                QuizQuestion question = new QuizQuestion(temp.GetWord());
                int answerCount = (numberOfAnswers == 0 ? rnd.Next(2, 6) : numberOfAnswers);
                while (question.GetTranslations().Count < answerCount)
                {
                    string answer = temp.GetTranslations()[rnd.Next(5)];
                    if (!question.GetTranslations().Contains(answer))
                    {
                        question.AddTranslation(answer);
                        if (answer == temp.GetTranslations()[temp.GetCorrectAnswer() - 1])
                        {
                            question.SetCorrectAnswer(question.GetTranslations().Count);
                        }
                    }
                }
                if (!question.GetTranslations().Contains(temp.GetTranslations()[temp.GetCorrectAnswer() - 1]))
                {
                    int correctIndex = rnd.Next(answerCount);
                    question.SetTranslation(temp.GetTranslations()[temp.GetCorrectAnswer() - 1], correctIndex);
                    question.SetCorrectAnswer(correctIndex + 1);
                }
                return question;
            }
            else
            {
                OnStart(quiz);
                return GetRandomQuizQuestion(quiz, numberOfAnswers);
            }
        }

        public static Question GetRandomQuestion(List<Question> list, List<Question> list2 = null)
        {
            Question question;
            Random rnd = new Random();
            if (list2 != null)
            {
                if (rnd.Next(2) == 0)
                {
                    question = list[rnd.Next(list.Count)];
                }
                else
                {
                    question = list2[rnd.Next(list2.Count)];
                }
            }
            else
            {
                question = list[rnd.Next(list.Count)];
            }
            return question;
        }

        
    }
}
