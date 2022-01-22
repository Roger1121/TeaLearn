using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AppLogic
{
    static class Files
    {

        internal static void GetSettingsFromXML(Settings settings)
        {
            using XmlReader reader = XmlReader.Create(@".\Assets\XmlFiles\Settings.xml");
            {
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                        {
                            settings.Music = reader.GetAttribute("music");
                            int.TryParse(reader.GetAttribute("questionsInTest"), out int questions);
                            if (questions != 0)
                                settings.QuestionsInTest = questions;
                            settings.TrainingMode = reader.GetAttribute("trainingMode");
                            settings.ExerciseType = reader.GetAttribute("exerciseType");
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        internal static void SaveSettingsToXML(Settings settings)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            using XmlWriter writer = XmlWriter.Create(@".\Assets\XmlFiles\Settings.xml", xmlWriterSettings);
            {
                writer.WriteStartElement("Settings");
                writer.WriteAttributeString("music", settings.Music);
                writer.WriteAttributeString("questionsInTest", settings.QuestionsInTest.ToString());
                writer.WriteAttributeString("trainingMode", settings.TrainingMode);
                writer.WriteAttributeString("exerciseType", settings.ExerciseType);
                writer.WriteEndElement();
            }
        }

        internal static void SaveQuizQuestionsToXML(List<QuizQuestion> questions, string filename)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using XmlWriter writer = XmlWriter.Create(@".\Assets\XmlFiles\" + filename, xmlWriterSettings);
            {
                writer.WriteStartElement("Questions");
                foreach (var q in questions)
                {
                    writer.WriteStartElement("Question");
                    writer.WriteAttributeString("word", q.GetWord());
                    foreach (var a in q.GetTranslations())
                    {
                        writer.WriteElementString("Answer", a);
                    }
                    writer.WriteElementString("CorrectAnswer", q.GetCorrectAnswer().ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        internal static void GetQuestionsFromXML(List<Question> questions, string filename)
        {
            using XmlReader reader = XmlReader.Create(@".\Assets\XmlFiles\" + filename);
            {
                try
                {
                    Question question = null;
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if(reader.Name == "Question")
                            {
                                question = new Question(reader.GetAttribute("word"));
                            }
                            else if (reader.Name == "Answer")
                            {
                                question.AddTranslation(reader.ReadElementContentAsString());
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Question")
                        {
                            questions.Add(question);
                            question = null;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        internal static void GetQuizQuestionsFromXML(List<QuizQuestion> questions, string filename)
        {
            using XmlReader reader = XmlReader.Create(@".\Assets\XmlFiles\" + filename);
            {
                try
                {
                    QuizQuestion question = null;
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "Question")
                            {
                                question = new QuizQuestion(reader.GetAttribute("word"));
                            }
                            else if (reader.Name == "Answer")
                            {
                                question.AddTranslation(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "CorrectAnswer")
                            {
                                question.SetCorrectAnswer(reader.ReadElementContentAsInt());
                            }
                        }
                        else if (reader.NodeType== XmlNodeType.EndElement && reader.Name == "Question")
                        {
                            questions.Add(question);
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        internal static void SaveQuestionsToXML(List<Question> questions, string filename)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using XmlWriter writer = XmlWriter.Create(@".\Assets\XmlFiles\" + filename, xmlWriterSettings);
            {
                writer.WriteStartElement("Questions");
                foreach(var q in questions)
                {
                    writer.WriteStartElement("Question");
                    writer.WriteAttributeString("word", q.GetWord());
                    foreach(var a in q.GetTranslations())
                    {
                        writer.WriteElementString("Answer", a);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        internal static string GetInfo()
        {
            return File.ReadAllText(@".\Assets\TextFiles\About.txt");
        }
    }
}
