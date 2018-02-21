using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calc4Life.Services;
using System.Globalization;
using Calc4Life.Models;
using Prism.Services;
using Calc4Life.Services.OperationServices;

namespace Calc4Life.ViewModels
{
    public class CalcPageViewModel : ViewModelBase
    {
        #region Declarations

        bool isBackSpaceApplicable; //флаг - возможно ли редактирование дисплея кнопкой BackSpace
        bool mustClearDisplay; //флаг - необходимо ли очистить дисплей перед вводом
        string _lastOperator; // последний введенный оператор
        string _DecimalSeparator;

        IPageDialogService _dialogService;
        IBinaryOperationService _binaryOperation;
        #endregion

        #region Constructors

        public CalcPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IBinaryOperationService binaryOperationService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _binaryOperation = binaryOperationService;

            Title = "Calculator for Life";
            Display = "0";
            isBackSpaceApplicable = true;
            mustClearDisplay = false;

            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            ConstCommand = new DelegateCommand(ConstCommandExecute);
            OptionsCommand = new DelegateCommand(OptionsCommandExecute);
            EditDisplayCommand = new DelegateCommand<string>(EditDisplayExecute);
            BackSpaceCommand = new DelegateCommand(BackSpaceExecute);
            OperatorCommand = new DelegateCommand<string>(OperatorExecute);
            CalcCommand = new DelegateCommand(CalcExecute);
            SignCommand = new DelegateCommand(SignExecute);
            MemoryCommand = new DelegateCommand<string>(MemoryExecute);
            AddConstantCommand = new DelegateCommand(AddConstExecute);
        }

        #endregion

