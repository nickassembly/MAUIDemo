using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;
using System.Diagnostics;


namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
	private readonly IRestDataService _dataService;
	ToDo _toDo;
	bool _isNew;

	public ToDo ToDo
	{
		get => _toDo;
		set
		{
			_isNew = IsNew(value);
			_toDo = value;
			OnPropertyChanged();
		}
	}

	public ManageToDoPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;
		BindingContext = this;
	}

	bool IsNew(ToDo todo)
	{
		return todo.Id == 0;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if(_isNew)
		{
			Debug.WriteLine("---> Add new Item");
			await _dataService.AddToDoAsync(ToDo);

		}
		else
		{
            Debug.WriteLine("---> Update Item");
            await _dataService.UpdateToDoAsync(ToDo);
        }

        await Shell.Current.GoToAsync("..");
    }

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteToDoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}