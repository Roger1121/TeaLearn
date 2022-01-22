﻿using AppLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        QuizQuestion question;
        int points;

        public QuizPage()
        {
            InitializeComponent();
        }

        public QuizPage(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz, int questionNumber = 0, int points = 0)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            this.questionNumber = questionNumber;
            this.points = points;
            if(settings.ExerciseType == "Mieszany z rosnącym poziomem trudności")
            {
                question = Logic.GetRandomQuizQuestion(quiz, (questionNumber / 5)+2);
            }
            else
            {
                question = Logic.GetRandomQuizQuestion(quiz);
            }

            InitializeComponent();

            QuestionText.Text = "Co oznacza słowo "+question.Word+"?";

            for(int i=1; i<=question.translations.Count; i++)
            {
                Button button = new Button();
                button.Name = "answer" + i.ToString();
                button.Content = question.translations[i-1];
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
            if(Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
            {
                points++;
            }
            if(settings.TrainingMode == "Test" && questionNumber+1 == settings.QuestionsInTest)
            {
                TestResult testResult = new TestResult(settings, polishToEnglish, englishToPolish, quiz, questionNumber+1, points);
                NavigationService.Navigate(testResult);
            }
            else if(settings.ExerciseType == "Quiz")
            {
                if(settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.translations[question.correctAnswer-1], "", MessageBoxButton.OK);
                    }
                    QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                    NavigationService.Navigate(quizPage);
                }
                else if(settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
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
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(quizPage);
                    }
                }
                else
                {
                    QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                    NavigationService.Navigate(quizPage);
                }
            }
            else if(settings.ExerciseType == "Mieszany")
            {
                if (settings.TrainingMode == "Trening")
                {
                    if(!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.translations[question.correctAnswer], "", MessageBoxButton.OK);
                    }
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber+1, points);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
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
                            QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                            NavigationService.Navigate(quizPage);
                        }
                        else
                        {
                            TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                            NavigationService.Navigate(translationPage);
                        }
                    }
                }
                else
                {
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(translationPage);
                    }
                }
            }
            else
            {
                if (settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
                    {
                        MessageBox.Show("Niestety nie, prawidłowa odpowiedź, to: " + question.translations[question.correctAnswer], "", MessageBoxButton.OK);
                    }
                    if (questionNumber < 20)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.correctAnswer, int.Parse(button.Name.Substring(6, 1))))
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
                            QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                            NavigationService.Navigate(quizPage);
                        }
                        else
                        {
                            TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                            NavigationService.Navigate(translationPage);
                        }
                    }
                }
                else
                {
                    if (questionNumber < 20)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
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