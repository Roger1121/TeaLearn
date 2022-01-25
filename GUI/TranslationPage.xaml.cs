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
        IQuestion question;

        public TranslationPage(Settings settings, List<Question> polishToEnglish, List<Question> englishToPolish, List<QuizQuestion> quiz, int questionNumber = 0)
        {
            this.settings = settings;
            this.polishToEnglish = polishToEnglish;
            this.englishToPolish = englishToPolish;
            this.quiz = quiz;
            this.questionNumber = questionNumber;

            if(questionNumber == 0)
            {
                Logic.ClearPoints();
                Logic.ClearMaxPoints();
            }

            if (settings.ExerciseType == "Tłumaczenie z polskiego na angielski" || (settings.ExerciseType== "Mieszany z rosnącym poziomem trudności" && questionNumber < 25))
            {
                question = new QuestionHard(Logic.GetRandomQuestion(polishToEnglish));
            }
            else if(settings.ExerciseType == "Tłumaczenie z angielskiego na polski" || (settings.ExerciseType == "Mieszany z rosnącym poziomem trudności" && questionNumber < 30))
            {
                question = new QuestionHard(Logic.GetRandomQuestion(englishToPolish));
            }
            else
            {
                question = new QuestionHard(Logic.GetRandomQuestion(polishToEnglish, englishToPolish));
            }

            InitializeComponent();

            QuestionText.Text = "Co oznacza słowo " + question.GetWord() + "?";
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            Logic.AddMaxPoints(question.GetPoints());
            Logic.history.Add(new MementoQuestion(question, answer.Text));
            if (Logic.IsAnswerCorrect(question.GetTranslations(), answer.Text))
            {
                Logic.AddPoints(question.GetPoints());
            }
            if(settings.TrainingMode == "Test" && questionNumber+1 == settings.QuestionsInTest)
            {
                TestResult testResult = new TestResult(settings, polishToEnglish, englishToPolish, quiz);
                NavigationService.Navigate(testResult);
            }
            else if (settings.ExerciseType == "Mieszany")
            {
                if (settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.GetTranslations(), answer.Text))
                    {
                        MessageBox.Show("Niestety nie, akceptowane odpowiedzi, to: " + string.Join(", ", question.GetTranslations()), "", MessageBoxButton.OK);
                    }
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else if (settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.GetTranslations(), answer.Text))
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
                            QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(quizPage);
                        }
                        else
                        {
                            TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                            NavigationService.Navigate(translationPage);
                        }
                    }
                }
                else
                {
                    if (rnd.Next(6) <= 3)
                    {
                        QuizPage quizPage = new QuizPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(quizPage);
                    }
                    else
                    {
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
            }
            else
            {
                if(settings.TrainingMode == "Trening")
                {
                    if (!Logic.IsAnswerCorrect(question.GetTranslations(), answer.Text))
                    {
                        MessageBox.Show("Niestety nie, akceptowane odpowiedzi, to: " + string.Join(", ", question.GetTranslations()), "", MessageBoxButton.OK);
                    }
                    TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                    NavigationService.Navigate(translationPage);
                }
                else if(settings.TrainingMode == "Nauka")
                {
                    if (!Logic.IsAnswerCorrect(question.GetTranslations(), answer.Text))
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
                        TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
                        NavigationService.Navigate(translationPage);
                    }
                }
                else
                {
                    TranslationPage translationPage = new TranslationPage(settings, polishToEnglish, englishToPolish, quiz, questionNumber + 1);
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
