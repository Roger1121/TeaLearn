using AppLogic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy QuestionMenu.xaml
    /// </summary>
    public partial class QuestionMenu : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;

        public QuestionMenu()
        {
            InitializeComponent();
        }

        public QuestionMenu(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();
        }

        private void AddPlEn(object sender, RoutedEventArgs e)
        {
            var addPlToEn = new AddPlToEn(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(addPlToEn);
        }

        private void AddEnPl(object sender, RoutedEventArgs e)
        {
            var addEnToPl = new AddEnToPl(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(addEnToPl);
        }

        private void AddQuiz(object sender, RoutedEventArgs e)
        {
            var addQuiz = new AddQuiz(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(addQuiz);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(mainMenu);
        }
    }
}
