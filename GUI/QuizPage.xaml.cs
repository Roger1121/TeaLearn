using AppLogic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;
        int questionNumber;
        IQuestion question;

        public QuizPage()
        {
            InitializeComponent();
        }

        public QuizPage(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz, int questionNumber = 0)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            this.questionNumber = questionNumber;

            if (questionNumber == 0)
            {
                Logic.ClearPoints();
                Logic.ClearMaxPoints();
            }

            if (settings.ExerciseType == "Mieszany z rosnącym poziomem trudności")
            {
                int answerCount = (questionNumber / 5) + 2;
                if (answerCount < 4)
                {
                    question = new QuestionEasy(Logic.GetRandomQuizQuestion(quiz, answerCount));
                }
                else
                {
                    question = new QuestionMedium(Logic.GetRandomQuizQuestion(quiz, answerCount));
                }
            }
            else
            {
                question = Logic.GetRandomQuizQuestion(quiz);
                if (question.GetTranslations().Count < 4)
                {
                    question = new QuestionEasy(question);
                }
                else
                {
                    question = new QuestionMedium(question);
                }
            }

            InitializeComponent();

            QuestionText.Text = "Co oznacza słowo "+question.GetWord()+"?";

            for(int i=1; i<=question.GetTranslations().Count; i++)
            {
                Button button = new Button();
                button.Name = "answer" + i.ToString();
                button.Content = question.GetTranslations()[i-1];
                button.Click += new RoutedEventHandler(button_Click);
                button.Margin = new Thickness(10, 10, 10, 10);
                
                Answers.Children.Add(button);
                Answers.RegisterName(button.Name, button);
            }
        }

        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Random rnd = new Random();
            Logic.AddMaxPoints(question.GetPoints());
            if (Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
            {
                Logic.AddPoints(question.GetPoints());
            }
            if(settings.TrainingMode == "Test" && questionNumber+1 == settings.QuestionsInTest)
            {
                TestResult testResult = new TestResult(settings, polishToEnglish, englishToPolish, quiz);
                NavigationService.Navigate(testResult);
            }
            else if(settings.ExerciseType == "Quiz")
            {
                if(settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.GetTranslations()[question.GetCorrectAnswer() - 1], "", MessageBoxButton.OK);
                    }
                    QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                    NavigationService.Navigate(quizPage);
                }
                else if(settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBoxResult result = MessageBox.Show("Błędna odpowiedź, czy chcesz spróbować ponownie?", "", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                break;
                            case MessageBoxResult.No:
                                MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
                                NavigationService.Navigate(menu);
                                break;

                        }
                    }
                    else
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                }
                else
                {
                    QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                    NavigationService.Navigate(quizPage);
                }
            }
            else if(settings.ExerciseType == "Mieszany")
            {
                if (settings.TrainingMode == "Trening")
                {
                    if(!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.GetTranslations()[question.GetCorrectAnswer()], "", MessageBoxButton.OK);
                    }
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber+1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBoxResult result = MessageBox.Show("Błędna odpowiedź, czy chcesz spróbować ponownie?", "", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                break;
                            case MessageBoxResult.No:
                                MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
                                NavigationService.Navigate(menu);
                                break;

                        }
                    }
                    else
                    {
                        if (rnd.Next(6) <= 3)
                        {
                            QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(quizPage);
                        }
                        else
                        {
                            TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(translationPage);
                        }
                    }
                }
                else
                {
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
            }
            else
            {
                if (settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.GetTranslations()[question.GetCorrectAnswer()], "", MessageBoxButton.OK);
                    }
                    if (questionNumber < 20)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.GetCorrectAnswer(), int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBoxResult result = MessageBox.Show("Błędna odpowiedź, czy chcesz spróbować ponownie?", "", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                break;
                            case MessageBoxResult.No:
                                MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
                                NavigationService.Navigate(menu);
                                break;
                        }
                    }
                    else
                    {
                        if (questionNumber < 20)
                        {
                            QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(quizPage);
                        }
                        else
                        {
                            TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(translationPage);
                        }
                    }
                }
                else
                {
                    if (questionNumber < 20)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
            }
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(menu);
        }
    }
}
