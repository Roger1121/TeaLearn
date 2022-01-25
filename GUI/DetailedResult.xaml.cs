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
    /// Logika interakcji dla klasy DetailedResult.xaml
    /// </summary>
    public partial class DetailedResult : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;

        public DetailedResult(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();

            TextBlock blockTitle = new TextBlock();
            blockTitle.Name = "title";
            string text1 = "Słowo   podane tłumaczenie  poprawność odpowiedzi";
            blockTitle.Text = text1;
            blockTitle.Foreground = Brushes.Peru;
            blockTitle.FontSize = 22;
            questions.Children.Add(blockTitle);
            blockTitle.RegisterName(blockTitle.Name, blockTitle);
            int i = 1;
            foreach (var q in Logic.history)
            {
                TextBlock block = new TextBlock();
                block.Name = "question" + (i++).ToString();
                block.Foreground = Brushes.Peru;
                block.FontSize = 22;
                
                string text = q.getStateQuestion().GetWord() + "              ";
                if (!string.IsNullOrEmpty(q.getStateTextAnswer()))
                {
                    text += q.getStateTextAnswer() + (Logic.IsAnswerCorrect(q.getStateQuestion().GetTranslations(),q.getStateTextAnswer()) ? "      <- poprawna odpowiedź" : "      <-nieprawidłowa odpowiedź");
                }
                else
                {
                    text += q.getStateQuestion().GetTranslations()[q.getStateUserAnswer() - 1] +"              "+ q.getStateQuestion().GetTranslations()[q.GetCorrectAnswer() - 1];
                }
                block.Text = text;
                questions.Children.Add(block);
                block.RegisterName(block.Name, block);
            }
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(menu);
        }
    }
}
