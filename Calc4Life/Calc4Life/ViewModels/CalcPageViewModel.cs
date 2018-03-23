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
using Calc4Life.Services.FormatServices;
using Calc4Life.Helpers;



namespace Calc4Life.ViewModels
{
    public class CalcPageViewModel : ViewModelBase
    {
        #region Declarations
        const int maxFiguresNumber = 13; // максимальное число ВВОДИМЫХ ЦИФР С УЧЕТОМ ДЕСЯТИЧНОГО ЗНАКА (один знак зарезервирован под возможный МИНУС)

        bool isBackSpaceApplicable; // флаг - возможно ли редактирование дисплея кнопкой BackSpace
        bool mustClearDisplay; // флаг - необходимо ли очистить дисплей перед вводом

        decimal? registerOperand; // текущий операнд
        decimal? registerMemory; // значение ячейки памяти

        //string lastOperator; // последний введенный оператор

        string decimalSeparator; // десятичный знак числа

        IPageDialogService _dialogService;
        IBinaryOperationService _binaryOperation;
        FormatService _formatService;
        #endregion

        #region Constructors

        public CalcPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            IBinaryOperationService binaryOperationService, FormatService formatService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _binaryOperation = binaryOperationService;
            _formatService = formatService;

            Title = "Calculator for Life";
            Display = "0";
            isBackSpaceApplicable = true;
            mustClearDisplay = false;

            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            ConstCommand = new DelegateCommand(ConstCommandExecute);
            OptionsCommand = new DelegateCommand(OptionsCommandExecute);
            EnterFiguresCommand = new DelegateCommand<string>(EnterFiguresExecute);
            BackSpaceCommand = new DelegateCommand(BackSpaceExecute);
            EnterOperatorCommand = new DelegateCommand<string>(EnterOperatorExecute);
            CalcCommand = new DelegateCommand(CalcExecute);
            SignCommand = new DelegateCommand(SignExecute);
            MemoryCommand = new DelegateCommand<string>(MemoryExecute);
            AddConstantCommand = new DelegateCommand(AddConstExecute);
            ClearCommand = new DelegateCommand(ClearExecute);

        }

        #endregion

        #region Bindable Properties
        string _Display;
        public string Display
        {
            get { return _Display; }
            set { SetProperty(ref _Display, value); }
        }

        string _Expression;
        public string Expression
        {
            get { return _Expression; }
            set { SetProperty(ref _Expression, value); }
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
            get { return decimalSeparator; }
            set { SetProperty(ref decimalSeparator, value); }
        }

        bool _IsRounding;
        public bool IsRounding
        {
            get { return Settings.Rounding; }
            set { SetProperty(ref _IsRounding, value); }
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
            await NavigationService.NavigateAsync("OptionsPage?selectedTab=SettingsPage", null, false, true);
        }

        #region EditDisplayCommands

