using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc4Life.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Калькулятор";
            _navigationService = navigationService;
            ConstCommand = new DelegateCommand(ConstCommandExecute);
            OptionsCommand = new DelegateCommand(OptionsCommandExecute);
        }

        #region Properties
        #endregion

        #region Commands

        public DelegateCommand ConstCommand { get; }
        private async void ConstCommandExecute()
        {
            await _navigationService.NavigateAsync("NavigationPage/ConstantsContentPage", null, false, true);
        }

        public DelegateCommand OptionsCommand { get; }

        private void OptionsCommandExecute()
        {
            _navigationService.NavigateAsync("OptionsTabbedPage", null, false, true);
        }
        #endregion

        //bool isBackSpaceApplicable; //флаг - возможно ли редактирование дисплея кнопкой BackSpace
        //bool mustClearDisplay; //флаг - необходимо ли очистить дисплей перед вводом

        //public MainPage()
        //{
        //    this.InitializeComponent();
        //    Display.Text = "0";
        //    isBackSpaceApplicable = true;
        //    mustClearDisplay = false;
        //}

        ///// <summary>
        ///// кнопки редактирования значения на дисплее
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ButtonEditDisplay_Click(object sender, RoutedEventArgs e)
        //{
        //    //1. усли в операции определен оператор (значит идет ввод второго операнда), очищаем дисплей
        //    //if ((BinaryOperation.Operation != null & BinaryOperation.Operand2 == null))
        //    //    Display.Text = "";
        //    if (mustClearDisplay) Display.Text = String.Empty;

        //    //2. выводим на дисплей, значения вводимые с кнопок
        //    Display.Text = GetNewDisplayText(Display.Text, ((Button)sender).Tag.ToString());

        //    //3. назначаем операнд в операцию
        //    BinaryOperation.SetOperands(Double.Parse(Display.Text));

        //    mustClearDisplay = false;
        //    isBackSpaceApplicable = true;
        //}

        ///// <summary>
        ///// Кнопки ввода операторов
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ButtonOperator_Click(object sender, RoutedEventArgs e)
        //{
        //    mustClearDisplay = true;

        //    //1. форматируем дисплей
        //    double operand = Double.Parse(Display.Text);
        //    Display.Text = operand.ToString();

        //    //2. определить оператор и сохраняем в currentOperator
        //    Operations? currentOperator = null;

        //    string buttonTag = ((Button)sender).Tag.ToString();
        //    switch (buttonTag)
        //    {
        //        case "Plus": { currentOperator = Operations.Plus; break; }
        //        case "Minus": { currentOperator = Operations.Minus; break; }
        //        case "Multiplication": { currentOperator = Operations.Multiplication; break; }
        //        case "Division": { currentOperator = Operations.Division; break; }
        //        case "Discount": { currentOperator = Operations.Discount; break; }
        //    }

        //    //3. Сохранить оператор ИЛИ выполнить вычисление операции
        //    if (BinaryOperation.IsOperationFormed == true) //операция готова к вычислению
        //    {
        //        //1. произвести вычисление
        //        double? result = BinaryOperation.Result;

        //        //2. вывести результат на дисплей
        //        Display.Text = result.ToString();

        //        //3. очистить операцию
        //        BinaryOperation.Clear();

        //        //4. первому операнду присвоить значение, равное результату операции
        //        BinaryOperation.SetOperands(double.Parse(Display.Text));

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

        ///// <summary>
        ///// кнопка равно
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ButtonCalc_Click(object sender, RoutedEventArgs e)
        //{
        //    //1. производим вычисление ИЛИ выходим
        //    if (BinaryOperation.IsOperationFormed) //операция готова к вычислению
        //    {
        //        //1. произвести вычисление
        //        double? result = BinaryOperation.Result;

        //        //2. вывести результат на дисплей
        //        Display.Text = result.ToString();

        //        //3. очистить операцию
        //        BinaryOperation.Clear();

        //        //4. устанавливаем первый операнд равный результату вычисления
        //        BinaryOperation.SetOperands(Double.Parse(Display.Text));

        //        //5. устанавливаем флаги
        //        isBackSpaceApplicable = false;
        //        mustClearDisplay = true;
        //    }
        //}

        ///// <summary>
        ///// кнопки работы с памятью
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ButtonMemory_Click(object sender, RoutedEventArgs e)
        //{
        //    switch (((Button)sender).Tag)
        //    {
        //        case "mr":
        //            if (MemoryValue.Text.Count() == 0) return;

        //            Display.Text = MemoryValue.Text;
        //            isBackSpaceApplicable = false;
        //            mustClearDisplay = true;

        //            // назначаем операнд в операцию
        //            BinaryOperation.SetOperands(Double.Parse(Display.Text));
        //            break;
        //        case "mc":
        //            MemoryValue.Text = "";
        //            break;
        //        case "m":
        //            MemoryValue.Text = Display.Text;
        //            break;
        //    }
        //}

        ////кнопка изменения знака числа
        //private void ButtonSign_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (isBackSpaceApplicable == false) return;

        //    string str = Display.Text;

        //    if (str.StartsWith("-"))
        //        str = str.Remove(0, 1);
        //    else
        //        if (!str.StartsWith("0"))
        //        str = "-" + str;

        //    Display.Text = str;

        //    BinaryOperation.SetOperands(Double.Parse(str));
        //}

        ///// <summary>
        ///// кнопка BackSpace
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ButtonBackSpace_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!isBackSpaceApplicable) return;

        //    string currentDisplayText = Display.Text;

        //    int i = currentDisplayText.Length - 1;
        //    currentDisplayText = currentDisplayText.Remove(i);

        //    if (i == 0) currentDisplayText = "0";

        //    Display.Text = currentDisplayText;
        //    BinaryOperation.SetOperands(double.Parse(Display.Text));
        //}

        ///// <summary>
        ///// Возвращает отредактированное значение для дисплея
        ///// </summary>
        ///// <param name="currentDisplayText">Текущее значение дисплея</param>
        ///// <param name="tag">Тэг нажатой кнопки</param>
        ///// <returns></returns>
        //private string GetNewDisplayText(string currentDisplayText, string tag)
        //{
        //    string Result = currentDisplayText;
        //    switch (tag)
        //    {
        //        case "." when !currentDisplayText.Contains("."):
        //            {
        //                if (currentDisplayText == "") currentDisplayText = "0.";
        //                Result += tag;
        //                break;
        //            }
        //        case "0" when currentDisplayText != "0":
        //        case "1":
        //        case "2":
        //        case "3":
        //        case "4":
        //        case "5":
        //        case "6":
        //        case "7":
        //        case "8":
        //        case "9":
        //            {
        //                if (currentDisplayText.Length == 16) break;
        //                if (currentDisplayText == "0") Result = "";
        //                Result += tag;
        //                break;
        //            }

        //        case "Clear":
        //            {
        //                Result = "0";
        //                BinaryOperation.Clear();
        //                break;
        //            }
        //    }
        //    return Result;
        //}
    }
}
