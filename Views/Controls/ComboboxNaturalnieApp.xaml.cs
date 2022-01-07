using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaturalnieApp2.Views.Controls
{
    /// <summary>
    /// Interaction logic for ComboboxNaturalnieApp.xaml
    /// </summary>
    public partial class ComboboxNaturalnieApp : UserControl, INotifyPropertyChanged
    {
        internal class SearchListObject
        {
            public string? DisplayText { get; set; }
            public object? SourceObject { get; set; }

            internal void AddFromObject(object elementToAdd)
            {
                DisplayText = elementToAdd?.ToString();
                if (DisplayText == null) DisplayText = nameof(DisplayText);
                SourceObject = elementToAdd;
            }
        }

        ObservableCollection<SearchListObject> FullList { get; set; }

        #region Constructor
        public ComboboxNaturalnieApp()
        {
            InitializeComponent();
            ObservableCollection<object> test = new ObservableCollection<object>()
            { "Zima pada deszcz" , "test2", "SZCZE"};

            ObservableCollection<SearchListObject> testList = new ObservableCollection<SearchListObject>();
            foreach(var item in test)
            {
                testList.Add(new SearchListObject());
                testList.Last().AddFromObject(item);
            }

            HintItemsSource = test;
            HintItems = testList;
            FullList = testList;
            ;

        }
        #endregion

        #region Dependency Properties
        /// <summary>
        /// 
        /// </summary>
        static readonly DependencyProperty HintItemsSourceProperty = DependencyProperty.Register("HintItemsSource",
        typeof(IEnumerable<object>), typeof(ComboboxNaturalnieApp));

        private IEnumerable<object> HintItemsSource
        {
            get
            {
                return (IEnumerable<object>)GetValue(HintItemsSourceProperty);
            }
            set
            {
                SetValue(HintItemsSourceProperty, value);
                OnPropertyChanged(nameof(HintItemsSource));
            }
        }

        /// <summary>
        /// HintItemsSourceProperty
        /// </summary>
        static readonly DependencyProperty HintItemsProperty = DependencyProperty.Register("HintItems",
            typeof(ObservableCollection<SearchListObject>), typeof(ComboboxNaturalnieApp));
        
        private ObservableCollection<SearchListObject> HintItems
        {
            get
            {
                return (ObservableCollection<SearchListObject>)GetValue(HintItemsProperty);
            }
            set
            {
                SetValue(HintItemsProperty, value);
                OnPropertyChanged(nameof(HintItems));
            }
        }

        /// <summary>
        /// HintItemsSourceProperty
        /// </summary>
        static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
            typeof(IEnumerable<object>), typeof(ComboboxNaturalnieApp));


        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        public IEnumerable<object> SelectedItem
        {
            get
            {
                return (IEnumerable<object>)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        #endregion

        #region Private methods
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(HintItemsSource)) OnHintItemsSourceChange();
            if (propertyName == nameof(SelectedItem)) OnSelectedItemChange();
        }

        private void OnHintItemsSourceChange()
        {

        }

        private void OnSelectedItemChange()
        {

        }

        private ObservableCollection<SearchListObject> SearchInHintList(string searchText)
        {
            IEnumerable<SearchListObject> searchList = FullList.Where(e =>
            {
                bool? result = e?.DisplayText?.ToString()?.ToLower()?.Contains(searchText.ToLower());
                return result ?? true;
            });

            ObservableCollection<SearchListObject> returnList = new ObservableCollection<SearchListObject>();

            foreach (SearchListObject search in searchList)
            {
                returnList.Add(search);
            }

            return returnList;
        }

        #endregion

        #region Private events
        private void HintItemButton_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void InputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBox? localSender = e.Source as RichTextBox;
            string text = StringFromRichTextBox(localSender).Trim();

            if (localSender == null) return;

            if (StringFromRichTextBox(localSender) == "")
            {
                HintItems = FullList;
                return;
            }

            HintItems = SearchInHintList(text);

            if (HintItems == null || HintItems.Count == 0)
            {
                HintItems = FullList;
                return;
            }

            string? searchedText = HintItems.First().DisplayText;
            if (string.IsNullOrEmpty(searchedText)) return;

            int firstIndex, lastIndex;

            (firstIndex, lastIndex) = GetIndexcesStringInString(text, searchedText);
            ;

        }

        private (int, int) GetIndexcesStringInString(string searchingString, string searchedString)
        {
            int firstIndex = searchedString.IndexOf(searchingString);
            int lastIndex = firstIndex + searchingString.Length;

            return (firstIndex, lastIndex);
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
        #endregion


    }
}
