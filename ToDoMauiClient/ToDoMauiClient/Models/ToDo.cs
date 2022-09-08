using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoMauiClient.Models
{
  public class ToDo : INotifyPropertyChanged
  {

    int _Id;
    
    public int Id 
    { 
      get => _Id; 
      set 
      { 
        if(_Id == value)
          return;
        _Id = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
      } 
    }



    string _ToDoName;
    public string ToDoName
    { 
      get => _ToDoName ?? string.Empty;
      set
      {
        if (_ToDoName == value)
          return;
        _ToDoName = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
      }
    }


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
