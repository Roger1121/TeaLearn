using AppLogic;
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
    /// Logika interakcji dla klasy TranslationPage.xaml
    /// </summary>
    public partial class TranslationPage : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;
        int questionNumber;
        Question question;
        int points;

        public TranslationPage(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz, int questionNumber = 0, int points = 0)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            this.questionNumber = questionNumber;
            this.points = points;

            if (settings.ExerciseType == "Tłumaczenie z polskiego na angielski" || (settings.ExerciseType== "Mieszany z rosnącym poziomem trudności" && questionNumber < 25))
            {
                question = Logic.GetRandomQuestion(polishToEnglish);
            }
            else if(settings.ExerciseType == "Tłumaczenie z angielskiego na polski" || (settings.ExerciseType == "Mieszany z rosnącym poziomem trudności" && questionNumber < 30))
            {
                question = Logic.GetRandomQuestion(englishToPolish);
            }
            else
            {
                question = Logic.GetRandomQuestion(polishToEnglish, englishToPolish);
            }

            InitializeComponent();

            QuestionText.Text = "Co oznacza słowo " + question.Word + "?";
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (Logic.IsAnswerCorrect(question.translations, answer.Text))
            {
                points++;
            }
            if(settings.TrainingMode == "Test" && questionNumber+1 == settings.QuestionsInTest)
            {
                TestResult testResult = new TestResult(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                NavigationService.Navigate(testResult);
            }
            else if (settings.ExerciseType == "Mieszany")
            {
                if (settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.translations, answer.Text))
                    {
                        MessageBox.Show("Niestety nie, akceptowane odpowiedzi, to: " + string.Join(", ", question.translations), "", MessageBoxButton.OK);
                    }
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
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.translations, answer.Text))
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
                if(settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.translations, answer.Text))
                    {
                        MessageBox.Show("Niestety nie, akceptowane odpowiedzi, to: " + string.Join(", ", question.translations), "", MessageBoxButton.OK);
                    }
                    TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                    NavigationService.Navigate(translationPage);
                }
                else if(settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.translations, answer.Text))
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
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else
                {
                    TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1, points);
                    NavigationService.Navigate(translationPage);
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
