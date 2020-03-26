using MVVM_Fuzzy_string_search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Fuzzy_string_search.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        static ResultContext db = new ResultContext();

        private RequestResult _inputResult = new RequestResult();
        public RequestResult InputResult
        {
            get { return _inputResult; }
            set { Set(ref _inputResult, value); }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                Set(ref _searchString, value);
            }
        }

        public Source SelectedSource { get; set; } = new Source();
        public List<Source> Sources
        {
            get
            {
                List<Source> tmp = new List<Source>();
                tmp.Add(new Source() { Id = 0, Name = "All", RequestResults = db.RequestResults.ToList<RequestResult>() });
                tmp.AddRange(db.Sources.ToList<Source>());
                return tmp;
            }
        }

        public Source InputSelectedSource { get; set; } = new Source();
        public List<Source> InputSources
        {
            get
            {
                return db.Sources.ToList<Source>();
            }
        }

        public List<RequestResult> Data
        {
            get 
            {
                if (String.IsNullOrWhiteSpace(SearchString))
                {
                    return SelectedSource.RequestResults.ToList<RequestResult>();
                }
                //List<RequestResult> tmp = db.RequestResults.Where(res => res.Content.Contains(SearchString)).ToList<RequestResult>();
                List<RequestResult> tmp = SelectedSource.RequestResults.ToList<RequestResult>().Where(res => res.Content.Contains(SearchString)).ToList<RequestResult>();
                if (tmp.Count < 5)
                {
                    tmp = SelectedSource.RequestResults.ToList<RequestResult>().
                        Where(res => VectorModel.CompareSenteces(SearchString, res.Content) >= 0.85).ToList<RequestResult>();
                }
                return tmp;
            }
        }

        public ICommand AddResult { get; }
        public ICommand SearchCommand { get; }


        public MainWindowVM()
        {
            AddResult = new RelayCommand(OnAddResult);
            SearchCommand = new RelayCommand(OnSearchCommand);
            //AddToDb();
            
            OnPropertyChanged("Sources");
            OnPropertyChanged("Data");
        }

        //
        public void AddToDb()
        {
            ParseResult file = ParserTxtToResult.Parse(@"D:\Timur\6_Semester\SPIT\timres.txt");
            List<Source> sources = file.Sources;
            List<RequestResult> res = file.RequestResults;

            db.Sources.AddRange(sources);
            db.SaveChanges();
            db.RequestResults.AddRange(res);
            db.SaveChanges();
        }

        private void OnSearchCommand(object obj)
        {
            OnPropertyChanged("Data");
        }

        private void OnAddResult(object param)
        {
            RequestResult result = new RequestResult
            {
                Source = InputSelectedSource,
                Content = InputResult.Content,
                Url = InputResult.Url,
                SourceId = InputSelectedSource.Id
            };

            db.RequestResults.Add(result);
            db.SaveChanges();
            OnPropertyChanged("Source");
            OnPropertyChanged("Data");
        }
    }
}
