using DMapp.Helpers;
using DMapp.Models;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace DMapp.ViewModel
{
    class ItemsVM : BaseViewModel
    {
        INavigation navigation;

        // Titles of these decisions sessions will be displayed in list.
        public ObservableCollection<DecisionSession> Decisions { get; set; }
        public Command LoadDecisionsCommand { get; set; }

        public Command NewDecisionCommand { get; set; }
        public Command DeleteCommand { get; set; }

        private ObservableCollection<SessionCategory> SessionCategoriesFromDatabase;

        private List<DecisionSession> notes;


        public ItemsVM(INavigation nav)
        {
            Title = "Browse";
            navigation = nav;
            InitializeFields();
        }

        private void InitializeFields()
        {
            //var navipaths = navigation.NavigationStack;
            Decisions = new ObservableCollection<DecisionSession>();
            LoadDecisionsCommand = new Command( () => ExecuteLoadDecisionsCommand());
            NewDecisionCommand = new Command(async () => await ExecuteNewDecisionCommand());
            PickerSessionCategories = new ObservableCollection<string>();
            DeleteCommand = new Command<DecisionSession>((x) => ExecuteDeleteCommand(x));

            ChoosenCategory = "All";
            
        }

        private void ExecuteDeleteCommand(DecisionSession sessionObject)
        {
            var allSessions = ManagerSQL.ReadDecisionSessions();
            DecisionSession sessionToDelete = new DecisionSession();
            foreach(var session in allSessions)
            {
                if(session.Title == sessionObject.Title) { sessionToDelete = session; break; }
            }
            List<Option> optionsToDelete = ManagerSQL.ReadOptions().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();
            List<Quality> qualitiesToDelete = ManagerSQL.ReadQualities().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();
            List<Weight> weightsToDelete = ManagerSQL.ReadWeights().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();

            ManagerSQL.DeleteDecisionSession(sessionToDelete);
            foreach(var option in optionsToDelete) { ManagerSQL.DeleteOption(option); }
            foreach (var quality in qualitiesToDelete) { ManagerSQL.DeleteQuality(quality); }
            foreach(var weight in weightsToDelete) { ManagerSQL.DeletetWeight(weight); }

            var test1 = ManagerSQL.ReadDecisionSessions();
            var test2 = ManagerSQL.ReadOptions();
            var test3 = ManagerSQL.ReadQualities();
            var test4 = ManagerSQL.ReadWeights();

            ExecuteLoadDecisionsCommand();
        }

        public void loadPickerOptions()
        {
            SessionCategoriesFromDatabase = new ObservableCollection<SessionCategory>(ManagerSQL.ReadSessionCategories());
            PickerSessionCategories.Clear();
            pickerSessionCategories.Add("All");
            ChoosenCategory = "All";
            foreach (var category in SessionCategoriesFromDatabase) { PickerSessionCategories.Add(category.CategoryName); }
        }

        public void PageAppeared()
        {
            
             ExecuteLoadDecisionsCommand();
            OptionsChoiceSliderValuesHolder.SliderValues = null;
            QualitiesChoiceSliderValuesHolder.SliderValues = null;
            InitializationCounter.numOfQualitiesChoicesVMInitialized = 0;
            InitializationCounter.numOfOptionsChoicesVMInitialized = 0;
            QualitiesChoiceSliderValuesHolder.oldSequence = null;
        }
    

        #region Commands
        private async void CallLoadDecisionSessionCommand()
        {
            await ExecuteLoadDecisionSessionCommand();
        }

        private  void CallLoadDecisionsCommand()
        {
             ExecuteLoadDecisionsCommand();
        }
        private void ExecuteLoadDecisionsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Decisions.Clear();

                // make reading this data async!


                //Mock_DB mockDB = new Mock_DB();
                notes = ManagerSQL.ReadDecisionSessions().ToList();

                if (choosenCategory != "All")
                {
                    int choosenCategoryID = SessionCategoriesFromDatabase.Where(x => x.CategoryName == choosenCategory).Select(x => x.SessionCategoryID).FirstOrDefault();
                    notes = notes.Where(x => x.SessionCategoryID == choosenCategoryID).ToList();
                }
                

                foreach (var note in notes)
                {
                    Decisions.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private async Task ExecuteNewDecisionCommand()
        {
            TemporaryDb.CleanAllData();
            CurrentIndexHolder.QualitiesCurrentIndex = 1;
            QualitiesChoiceSliderValuesHolder.SliderValues = null;
            await navigation.PushAsync(new SessionSetupPage(navigation));

        }
        private async Task ExecuteLoadDecisionSessionCommand()
        {
            await navigation.PushAsync(new GeneralResultsPage(navigation, 1, selectedSession.SessionID));
        }

        #endregion
        
        #region bindable properties
        private DecisionSession selectedSession;
        public DecisionSession SelectedSession
        {
            get { return selectedSession; }
            set
            {
                selectedSession = value;

                if (selectedSession != null)
                {
                    CallLoadDecisionSessionCommand(); //in the future add specific page which exist and we want to edit it.
                }


                OnPropertyChanged();

            }
        }

        private ObservableCollection<string> pickerSessionCategories;

        public ObservableCollection<string> PickerSessionCategories
        {
            get { return pickerSessionCategories; }
            set { pickerSessionCategories = value;
                OnPropertyChanged();
            }
        }

        private string choosenCategory;

        public string ChoosenCategory
        {
            get { return choosenCategory; }
            set { choosenCategory = value;
                CallLoadDecisionsCommand();
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
