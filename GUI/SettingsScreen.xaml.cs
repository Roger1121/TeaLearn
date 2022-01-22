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
    /// Logika interakcji dla klasy SettingsScreen.xaml
    /// </summary>
    public partial class SettingsScreen : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;
        public SettingsScreen()
        {
            InitializeComponent();
        }
        public SettingsScreen(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();

            boxMusic.Items.Add("On");
            boxMusic.Items.Add("Off");
            boxMusic.SelectedItem = settings.Music.ToString();

            boxMode.Items.Add("Trening");
            boxMode.Items.Add("Nauka");
            boxMode.Items.Add("Test");
            boxMode.SelectedItem = settings.TrainingMode.ToString();

            boxType.Items.Add("Quiz");
            boxType.Items.Add("Tłumaczenie z polskiego na angielski");
            boxType.Items.Add("Tłumaczenie z angielskiego na polski");
            boxType.Items.Add("Tłumaczenie - mieszany");
            boxType.Items.Add("Mieszany");
            boxType.Items.Add("Mieszany z rosnącym poziomem trudności");
            boxType.SelectedItem = settings.ExerciseType.ToString();

            for(int i=3; i<=99; i++)
            {
                boxQuestions.Items.Add(i.ToString());
            }
            boxQuestions.SelectedItem = settings.QuestionsInTest.ToString();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(mainMenu);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            settings.Music = boxMusic.SelectedItem.ToString();
            if (settings.Music == "Off")
            {
                Logic.StopMusic();
            }
            else
            {
                Logic.PlayMusic();
            }
            Logic.SetExerciseType(boxType.SelectedItem.ToString(), settings);
            Logic.SetTrainingMode(boxMode.SelectedItem.ToString(), settings);
            Logic.SetNumberOfQuestions(int.Parse(boxQuestions.SelectedItem.ToString()), settings);
            MessageBox.Show("Zmiany zostały zapisane tymczasowo. Aby zapisać zmiany na stałe, uruchom ponownie program.", "", MessageBoxButton.OK);
        }
    }
}
