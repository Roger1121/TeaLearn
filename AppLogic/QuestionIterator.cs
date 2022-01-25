using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{
    class QuestionIterator
    {
        private List<QuizQuestion> questionsQuiz;

        int index, size;

        public QuestionIterator()
        {
        }

        public QuestionIterator(List<QuizQuestion> quiz)
        {
            this.questionsQuiz = quiz;
            index = 0;
            size = quiz.Count;
        }
        public bool hasNext()
        {
            if(index > size)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public QuizQuestion next()
        {
            return questionsQuiz[index++];
        }
    }
}
