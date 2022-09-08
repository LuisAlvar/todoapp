using System.Diagnostics;
using System.Text.Json;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
	private readonly IRestDataService _dataService;

	ToDo _toDo;
	bool _isNew;
	string _jsonToDo;

	public ToDo ToDo
	{
		get => _toDo;
		set
		{
			_isNew = IsNew(value);
			_toDo = value;
			_jsonToDo = JsonSerializer.Serialize<ToDo>(_toDo);
			OnPropertyChanged();
		}
	}


	public ManageToDoPage(IRestDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
		BindingContext = this; //<----- this is important above all else 
	}


	public bool IsNew(ToDo data)
	{
		return data.Id == 0;
	}


	async void OnSaveButtonClick(object sender, EventArgs e)
	{
		try
		{
			if (_isNew)
			{
				Debug.WriteLine($"-------<> Adding a new ToDo object \n {_jsonToDo}");
				await _dataService.AddToDoAsync(ToDo);
			}
			else
			{
				Debug.WriteLine($"--------<> Updating an ToDo Object \n {_jsonToDo}");
				await _dataService.UpdateToDoAsync(ToDo);
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Error occur on Save Button Click - " + ex.Message);
		}

    await Shell.Current.GoToAsync("..");
  }


	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	async void OnDeleteButtonClick(object sender, EventArgs e)
	{
    Debug.WriteLine($"--------<> Delete an ToDo Object \n{_jsonToDo}");
    await _dataService.DeleteToDoAsync(ToDo.Id);
    await Shell.Current.GoToAsync("..");
  }


	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	async void OnCancelButtonClick(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}

}