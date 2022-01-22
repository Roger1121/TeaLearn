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
    /// Logika interakcji dla klasy TestResult.xaml
    /// </summary>
    public partial class TestResult : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;
        int questionNumber;
        int points;

        public TestResult(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz, int questionNumber = 0, int points = 0)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            this.questionNumber = questionNumber;
            this.points = points;

            InitializeComponent();

            Result.Text = "Uzyskano " + points.ToString() + "/" + settings.QuestionsInTest.ToString() + " punktów";
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(menu);
        }

        private void Retry(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            switch (settings.ExerciseType)
            {
                case "Quiz":
                    QuizPage quizPage = new QuizPage();
                    NavigationService.Navigate(quizPage);
                    break;
                case "Tłumaczenie z polskiego na angielski":
                    TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz);
                    NavigationService.Navigate(translationPage);
                    break;
                case "Tłumaczenie z angielskiego na polski":
                    TranslationPage translationPage2 = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz);
                    NavigationService.Navigate(translationPage2);
                    break;
                case "Tłumaczenie - mieszany":
                    TranslationPage translationPage3 = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz);
                    NavigationService.Navigate(translationPage3);
                    break;
                case "Mieszany":
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPageMixed = new QuizPage(settings, polishToEnglish, englishToPolish, quiz);
                        NavigationService.Navigate(quizPageMixed);
                    }
                    else
                    {
                        TranslationPage translationPage4 = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz);
                        NavigationService.Navigate(translationPage4);
                    }
                    break;
                case "Mieszany z rosnącym poziomem trudności":
                    QuizPage quizPageAscending = new QuizPage(settings, polishToEnglish, englishToPolish, quiz);
                    NavigationService.Navigate(quizPageAscending);
                    break;
            }
        }
    }
}
