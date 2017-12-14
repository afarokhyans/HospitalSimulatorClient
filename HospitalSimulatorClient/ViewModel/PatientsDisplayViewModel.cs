using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HospitalSimulatorClient.Model;
using HospitalSimulatorClient.Properties;

namespace HospitalSimulatorClient.ViewModel
{
    public class PatientsDisplayViewModel : ViewModelBase
    {
        public PatientsDisplayViewModel()
        {
            RegisteredPatients = new RelayCommand(ExecuteRegisteredPatients);
        }

        public static RelayCommand RegisteredPatients { get; set; }

        private List<Patient> _registeredPatientsList = new List<Patient>();
        public List<Patient> RegisteredPatientsList
        {
            get => _registeredPatientsList;
            set => Set(ref _registeredPatientsList, value);
        }

        private string _resultMessage;
        public string ResultMessage
        {
            get => _resultMessage;
            set => Set(ref _resultMessage, value);
        }

        private async void ExecuteRegisteredPatients()
        {
            ResultMessage = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Resources.BaseServerUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("api/patients");
                    if (response.IsSuccessStatusCode)
                    {
                        RegisteredPatientsList = await response.Content.ReadAsAsync<List<Patient>>();
                    }

                }
            }
            catch (HttpRequestException ex)
            {
                ResultMessage = ex.Message;
                RegisteredPatientsList = new List<Patient>();
            }
        }

    }
}
