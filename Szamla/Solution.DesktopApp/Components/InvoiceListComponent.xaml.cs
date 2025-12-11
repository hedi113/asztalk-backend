using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.Components;

public partial class InvoiceListComponent : ContentView
{
    public static readonly BindableProperty InvoiceProperty = BindableProperty.Create(
         propertyName: nameof(Invoice),
         returnType: typeof(InvoiceModel),
         declaringType: typeof(InvoiceListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public InvoiceModel Invoice
    {
        get => (InvoiceModel)GetValue(InvoiceProperty);
        set => SetValue(InvoiceProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(InvoiceListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );


    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(InvoiceListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );
    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);


    public InvoiceListComponent()
	{
		InitializeComponent();
	}

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameters = new ShellNavigationQueryParameters 
        {
            {"Invoice", this.Invoice }
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(InvoiceView.Name, navigationQueryParameters);
    }
}