using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Localization.Properties; //Пространство имен ресурсов

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    //Это свойства которые берут текст из Resources
    public string HelloText => Resources.HelloText;
    public string ChangeLanguageText => Resources.ChangeLanguage; //Изменено имя свойства
    public string String1 => Resources.String1;

    public ICommand ChangeLanguageCommand { get; }//Команда для смены языка

    public MainViewModel()//Rоманда, которая будет выполняться при нажатии на кнопку.
    {
        ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
    }

    private void ChangeLanguage()
    {
        string newLang = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? "ru-RU" : "en-US";
        CultureInfo newCulture = new CultureInfo(newLang);

        Thread.CurrentThread.CurrentUICulture = newCulture;
        Thread.CurrentThread.CurrentCulture = newCulture;
        Resources.Culture = newCulture; //Установка культуры в ресурсах

        //Обновляем язык WPF 
        Application.Current.MainWindow.Language = System.Windows.Markup.XmlLanguage.GetLanguage(newLang);

        UpdateLanguage();
    }


    public void UpdateLanguage()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HelloText)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChangeLanguageText))); // Обновляет текста 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(String1)));
    }
}
