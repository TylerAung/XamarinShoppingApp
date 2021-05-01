using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace XamarinCapstoneProj.Helpers.Validators
{
    interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }

        bool Validate();

        bool IsValid { get; set; }
    }
}
