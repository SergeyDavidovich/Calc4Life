using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc4Life.Models;
using Calc4Life.Services;
using Xamarin.Forms;
using Calc4Life.ViewModels;
using System.Diagnostics;

namespace Calc4Life.Services.RepositoryServices
{
    public class ConstantsRepositoryServiceFake : IConstantsRepositoryService
    {
        private List<Constant> _constants;

        public ConstantsRepositoryServiceFake()
        {
            Debug.WriteLine($"ConstantsRepositoryServiceFake { DateTime.Now}");
        }

        public async Task<List<Constant>> GetItemsAsync()
        {
            return _constants = _constants ?? await ReadConstantsAsync();
        }

        public async Task<Constant> GetItemAsync(int id)
        {
            return await Task.Run(() =>
          (from p in _constants where p.Id.Equals(id) select p).First());
        }

        public async Task AddAsync(Constant entity)
        {
            if (_constants == null) _constants = await ReadConstantsAsync();

            entity.Id = GetId();

            _constants.Add(entity);

            await WriteConstatsAsync();
        }

        public async Task<int> DeleteAsync(Constant item)
        {
            _constants.Remove(_constants.Find(x => x.Id == item.Id));

            MessagingCenter.Send<ConstantsRepositoryServiceFake>(this, "deleted");

            await WriteConstatsAsync();
            return item.Id;
        }

        public async Task<List<Constant>> GetItemsFavoriteAsync()
        {
            return await Task.Run(() =>
                    (from constant in _constants where constant.IsFavorite.Equals(true) select constant).ToList<Constant>());
        }


        public async Task<int> SaveAsync(Constant entity)
        {
            return 1;// await Task.Run(null);
        }

        #region read/write
        private async Task<List<Constant>> ReadConstantsAsync()
        {

            return await Task.Run(() => _constants = new List<Constant>
            {
                new Constant(){Id=1, Name="Example of constant", Value=1234567.89m,
                    Note ="This is the constant example. You can edit or delete this one. Also you can add another constants in the list. Select and press green button to use", IsFavorite=false},
                new Constant(){Id=2, Name="Сan of beer", Value=10.55m, Note="Средняя цена одной банки пива в Евросоюзе в 2017 году", IsFavorite=false},
                new Constant(){Id=3, Name="Second Constant", Value=23456789 , Note="Заметка о Second Constant", IsFavorite=false},
                new Constant(){Id=4, Name="Third Constant", Value=34567890, Note="", IsFavorite=false},
                new Constant(){Id=5, Name="Forth Constant", Value=456789 , Note="Заметка о Forth Constant", IsFavorite=false},
            });
        }
        private async Task WriteConstatsAsync()
        {
            await Task.CompletedTask;
        }
        #endregion
        private int GetId()
        {

            int result = _constants.Max(n => n.Id); //максимальный Id
            result++;

            return result;
        }
    }
}
