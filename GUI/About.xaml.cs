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
    /// Logika interakcji dla klasy About.xaml
    /// </summary>
    public partial class About : Page
    {
        Settings settings;
        List<Question> polishToEnglish;
        List<Question> englishToPolish;
        List<QuizQuestion> quiz;

        public About()
        {
            InitializeComponent();
        }

        public About(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;

            InitializeComponent();
            info.Text = Logic.GetInfo();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu(settings, polishToEnglish, englishToPolish, quiz);
            NavigationService.Navigate(mainMenu);
        }
    }
}