        public DelegateCommand<string> EnterFiguresCommand { get; }
        private void EnterFiguresExecute(string par)
        {
            //1 усли в операции определен оператор (значит идет ввод второго операнда), очищаем дисплей
            if (mustClearDisplay) Display = String.Empty;

            //2 выводим на дисплей, значения вводимые с кнопок
            Display = GetNewDisplayText(Display, par);

            //3 запоминаем в регистре операнда
            registerOperand = decimal.Parse(Display, CultureInfo.CurrentCulture);

            //4. назначаем операнд в операцию
            _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, null));

            //5 очищаем строку выражения
            Expression = GetNewExpression();

            //6 устанавливаем флаги
            mustClearDisplay = false;
            isBackSpaceApplicable = true;
        }

        public DelegateCommand BackSpaceCommand { get; }
        private void BackSpaceExecute()
        {
            if (!isBackSpaceApplicable) return;

            string currentDisplayText = Display;

            if (currentDisplayText == "0") return;

            int i = currentDisplayText.Length - 1;
            currentDisplayText = currentDisplayText.Remove(i);

            if (i == 1 & currentDisplayText == "-")
                currentDisplayText = "0";
            if (i == 0)
                currentDisplayText = "0";

            Display = currentDisplayText;
            // запоминаем в регистре операнда
            registerOperand = decimal.Parse(Display, CultureInfo.CurrentCulture);

            _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, null));

            Expression = GetNewExpression();
        }

        public DelegateCommand SignCommand { get; }
        private void SignExecute()
        {
            string currentDisplayText = Display;
            if (currentDisplayText == "0") return;

            if (currentDisplayText.StartsWith("-"))
                currentDisplayText = currentDisplayText.Remove(0, 1);
            else
                currentDisplayText = "-" + currentDisplayText;
            Display = currentDisplayText;

            // запоминаем в регистре операнда
            registerOperand = decimal.Parse(Display, CultureInfo.CurrentCulture);

            _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, null));

            Expression = GetNewExpression();
        }

        public DelegateCommand ClearCommand { get; }
        private void ClearExecute()
        {
            registerOperand = null;
            Display = "0";
            Expression = "";
            _binaryOperation.Clear();
        }

        #endregion

        public DelegateCommand<string> EnterOperatorCommand { get; }
        private void EnterOperatorExecute(string par) // Plus Minus Multiplication Division Discount
        {
            if (_binaryOperation.Operand1 == null) return;

            //1. поднимаем флаг
            mustClearDisplay = true;

            //2. форматируем дисплей
            Display = registerOperand.ToString();

            //3. 

            if (_binaryOperation.IsReadyForCalc() == false)
            {
                _binaryOperation.SetOperator(par);
            }
            else if (_binaryOperation.IsReadyForCalc() == true)
            {
                //1. произвести вычисление
                registerOperand = _binaryOperation.GetResult();
                //2. вывести результат на дисплей
                Display = _formatService.FormatResult(registerOperand.Value);

                //3. очистить операцию
                _binaryOperation.Clear();

                //4. первому операнду присвоить значение, равное результату операции

                _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, null));

                //5.
                _binaryOperation.SetOperator(par);

                //6.
                isBackSpaceApplicable = false;
            }

            Expression = GetNewExpression();
        }

        public DelegateCommand CalcCommand { get; }
        private void CalcExecute()
        {
            //1. производим вычисление ИЛИ выходим
            if (_binaryOperation.IsReadyForCalc()) //операция готова к вычислению
            {
                //1. произвести вычисление
                registerOperand = _binaryOperation.GetResult();

                //2. вывести результат на дисплей
                Display = _formatService.FormatResult(registerOperand.Value);
                Expression = GetNewExpression();

                //3. очистить операцию
                _binaryOperation.Clear();

                //4. устанавливаем первый операнд равный результату вычисления
                _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, null));

                //5. устанавливаем флаги
                isBackSpaceApplicable = false;
                mustClearDisplay = true;
            }
            //else
            //{
            //    //1.
            //    _binaryOperation.SetOperand(decimal.Parse(Display, CultureInfo.CurrentCulture));
            //    //2
            //    decimal? result = _binaryOperation.Result();

            //    //2. вывести результат на дисплей
            //    Display = result.ToString();

            //    //3. очистить операцию
            //    _binaryOperation.Clear();

            //    //4. устанавливаем первый операнд равный результату вычисления
            //    _binaryOperation.SetOperand(decimal.Parse(Display, CultureInfo.CurrentCulture));

            //    //5. устанавливаем флаги
            //    isBackSpaceApplicable = false;
            //    mustClearDisplay = true;
            //}
        }

        public DelegateCommand<string> MemoryCommand { get; }
        private void MemoryExecute(string par)
        {
            switch (par)
            {
                case "Add":
                    //decimal memoryValue;
                    if (Memory != null)
                    {
                        registerMemory = decimal.Parse(Memory, CultureInfo.CurrentCulture);
                        registerMemory += decimal.Parse(Display, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        registerMemory = decimal.Parse(Display, CultureInfo.CurrentCulture);
                    }
                    Memory = registerMemory.ToString();

                    IsMemoryVisible = true;
                    break;
                case "Clear":
                    Memory = null;
                    IsMemoryVisible = false;
                    break;
                case "Read":
                    if (Memory == null) return;
                    //memoryValue = decimal.Parse(Memory, CultureInfo.CurrentCulture);
                    Display = Memory;
                    _binaryOperation.SetOperand(CreateOperand(decimal.Parse(Display, CultureInfo.CurrentCulture), null));
                    isBackSpaceApplicable = false;
                    mustClearDisplay = true;
                    break;
            }
        }

        public DelegateCommand AddConstantCommand { get; }
        private async void AddConstExecute()
        {
            string message = $"Do you want to save {(decimal.Parse(Display, CultureInfo.CurrentCulture)).ToString()} as constant";
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
                decimal curConstValue = ((Constant)parameters["const"]).Value;
                string curConstName = ((Constant)parameters["const"]).Name;

                //2 
                registerOperand = curConstValue;

                //2. отражаем на дисплее
                Display = registerOperand.ToString();
                Expression = GetNewExpression();

                //3. назначаем операнд в операцию
                _binaryOperation.SetOperand(CreateOperand(registerOperand.Value, curConstName));
                Expression = GetNewExpression();

                //4. Устанавливаем флаги
                isBackSpaceApplicable = false;
                mustClearDisplay = true;
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            IsRounding = Settings.Rounding;
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

            //base.OnNavigatedFrom(parameters);
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
                        if (currentDisplayText.Length >= maxFiguresNumber) break;
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
                        if (currentDisplayText.Length >= maxFiguresNumber) break;
                        if (currentDisplayText == "0") Result = "";

                        Result += tag;

                        break;
                    }
            }
            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetNewExpression()
        {
            string output;
            string operand1;
            string operand2;

            //получаем операнды из операции
            operand1 = (_binaryOperation.Operand1 == null) ? "" : _binaryOperation.Operand1.ToString();
            operand2 = (_binaryOperation.Operand2 == null) ? "" : _binaryOperation.Operand2.ToString();

            //заворачиваем в скобки, если отрицательные
            if (operand1.StartsWith("-")) operand1 = $"({operand1})";
            if (operand2.StartsWith("-")) operand2 = $"({operand2})";

            //получаем оператор из операции
            string oper = "";

            {
                switch (_binaryOperation.Operator)
                {
                    case BinaryOperators.Plus:
                        oper = "+"; break;
                    case BinaryOperators.Minus:
                        oper = "-"; break;
                    case BinaryOperators.Multiplication:
                        oper = "×"; break;
                    case BinaryOperators.Division:
                        oper = "÷"; break;
                    case BinaryOperators.Discount:
                        oper = "%"; break;
                }
            }

            //добавляем (или нет) знак равенства в выражение 
            string equal;
            decimal? result = _binaryOperation.Result;
            if (result != null)
                equal = " =";
            else equal = "";

            //формируем строку вывода выражения
            oper = oper.Length == 0 ? "" : " " + oper;
            operand2 = operand2.Length == 0 ? "" : " " + operand2;

            output = $"{operand1}{oper}{operand2}{equal}";
            return output;
        }

        private Operand CreateOperand(decimal value, string name)
        {
            var result = new Operand();
            result.OperandValue = value;
            result.OperandName = name;

            return result;
        }

        #endregion
    }
}