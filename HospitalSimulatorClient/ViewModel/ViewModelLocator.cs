/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:HospitalSimulatorClient.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using HospitalSimulatorClient.Model;

namespace HospitalSimulatorClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataService, DataService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PatientRegistrationViewModel>();
            SimpleIoc.Default.Register<PatientsDisplayViewModel>();
            SimpleIoc.Default.Register<ConsultationsDisplayViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public PatientRegistrationViewModel PatientRegistration => ServiceLocator.Current.GetInstance<PatientRegistrationViewModel>();
        public PatientsDisplayViewModel PatientsDisplay => ServiceLocator.Current.GetInstance<PatientsDisplayViewModel>();
        public ConsultationsDisplayViewModel ConsultationsDisplay => ServiceLocator.Current.GetInstance<ConsultationsDisplayViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}