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
            Grid grid = SetLine("Słowo", "Odpowiedź użytkownika", "Odpowiedź/Komentarz");
            questions.Children.Add(grid);
            
            foreach (var q in Logic.history)
            {
                if (!string.IsNullOrEmpty(q.getStateTextAnswer()))
                {
                    grid = SetLine(q.getStateQuestion().GetWord(), q.getStateTextAnswer(), (Logic.IsAnswerCorrect(q.getStateQuestion().GetTranslations(), q.getStateTextAnswer()) ? "poprawna odpowiedź" : "nieprawidłowa odpowiedź"));
                }
                else
                {
                    grid = SetLine(q.getStateQuestion().GetWord(), q.getStateQuestion().GetTranslations()[q.getStateUserAnswer() - 1], q.getStateQuestion().GetTranslations()[q.GetCorrectAnswer() - 1]);
                }
                questions.Children.Add(grid);
            }
        }
        private Grid SetLine(string text1, string text2, string text3)
        {
            Grid grid = new Grid();
            grid.Height = 50;
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            gridCol1.Width = new GridLength(1, GridUnitType.Star);
            gridCol2.Width = new GridLength(1, GridUnitType.Star);
            gridCol3.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(gridCol1);
            grid.ColumnDefinitions.Add(gridCol2);
            grid.ColumnDefinitions.Add(gridCol3);
            TextBlock block1 = new TextBlock();
            TextBlock block2 = new TextBlock();
            TextBlock block3 = new TextBlock();
            block1.Text = text1;
            block2.Text = text2;
            block3.Text = text3;
            block1.Foreground = Brushes.Peru;
            block2.Foreground = Brushes.Peru;
            block3.Foreground = Brushes.Peru;
            block1.FontSize = 22;
            block2.FontSize = 22;
            block3.FontSize = 22;
            Grid.SetColumn(block1, 0);
            Grid.SetColumn(block2, 1);
            Grid.SetColumn(block3, 2);
            grid.Children.Add(block1);
            grid.Children.Add(block2);
            grid.Children.Add(block3);
            return grid;
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            Logic.ClearHistory();
            NavigationService.Navigate(menu);
        }
    }
}
