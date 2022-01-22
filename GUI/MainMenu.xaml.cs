using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AppLogic;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        Settings settings;
        List<Question> polishToEnglish = new List<Question>();
        List<Question> englishToPolish = new List<Question>();
        List<QuizQuestion> quiz = new List<QuizQuestion>();
        public MainMenu()
        {
            InitializeComponent();
            settings = Settings.GetSettings();
            Logic.LoadSystemState(settings, polishToEnglish, englishToPolish, quiz);
        }
        public MainMenu(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            InitializeComponent();
        }
        private void StartLearning(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            switch (settings.ExerciseType)
            {
                case "Quiz":
                    QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz);
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

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            var settingsScreen = new SettingsScreen(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(settingsScreen);
        }
        private void PrintAbout(object sender, RoutedEventArgs e)
        {
            var about = new About(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(about);
        }
        private void AddQuestion(object sender, RoutedEventArgs e)
        {  
            var questionMenu = new QuestionMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(questionMenu);
        }
        private void Quit(object sender, RoutedEventArgs e)
        {
            Logic.SaveSystemState(settings, polishToEnglish, englishToPolish, quiz);
            Application.Current.Shutdown();
        }
    }
}
