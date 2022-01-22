using AppLogic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy AddQuiz.xaml
    /// </summary>
    public partial class AddQuiz : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;

        public AddQuiz()
        {
            InitializeComponent();
        }

        public AddQuiz(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();

            for (int i = 1; i <= 5; i++)
            {
                correctAnswer.Items.Add(i.ToString());
            }
            correctAnswer.SelectedItem = "1";
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Logic.AddQuizQuestion(quiz, question.Text, new List<string>()
            {
                answer1.Text, answer2.Text, answer3.Text, answer4.Text, answer5.Text
            }, int.Parse(correctAnswer.SelectedValue.ToString()));
            MessageBox.Show("Pomyślnie dodano nowe pytanie");
            var mainMenu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(mainMenu);
        }

        private void Quit(object sender, System.Windows.RoutedEventArgs e)
        {
            var questionMenu = new QuestionMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(questionMenu);
        }
    }
}
