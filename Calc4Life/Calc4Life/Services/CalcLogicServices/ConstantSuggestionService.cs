using Calc4Life.Data;
using Calc4Life.Helpers;
using Calc4Life.Models;
using Calc4Life.Services.RepositoryServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calc4Life.Services.CalcLogicServices
{
    public class ConstantSuggestionService
    {
        IConstantsRepositoryService _constantsRepository;
        private List<Constant> _constants;
        private bool constantsUpdatedFlag;
        public ConstantSuggestionService()
        {
            _constantsRepository = App.Database;
            MessagingCenter.Subscribe<ConstItemDatabase>(this, AppConstants.CONSTANTS_UPDATED_MESSAGE, async (s) => await UpdateConstants());

        }
        private async Task UpdateConstants()
        {
            //TODO обновлять Subject при каждом обновлении констант
            _constants = await _constantsRepository.GetItemsAsync();
        }
        public IObservable<List<Constant>> SuggestionsObservable(IObservable<string> observable)
        {
            //var propertyChangedObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(target, eventName);
            //var displayChangedObservable = propertyChangedObservable.Where(r => r.EventArgs.PropertyName == propertyName).Select(m => propertyName);
            var subject = new Subject<List<Constant>>();

            observable
                 .DistinctUntilChanged()
                     .Subscribe(async s =>
                     {
                         if (!constantsUpdatedFlag)
                         {
                             await UpdateConstants();
                             _constants.ForEach(c => { if (c.Name.Count() > 27) c.Name = c.Name.Substring(0, 27) + "..."; });
                             constantsUpdatedFlag = true;
                         }
                         var consts = _constants?.Where(c => c.Value.ToString().StartsWith(s));
                         subject.OnNext(consts.ToList());


                     });
            return subject;
        }
    }
}
