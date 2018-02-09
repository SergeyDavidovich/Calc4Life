using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calc4Life.Services;
using System.Globalization;

namespace Calc4Life.ViewModels
{
    public class CalcPageViewModel : ViewModelBase
    {
        #region Declarations

        bool isBackSpaceApplicable; //флаг - возможно ли редактирование дисплея кнопкой BackSpace
        bool mustClearDisplay; //флаг - необходимо ли очистить дисплей перед вводом
        #endregion

        #region Constructors

        public CalcPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Calculator for Life";
            Display = "0";
            isBackSpaceApplicable = true;
            mustClearDisplay = false;

            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            ConstCommand = new DelegateCommand(ConstCommandExecute);
            OptionsCommand = new DelegateCommand(OptionsCommandExecute);
            EditDisplayCommand = new DelegateCommand<string>(EditDisplayExecute);
            BackSpaceCommand = new DelegateCommand(BackSpaceExecute);
            OperatorCommand = new DelegateCommand<string>(OperatorExecute);
            CalcCommand = new DelegateCommand(CalcExecute);
            SignCommand = new DelegateCommand(SignExecute);
            MemoryCommand = new DelegateCommand<string>(MemoryExecute);
        }

        #endregion

        #region Properties
        string _Display;
        public string Display
        {
            get { return _Display; }
            set { SetProperty(ref _Display, value); }
        }

        string _Memory;
        public string Memory
        {
            get { return _Memory; }
            set { SetProperty(ref _Memory, value); }
        }

        bool _IsMemoryVisible;
        public bool IsMemoryVisible
        {
            get { return _IsMemoryVisible; }
            set { SetProperty(ref _IsMemoryVisible, value); }
        }

        string _DecimalSeparator;
        public string DecimalSeparator
        {
            get { return _DecimalSeparator; }
            set { SetProperty(ref _DecimalSeparator, value); }
        }
        #endregion

        #region Commands

        public DelegateCommand ConstCommand { get; }
        private async void ConstCommandExecute()
        {
            await NavigationService.NavigateAsync("ConstantsPage", null, false, true);
        }

        public DelegateCommand OptionsCommand { get; }
        private async void OptionsCommandExecute()
        {
            await NavigationService.NavigateAsync("OptionsPage?selectedTab=SettingsPage");
        }

        public DelegateCommand<string> EditDisplayCommand { get; }
        private void EditDisplayExecute(string par)
        {
            //1. усли в операции определен оператор (значит идет ввод второго операнда), очищаем дисплей
            if (mustClearDisplay) Display = String.Empty;

            //2. выводим на дисплей, значения вводимые с кнопок
            Display = GetNewDisplayText(Display, par);

            //    //3. назначаем операнд в операцию
            BinaryOperation.SetOperands(Double.Parse(Display));

            mustClearDisplay = false;
            isBackSpaceApplicable = true;
        }

        public DelegateCommand BackSpaceCommand { get; }
        private void BackSpaceExecute()
        {
            if (!isBackSpaceApplicable) return;

            string currentDisplayText = Display;

            int i = currentDisplayText.Length - 1;
            currentDisplayText = currentDisplayText.Remove(i);

            if (i == 0) currentDisplayText = "0";

            Display = currentDisplayText;
            BinaryOperation.SetOperands(double.Parse(Display));
        }

        public DelegateCommand<string> OperatorCommand { get; }
        private void OperatorExecute(string par)
        {
            mustClearDisplay = true;

            //1. форматируем дисплей
            double operand = Double.Parse(Display);
            Display = operand.ToString();

            //2. определить оператор и сохраняем в currentOperator
            Operations? currentOperator = null;

            switch (par)
            {
                case "Plus": { currentOperator = Operations.Plus; break; }
                case "Minus": { currentOperator = Operations.Minus; break; }
                case "Multiplication": { currentOperator = Operations.Multiplication; break; }
                case "Division": { currentOperator = Operations.Division; break; }
                case "Discount": { currentOperator = Operations.Discount; break; }
            }

            //3. Сохранить оператор ИЛИ выполнить вычисление операции
            if (BinaryOperation.IsOperationFormed == true) //операция готова к вычислению
            {
                //1. произвести вычисление
                double? result = BinaryOperation.Result;

                //2. вывести результат на дисплей
                Display = result.ToString();

                //3. очистить операцию
                BinaryOperation.Clear();

                //4. первому операнду присвоить значение, равное результату операции
                BinaryOperation.SetOperands(double.Parse(Display));

                isBackSpaceApplicable = false;
            }
            else //операция не готова к вычислению
            {
                //1. изменить оператор
                BinaryOperation.Operation = currentOperator;

                //2. выйди из процедуры
                return;
            }
        }

        public DelegateCommand CalcCommand { get; }
        private void CalcExecute()
        {
            //1. производим вычисление ИЛИ выходим
            if (BinaryOperation.IsOperationFormed) //операция готова к вычислению
            {
                //1. произвести вычисление
                double? result = BinaryOperation.Result;

                //2. вывести результат на дисплей
                Display = result.ToString();

                //3. очистить операцию
                BinaryOperation.Clear();

                //4. устанавливаем первый операнд равный результату вычисления
                BinaryOperation.SetOperands(Double.Parse(Display));

                //5. устанавливаем флаги
                isBackSpaceApplicable = false;
                mustClearDisplay = true;
            }
        }

        public DelegateCommand SignCommand { get; }
        private void SignExecute()
        {
            //if (isBackSpaceApplicable == false) return;

            string str = Display;

            if (str.StartsWith("-"))
                str = str.Remove(0, 1);
            else
                if (!str.StartsWith("0"))
                str = "-" + str;

            Display = str;
            BinaryOperation.SetOperands(Double.Parse(str));
        }

        public DelegateCommand<string> MemoryCommand { get; }
        private void MemoryExecute(string par)
        {
            switch (par)
            {
                case "Add":
                    Memory = null;
                    Memory = double.Parse(Display).ToString();
                    IsMemoryVisible = true;
                    break;
                case "Clear":
                    Memory = null;
                    IsMemoryVisible = false;
                    break;
                case "Read":
                    if (Memory == null) return;
                    Display = Memory;
                    BinaryOperation.SetOperands(Double.Parse(Display));
                    isBackSpaceApplicable = false;
                    mustClearDisplay = true;
                    break;
            }
        }
        #endregion

        #region Private utilites
        /// <summary>
        /// Возвращает отредактированное значение для дисплея
        /// </summary>
        /// <param name="currentDisplayText">Текущее значение дисплея</param>
        /// <param name="tag">Тэг нажатой кнопки</param>
        /// <returns></returns>
        private string GetNewDisplayText(string currentDisplayText, string tag)
        {
            string Result = currentDisplayText;
            switch (tag)
            {
                case "," when !currentDisplayText.Contains("."):
                    {
                        if (currentDisplayText == "") currentDisplayText = "0,";
                        Result += tag;
                        break;
                    }
                case "0" when currentDisplayText != "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    {
                        if (currentDisplayText.Length == 16) break;
                        if (currentDisplayText == "0") Result = "";
                        Result += tag;
                        break;
                    }

                case "Clear":
                    {
                        Result = "0";
                        BinaryOperation.Clear();
                        break;
                    }
            }
            return Result;
        }
        #endregion
    }
}