        #region Bindable Properties
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
            await NavigationService.NavigateAsync("ConstantsPage", null, false, false);
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
            _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));

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

            if (i == 1 & currentDisplayText == "-")
                currentDisplayText = "0";
            if (i == 0)
                currentDisplayText = "0";

            Display = currentDisplayText;
            _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));
        }

        public DelegateCommand<string> OperatorCommand { get; }
        private void OperatorExecute(string par) // Plus Minus Multiplication Division Discount
        {
            //1. поднимаем флаг
            mustClearDisplay = true;

            //2. форматируем дисплей
            double operand = Double.Parse(Display, CultureInfo.CurrentCulture);
            Display = operand.ToString();

            //3. 
            _lastOperator = par;


            if (_binaryOperation.IsReadyForCalc() == false)
            {
                _binaryOperation.SetOperator(par);
            }
            else if (_binaryOperation.IsReadyForCalc() == true)
            {
                //1. произвести вычисление
                double? result = _binaryOperation.Result();

                //2. вывести результат на дисплей
                Display = result.ToString();

                //3. очистить операцию
                _binaryOperation.Clear();

                //4. первому операнду присвоить значение, равное результату операции
                _binaryOperation.SetOperand(double.Parse(Display, CultureInfo.CurrentCulture));

                //5.
                _binaryOperation.SetOperator(par);

                //6.
                isBackSpaceApplicable = false;
            }
        }

        //private void OperatorExecute1(string par) // Plus Minus Multiplication Division Discount
        //{
        //    mustClearDisplay = true;

        //    //1. форматируем дисплей
        //    double operand = Double.Parse(Display, CultureInfo.CurrentCulture);
        //    Display = operand.ToString();

        //    ////2. определить оператор и сохраняем в currentOperator
        //    //BinaryOperators? currentOperator = null;

        //    //switch (par)
        //    //{
        //    //    case "Plus": { currentOperator = BinaryOperators.Plus; break; }
        //    //    case "Minus": { currentOperator = BinaryOperators.Minus; break; }
        //    //    case "Multiplication": { currentOperator = BinaryOperators.Multiplication; break; }
        //    //    case "Division": { currentOperator = BinaryOperators.Division; break; }
        //    //    case "Discount": { currentOperator = BinaryOperators.Discount; break; }
        //    //}

        //    //3. Сохранить оператор ИЛИ выполнить вычисление операции
        //    if (BinaryOperation.IsOperationFormed == true) //операция готова к вычислению
        //    {
        //        //1. произвести вычисление
        //        double? result = BinaryOperation.Result;

        //        //2. вывести результат на дисплей
        //        Display = result.ToString();

        //        //3. очистить операцию
        //        BinaryOperation.Clear();

        //        //4. первому операнду присвоить значение, равное результату операции
        //        BinaryOperation.SetOperands(double.Parse(Display, CultureInfo.CurrentCulture));

        //        isBackSpaceApplicable = false;
        //    }
        //    else //операция не готова к вычислению
        //    {
        //        //1. изменить оператор
        //        BinaryOperation.Operation = currentOperator;

        //        //2. выйди из процедуры
        //        return;
        //    }
        //}

        public DelegateCommand CalcCommand { get; }
        private void CalcExecute()
        {
            //1. производим вычисление ИЛИ выходим
            if (_binaryOperation.IsReadyForCalc()) //операция готова к вычислению
            {
                //1. произвести вычисление
                double? result = _binaryOperation.Result();

                //2. вывести результат на дисплей
                Display = result.ToString();

                //3. очистить операцию
                _binaryOperation.Clear();

                //4. устанавливаем первый операнд равный результату вычисления
                _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));

                //5. устанавливаем флаги
                isBackSpaceApplicable = false;
                mustClearDisplay = true;
            }
            //else
            //{
            //    //1.
            //    _binaryOperation.SetOperand(double.Parse(Display, CultureInfo.CurrentCulture));
            //    //2
            //    double? result = _binaryOperation.Result();

            //    //2. вывести результат на дисплей
            //    Display = result.ToString();

            //    //3. очистить операцию
            //    _binaryOperation.Clear();

            //    //4. устанавливаем первый операнд равный результату вычисления
            //    _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));

            //    //5. устанавливаем флаги
            //    isBackSpaceApplicable = false;
            //    mustClearDisplay = true;
            //}
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
            _binaryOperation.SetOperand(Double.Parse(str, CultureInfo.CurrentCulture));
        }

        public DelegateCommand<string> MemoryCommand { get; }
        private void MemoryExecute(string par)
        {
            switch (par)
            {
                case "Add":
                    double memoryValue;
                    if (Memory != null)
                    {
                        memoryValue = Double.Parse(Memory, CultureInfo.CurrentCulture);
                        memoryValue += double.Parse(Display, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        memoryValue = double.Parse(Display, CultureInfo.CurrentCulture);
                    }
                    Memory = memoryValue.ToString();

                    IsMemoryVisible = true;
                    break;
                case "Clear":
                    Memory = null;
                    IsMemoryVisible = false;
                    break;
                case "Read":
                    if (Memory == null) return;
                    Display = Memory;
                    _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));
                    isBackSpaceApplicable = false;
                    mustClearDisplay = true;
                    break;
            }
        }

        public DelegateCommand AddConstantCommand { get; }
        private async void AddConstExecute()
        {
            string message = $"Do you want to save {(double.Parse(Display, CultureInfo.CurrentCulture)).ToString()} as constant";
            var answer = await _dialogService.DisplayAlertAsync("", message, "Yes", "No");
            if (answer == true)
            {
                var par = new NavigationParameters();
                par.Add("value", Display);
                await NavigationService.NavigateAsync("EditConstPage", par, false, true);
            }
        }
        #endregion

        #region Navigation

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                //1. получаем параметр
                double curConst = ((Constant)parameters["const"]).Value;
                //2. отражаем на дисплее
                Display = curConst.ToString();
                //3. назначаем операнд в операцию
                _binaryOperation.SetOperand(Double.Parse(Display, CultureInfo.CurrentCulture));
                //4. Устанавливаем флаги
                isBackSpaceApplicable = false;
                mustClearDisplay = true;
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
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
                case "DecPoint" when !currentDisplayText.Contains(DecimalSeparator):
                    {
                        if (currentDisplayText == "") currentDisplayText = "0" + DecimalSeparator;
                        else currentDisplayText += DecimalSeparator;

                        Result = currentDisplayText;
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
                        _binaryOperation.Clear();
                        break;
                    }
            }
            return Result;
        }
        #endregion
    }
}
