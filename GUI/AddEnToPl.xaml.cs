using AppLogic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy AddEnToPl.xaml
    /// </summary>
    public partial class AddEnToPl : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;
        int countTranslations;

        public AddEnToPl()
        {
            InitializeComponent();
        }

        public AddEnToPl(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();

            for (int i = 1; i <= 25; i++)
            {
                count.Items.Add(i.ToString());
            }
            count.SelectedItem = "1";
            countTranslations = int.Parse(count.SelectedItem.ToString());
        }

        private void count_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (int.Parse(count.SelectedItem.ToString()) > countTranslations)
            {
                for (int i = countTranslations + 1; i <= int.Parse(count.SelectedItem.ToString()); i++)
                {
                    TextBox box = new TextBox();
                    box.Name = "box" + i.ToString();
                    box.Margin = new Thickness(0, 5, 5, 5);
                    translations.Children.Add(box);
                    box.RegisterName(box.Name, box);
                }
            }
            else
            {
                for (int i = countTranslations; i > int.Parse(count.SelectedItem.ToString()); i--)
                {
                    TextBox box = (TextBox)LogicalTreeHelper.FindLogicalNode(translations, "box" + i.ToString());
                    box.UnregisterName("box" + i.ToString());
                    translations.Children.Remove(box);

                }
            }
            countTranslations = int.Parse(count.SelectedItem.ToString());
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= countTranslations; i++)
            {
                TextBox box = (TextBox)LogicalTreeHelper.FindLogicalNode(translations, "box" + i.ToString());
                list.Add(box.Text);
            }
            Logic.AddTranslation(englishToPolish, word.Text, list);
            MessageBox.Show("Pomyślnie dodano nowe pytanie");
            var mainMenu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(mainMenu);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            var questionMenu = new QuestionMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(questionMenu);
        }
    }
}
