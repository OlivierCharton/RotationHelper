using Gw2Sharp.Models;
using Gw2Sharp.WebApi.V2.Models;
using Japyx.RotationHelper.Enums;
using Japyx.RotationHelper.Services;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Japyx.RotationHelper.Models
{
    [DataContract]
    public class RotationModel
    {
        private readonly Action _requestSave;
        private readonly ObservableCollection<RotationModel> _rotationModels;
        private readonly Data _data;

        private bool _initialized;

        private string _name;
        private SpecializationType _specialization;

        public RotationModel()
        {

        }

        public RotationModel(string name, string modulePath, Action requestSave, ObservableCollection<RotationModel> rotationModels, Data data)
        {
            ModulePath = modulePath;
            _requestSave = requestSave;
            _rotationModels = rotationModels;
            _data = data;

            _initialized = true; //TODO: why ?

            Name = name;
        }

        public event EventHandler Updated;

        public event EventHandler Deleted;

        [DataMember]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [DataMember]
        public SpecializationType Specialization
        {
            get => _specialization;
            set => SetProperty(ref _specialization, value);
        }

        //public List<RotationDetail> RotationDetails { get; set; }

        public string SpecializationName => _data != null && Specialization != SpecializationType.None && Enum.IsDefined(typeof(SpecializationType), Specialization)
                    ? _data.Specializations[Specialization].Name
                    : "Spé inconnue";

        public string ModulePath { get; private set; }

        public void Delete()
        {
            Deleted?.Invoke(null, null);
            _ = _rotationModels.Remove(this);
            Save();
        }

        public void Initialize(string modulePath)
        {
            _initialized = true;
            ModulePath = modulePath;
        }

        protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string caller = "")
        {
            if (Equals(property, newValue))
            {
                return false;
            }

            property = newValue;
            if (_initialized)
            {
                OnUpdated();
            }

            return true;
        }

        public void Save()
        {
            if (_initialized) _requestSave?.Invoke();
        }

        public void UpdateRotation(string name)
        {
            _name = name;
            _specialization = SpecializationType.None;
        }

        private void OnUpdated(bool save = true)
        {
            Updated?.Invoke(this, EventArgs.Empty);
            if (save) Save();
        }
    }
}
