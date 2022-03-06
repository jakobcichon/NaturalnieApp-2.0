using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            public string? FullText { get; set; }
            public object? SourceObject { get; set; }
            public TextBlock TextBlock { get; set; }

            public SearchListObject()
            {
                TextBlock = new TextBlock();
            }

            internal void AddFromObject(object elementToAdd)
            {
                FullText = elementToAdd?.ToString();
                if (FullText == null) FullText = nameof(FullText);
                SourceObject = elementToAdd;
                TextBlock.Inlines.Add(new Run(FullText));
            }

            internal void FillFromOtherObject(SearchListObject referenceObject)
            {
                FullText = referenceObject.FullText;
                SourceObject = referenceObject.SourceObject;

            }
        }

        ObservableCollection<SearchListObject> FullList { get; set; }

        #region Constructor
        public ComboboxNaturalnieApp()
        {
            InitializeComponent();

        }
        #endregion

        #region Dependency Properties
        /// <summary>
        /// 
        /// </summary>
        static readonly DependencyProperty HintItemsSourceProperty = DependencyProperty.Register("HintItemsSource",
        typeof(IEnumerable<object>), typeof(ComboboxNaturalnieApp),
        new PropertyMetadata(null, new PropertyChangedCallback(HintItemsSourceChanged)));

        private static void HintItemsSourceChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            ComboboxNaturalnieApp local = d as ComboboxNaturalnieApp;
            local.HintItemsSourceChanged(e);
        }

        private void HintItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            OnHintItemsSourceChange();
        }

        public IEnumerable<object>? HintItemsSource
        {
            get
            {
                return (IEnumerable<object>)GetValue(HintItemsSourceProperty);
            }
            set
            {
                SetValue(HintItemsSourceProperty, value);
            }
        }

        /// <summary>
        /// HintItemsSourceProperty
        /// </summary>
        static readonly DependencyProperty HintItemsProperty = DependencyProperty.Register("HintItems",
            typeof(ObservableCollection<SearchListObject>), typeof(ComboboxNaturalnieApp));
        
        private ObservableCollection<SearchListObject>? HintItems
        {
            get
            {
                return (ObservableCollection<SearchListObject>)GetValue(HintItemsProperty);
            }
            set
            {
                SetValue(HintItemsProperty, value);
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
            }
        }
        #endregion

        #region Private methods
        private void OnHintItemsSourceChange()
        {
            if (HintItemsSource == null) return;

            ObservableCollection<SearchListObject> convertedList = new ObservableCollection<SearchListObject>();

            foreach (var item in HintItemsSource)
            {
                convertedList.Add(new SearchListObject());
                convertedList.Last().AddFromObject(item);
            }

            FullList = convertedList;
            HintItems = FullList;
        }

        private void OnSelectedItemChange()
        {

        }

        private void ShowHintItemsPanel()
        {
            if (HintItemsPanel == null) return;
            HintItemsPanel.IsOpen = true;
            HintItemsPanel.Focus();

        }

        private void HideHintItemsPanel()
        {
            if (HintItemsPanel == null) return;
            HintItemsPanel.IsOpen = false;
        }

        private void ToggleHintItemsPanel()
        {
            if (HintItemsPanel == null) return;
            if (HintItemsPanel.IsOpen == false)
            {
                HintItemsPanel.IsOpen = true;
                return;
            }
            HintItemsPanel.IsOpen = false;

        }

        private ObservableCollection<SearchListObject>? SearchInHintList(string searchText)
        {
            if (FullList == null) return null;

            IEnumerable<SearchListObject> searchList = FullList.Where(e =>
            {
                bool? result = e?.FullText?.ToString()?.ToLower()?.Contains(searchText.ToLower());
                return result ?? true;
            });

            ObservableCollection<SearchListObject> returnList = new ObservableCollection<SearchListObject>();

            foreach (SearchListObject search in searchList)
            {
                SearchListObject searchObject = new SearchListObject();
                searchObject.FillFromOtherObject(search);

                //Get parts of a text
                List<Run> inlines = SplitStringBySearchedTextBolded(searchObject.FullText, searchText);
                searchObject.TextBlock.Inlines.Clear();
                searchObject.TextBlock.Inlines.AddRange(inlines);
                returnList.Add(searchObject);
            }

            return returnList;
        }

        /// <summary>
        /// Method returns same text, but projected into Run list, where search text is bold
        /// </summary>
        /// <param name="fullText"></param>
        /// <param name="searchedText"></param>
        /// <returns></returns>
        private List<Run> SplitStringBySearchedTextBolded(string fullText, string searchedText)
        {
            List<Run> retList = new List<Run>();
            
            Regex regex = new Regex(@searchedText.ToLower());
            Match match = regex.Match(fullText.ToLower());

            if (match.Success)
            {
                int searchPartStartIndex = match.Index;
                int searchPartLength = match.Length;

                int prePartStartIndex;
                int prePartLength;

                int posPartStartIndex;
                int postPartLength;

                //If exist, add pre part
                if (searchPartStartIndex > 0)
                {
                    prePartStartIndex = 0;
                    prePartLength = searchPartStartIndex;

                    retList.Add(new Run(fullText.Substring(prePartStartIndex, prePartLength)));
                }

                //Add bold part
                retList.Add(new Run(fullText.Substring(searchPartStartIndex, searchPartLength)));
                retList.Last().FontWeight = FontWeights.Bold;

                //If any post text exist, use recurrence to search string
                if (searchPartStartIndex + searchPartLength < fullText.Length)
                {
                    posPartStartIndex = searchPartStartIndex + searchPartLength;
                    postPartLength = fullText.Length - posPartStartIndex;

                    string postText = fullText.Substring(posPartStartIndex, postPartLength);

                    retList.AddRange(SplitStringBySearchedTextBolded(postText, searchedText));
                }

                return retList;
            }

            // If does not found anything, return full text
            retList.Add(new Run(fullText));

            return retList;
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
            return textRange.Text.Trim();
        }

        #endregion

        #region Private events
        private void HintItemButton_Click(object sender, RoutedEventArgs e)
        {

            ListViewItem localSender = sender as ListViewItem;
            if (localSender == null) return;

            SearchListObject localContent = localSender.Content as SearchListObject;
            if (localContent == null) return;

            if (localContent.TextBlock.GetType() == typeof(TextBlock))
            {
                TextBlock local = localContent.TextBlock;
                if (local == null) return;

                InputField.Document.Blocks.Clear();
                string text = "";
                foreach (Inline inline in local.Inlines)
                {
                    Run temp = inline as Run;
                    if (temp == null) continue;

                    text += temp.Text;
                }
                InputField.Document.Blocks.Add(new Paragraph(new Run(text)));
            }

            HideHintItemsPanel();
        }

        private void HintItemsPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            HideHintItemsPanel();
        }

        private void OpenCloseHintItemsButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleHintItemsPanel();
        }

        private void InputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            DebugUtils.DebugTimer timer = new DebugUtils.DebugTimer("Search text changed");
            timer.StartTimer();

            RichTextBox? localSender = e.Source as RichTextBox;
            string text = StringFromRichTextBox(localSender).Trim();

            if (localSender == null || FullList == null) return;


            if (StringFromRichTextBox(localSender) == "")
            {
                HintItems = FullList;
                HideHintItemsPanel();
                return;
            }

            HintItems = SearchInHintList(text);

            if (HintItems == null || HintItems.Count == 0)
            {
                HintItems = FullList;
                HideHintItemsPanel();
                return;
            }

            //Show hint items panel
            ShowHintItemsPanel();

            timer.StopTimer();

        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (HintItemsPanel.IsMouseOver) return;
            HideHintItemsPanel();
        }


        private void HintItemsPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            HideHintItemsPanel();
        }
        #endregion

    }
}
