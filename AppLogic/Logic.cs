using System;
using System.Collections.Generic;
using WMPLib;

namespace AppLogic
{
    public class Question
    {
        public Question(string word)
        {
            Word = word;
            translations = new List<string>();
        }
        public string Word { get; set; }
        public List<string> translations;
    }

    public class QuizQuestion : Question
    {
        public QuizQuestion(string word) : base(word) { }

        public int correctAnswer;
    }

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
            Music = "On";
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
                PlayMusic();
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
            var question = new Question(word)
            {
                translations = answers
            };
            translations.Add(question);
        }

        public static void AddQuizQuestion(List<QuizQuestion> quiz, string word, List<string> answers, int number)
        {
            var quizQuestion = new QuizQuestion(word)
            {
                translations = answers,
                correctAnswer = number,
            };
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
            Random rnd = new Random();
            int index = rnd.Next(quiz.Count);
            QuizQuestion temp = quiz[index];
            QuizQuestion question = new QuizQuestion(temp.Word);
            int answerCount = (numberOfAnswers == 0 ? rnd.Next(2, 6) : numberOfAnswers);

            while (question.translations.Count < answerCount)
            {
                string answer = temp.translations[rnd.Next(5)];
                if (!question.translations.Contains(answer))
                {
                    question.translations.Add(answer);
                    if (answer == temp.translations[temp.correctAnswer - 1])
                    {
                        question.correctAnswer = question.translations.Count;
                    }
                }
            }
            if (!question.translations.Contains(temp.translations[temp.correctAnswer - 1]))
            {
                int correctIndex = rnd.Next(answerCount);
                question.translations[correctIndex] = temp.translations[temp.correctAnswer - 1];
                question.correctAnswer = correctIndex + 1;
            }
            return question;
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
