using Calc4Life.Models;
using Calc4Life.Views.CustomCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Calc4Life.Helpers
{
    public class ChatListViewTemplateSelector : DataTemplateSelector
    {
        public ChatListViewTemplateSelector()
        {
            this.answerDataTemplate = new DataTemplate(typeof(AnswerViewCell));
            this.questionDataTemplate = new DataTemplate(typeof(QuestionViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as AboutMessage;
            if (messageVm == null)
                return null;
            return messageVm.IsAnswer ? this.answerDataTemplate : this.questionDataTemplate;
        }

        private readonly DataTemplate answerDataTemplate;
        private readonly DataTemplate questionDataTemplate;
    }
}